USE DronesDatabase
GO

--PROC�DURE: R�cup�ration du revenu g�n�r� par les ventes d'un mod�le en question donn� en param�te qui utilise la fonction Magasins.ufn_VentesModele
CREATE PROCEDURE Magasins.usp_RecupRevenuSelonModele
    @ModeleID INT
AS
BEGIN
    DECLARE @Revenu DECIMAL(10, 2);

    SET @Revenu = Magasins.ufn_VentesModele(@ModeleID);
	
    PRINT 'Le revenu total pour le mod�le avec l''ID ' + CAST(@ModeleID AS VARCHAR) + ' est : ' + CAST(@Revenu AS VARCHAR);
END;
GO

--TEST de la proc�dure Magasins.usp_RecupRevenuSelonModele
EXEC Magasins.usp_RecupRevenuSelonModele @ModeleID = 3;
EXEC Magasins.usp_RecupRevenuSelonModele @ModeleID = 4;
GO

--PROC�DURE: R�cup�ration des informations pour un client donn� en fonction de son ID
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

--TEST de la proc�dure Clients.usp_RecupInfosClient
EXEC Clients.usp_RecupInfosClient @ClientID = 1;
EXEC Clients.usp_RecupInfosClient @ClientID = 2;
GO