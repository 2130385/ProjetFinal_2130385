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