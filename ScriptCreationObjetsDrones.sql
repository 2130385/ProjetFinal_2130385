USE DronesDatabase
GO

--VUE: Magasins.vw_Drones
CREATE VIEW Magasins.vw_Drones AS
SELECT 
    D.DroneID,
    D.NumSerie,
    M.Nom AS NomModele,
    M.Vitesse,
    M.Prix,
    M.DateSortie
FROM 
    Magasins.Drone AS D
INNER JOIN 
    Magasins.Modele AS M ON D.ModeleID = M.ModeleID;
GO

--VUE: Magasins.vw_Commandes
CREATE VIEW Magasins.vw_Commandes AS
SELECT 
    CMD.CommandeID,
    CLI.NomFamille + ', ' + CLI.Prenom AS NomClient,
	COU.courriel,
    DR.NumSerie AS NumSerieDrone,
    MAG.MagasinID,
    ADR.NoPorte + ' ' + ADR.Rue + ', ' + ADR.Ville + ', ' + ADR.Province + ', ' + ADR.Pays AS AdresseMagasin,
    CMD.DateCommande,
    CMD.Montant
FROM 
    Magasins.Commande AS CMD
INNER JOIN 
    Clients.Client AS CLI ON CMD.ClientID = CLI.ClientID
INNER JOIN 
    Magasins.Drone AS DR ON CMD.DroneID = DR.DroneID
INNER JOIN 
    Magasins.Magasin AS MAG ON CMD.MagasinID = MAG.MagasinID
INNER JOIN
	dbo.Adresse AS ADR ON MAG.AdresseID = ADR.AdresseID
INNER JOIN
	Clients.Courriel AS COU ON CLI.ClientID = COU.ClientID
GO

--FONCTION: Quel revenu est généré pour un certain modèle de drone donné en paramètre?
CREATE FUNCTION Magasins.ufn_VentesModele (@ModeleID INT)
RETURNS DECIMAL(10, 2)
AS
BEGIN
    DECLARE @Revenu DECIMAL(10, 2);

    SELECT @Revenu = ISNULL(SUM(M.Prix * CMD.NombreVentes), 0)
    FROM Magasins.Modele M
    LEFT JOIN (
        SELECT D.ModeleID, COUNT(*) AS NombreVentes
        FROM Magasins.Commande CMD
        INNER JOIN Magasins.Drone D ON CMD.DroneID = D.DroneID
        GROUP BY D.ModeleID
    ) CMD ON M.ModeleID = CMD.ModeleID
    WHERE M.ModeleID = @ModeleID;

    RETURN @Revenu;
END;
GO

--TEST de la fonction Magasins.ufn_VentesModele
DECLARE @ModeleID1 INT = 1;
DECLARE @ModeleID2 INT = 2;

PRINT 'Revenu total pour le modèle avec l''ID ' + CAST(@ModeleID1 AS VARCHAR) + ' : ' + CAST(Magasins.ufn_VentesModele(@ModeleID1) AS VARCHAR);
PRINT 'Revenu total pour le modèle avec l''ID ' + CAST(@ModeleID2 AS VARCHAR) + ' : ' + CAST(Magasins.ufn_VentesModele(@ModeleID2) AS VARCHAR);
GO

--PROCÉDURE: Récupération du revenu généré par les ventes d'un modèle en question donné en paramète qui utilise la fonction Magasins.ufn_VentesModele
CREATE PROCEDURE Magasins.usp_RecupRevenuSelonModele
    @ModeleID INT
AS
BEGIN
    DECLARE @Revenu DECIMAL(10, 2);

    SET @Revenu = Magasins.ufn_VentesModele(@ModeleID);
	
    PRINT 'Le revenu total pour le modèle avec l''ID ' + CAST(@ModeleID AS VARCHAR) + ' est : ' + CAST(@Revenu AS VARCHAR);
END;
GO

--TEST de la procédure Magasins.usp_RecupRevenuSelonModele
EXEC Magasins.usp_RecupRevenuSelonModele @ModeleID = 3;
EXEC Magasins.usp_RecupRevenuSelonModele @ModeleID = 4;
GO

--PROCÉDURE: Récupération des informations pour un client donné en fonction de son ID
CREATE PROCEDURE Clients.usp_RecupInfosClient
    @ClientID INT
AS
BEGIN
    SELECT 
        CLI.ClientID,
        CLI.Prenom,
        CLI.NomFamille,
        CLI.NoTel1,
        CLI.NoTel2,
        CLI.DateNaissance,
        COU.courriel
    FROM 
        Clients.Client AS CLI
    INNER JOIN
        Clients.Courriel AS COU ON CLI.ClientID = COU.ClientID
    WHERE 
        CLI.ClientID = @ClientID;
END;
GO

--TEST de la procédure Clients.usp_RecupInfosClient
EXEC Clients.usp_RecupInfosClient @ClientID = 1;
EXEC Clients.usp_RecupInfosClient @ClientID = 2;
GO

--DÉCLENCHEUR: Modification à la table [Magasins].[Modele]
CREATE TRIGGER Magasins.trg_ModificationModelesDrone
ON [Magasins].[Modele]
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    PRINT 'Modification à la table des modèles de drone détectée. Voir la section des résultats pour afficher la ou les modification(s).';

    IF EXISTS(SELECT * FROM inserted)
    BEGIN
        SELECT 'Données insérées ou mises à jour en date du ' + CAST(FORMAT(GETDATE(), 'yyyy-MM-dd HH:mm:ss') AS VARCHAR(30)) + '. ModeleID: ' + CAST(ModeleID AS VARCHAR(10)) +
               ', Nom: ' + Nom +
               ', Vitesse: ' + CAST(Vitesse AS VARCHAR(10)) +
               ', Prix: ' + CAST(Prix AS VARCHAR(10)) +
               ', DateSortie: ' + CONVERT(VARCHAR(20), DateSortie, 120)
        FROM inserted;
    END

    IF EXISTS(SELECT * FROM deleted)
    BEGIN
        SELECT 'Données supprimées en date du ' + CAST(FORMAT(GETDATE(), 'yyyy-MM-dd HH:mm:ss') AS VARCHAR(30)) + '. ModeleID: ' + CAST(ModeleID AS VARCHAR(10)) +
               ', Nom: ' + Nom +
               ', Vitesse: ' + CAST(Vitesse AS VARCHAR(10)) +
               ', Prix: ' + CAST(Prix AS VARCHAR(10)) +
               ', DateSortie: ' + CONVERT(VARCHAR(20), DateSortie, 120)
        FROM deleted;
	END
END;
GO

--TEST du déclencheur Magasins.trg_ModificationDrone
INSERT INTO Magasins.Modele (ModeleID, Nom, Vitesse, Prix, DateSortie)
VALUES (6, 'ModelF', 100, 1500, '2023-06-01');

UPDATE Magasins.Modele
SET Prix = 1500
WHERE ModeleID = 1;

DELETE FROM Magasins.Modele
WHERE ModeleID = 6;