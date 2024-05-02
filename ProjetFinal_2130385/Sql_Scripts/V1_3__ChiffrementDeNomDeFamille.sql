USE DronesDatabase
GO

--CLÉ--
CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'Passw0rd!TrèsFort!';
GO

--CERTIFICAT--
CREATE CERTIFICATE DronesCertificat WITH SUBJECT = 'ChiffrementNomFamille';
GO

--CLÉ SYMMÉTRIQUE--
CREATE SYMMETRIC KEY CleDrones WITH ALGORITHM = AES_256 ENCRYPTION BY CERTIFICATE DronesCertificat;
GO

--AJOUT CLIENT
CREATE PROCEDURE Clients.usp_AjoutClient
    @ClientID INT,
    @AdresseID INT,
    @Prenom NVARCHAR(50),
    @NomFamille NVARCHAR(50),
    @NoTel1 VARCHAR(15),
    @NoTel2 VARCHAR(15),
    @DateNaissance DATE
AS
BEGIN
    DECLARE @NomFamilleChiffre VARBINARY(MAX);

    OPEN SYMMETRIC KEY CleDrones
    DECRYPTION BY CERTIFICATE DronesCertificat;
    SET @NomFamilleChiffre = EncryptByKey(KEY_GUID('CleDrones'), @NomFamille);
    CLOSE SYMMETRIC KEY CleDrones;

    INSERT INTO Clients.Client (ClientID, AdresseID, Prenom, NomFamille, NoTel1, NoTel2, DateNaissance)
    VALUES (@ClientID, @AdresseID, @Prenom, CAST(@NomFamilleChiffre AS NVARCHAR(50)), @NoTel1, @NoTel2, @DateNaissance);
END
GO

--CHIFFREMENT NOM DE FAMILLE POUR UN CLIENT
CREATE PROCEDURE Clients.usp_ChiffrerNomFamilleClient
    @ClientID INT
AS
BEGIN
    DECLARE @NomFamille NVARCHAR(50);
    DECLARE @NomFamilleChiffre VARBINARY(MAX);

    SELECT @NomFamille = NomFamille
    FROM Clients.Client
    WHERE ClientID = @ClientID;

    OPEN SYMMETRIC KEY CleDrones
    DECRYPTION BY CERTIFICATE DronesCertificat;
    SET @NomFamilleChiffre = EncryptByKey(KEY_GUID('CleDrones'), @NomFamille)
    CLOSE SYMMETRIC KEY CleDrones;

    UPDATE Clients.Client
    SET NomFamille = CAST(@NomFamilleChiffre AS NVARCHAR(50))
    WHERE ClientID = @ClientID;
END
GO

-- DECHIFFREMENT NOM DE FAMILLE POUR UN CLIENT
CREATE PROCEDURE Clients.usp_DechiffrerNomFamilleClient
    @ClientID INT
AS
BEGIN
    DECLARE @NomFamille NVARCHAR(50);
    DECLARE @NomFamilleChiffre VARBINARY(MAX);

    SELECT @NomFamilleChiffre = CAST(NomFamille AS VARBINARY(MAX))
    FROM Clients.Client
    WHERE ClientID = @ClientID;

    OPEN SYMMETRIC KEY CleDrones
    DECRYPTION BY CERTIFICATE DronesCertificat;
	SET @NomFamille = CAST(DecryptByKey(@NomFamilleChiffre) AS NVARCHAR(50));
    CLOSE SYMMETRIC KEY CleDrones;

    UPDATE Clients.Client
    SET NomFamille = @NomFamille
    WHERE ClientID = @ClientID;
END
GO

--CHIFFREMENT NOM DE FAMILLE POUR TOUS LES CLIENTS
CREATE PROCEDURE Clients.usp_ChiffrerNomFamilleTousLesClients
AS
BEGIN
    DECLARE @ClientID INT;
    DECLARE @NomFamille NVARCHAR(50);
    DECLARE @NomFamilleChiffre VARBINARY(MAX);

    DECLARE client_cursor CURSOR FOR
    SELECT ClientID, NomFamille
    FROM Clients.Client;

    OPEN client_cursor;
    FETCH NEXT FROM client_cursor INTO @ClientID, @NomFamille;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        OPEN SYMMETRIC KEY CleDrones
        DECRYPTION BY CERTIFICATE DronesCertificat;
        SET @NomFamilleChiffre = EncryptByKey(KEY_GUID('CleDrones'), @NomFamille);
        CLOSE SYMMETRIC KEY CleDrones;

        UPDATE Clients.Client
        SET NomFamille = CAST(@NomFamilleChiffre AS NVARCHAR(50))
        WHERE ClientID = @ClientID;

        FETCH NEXT FROM client_cursor INTO @ClientID, @NomFamille;
    END;

    CLOSE client_cursor;
    DEALLOCATE client_cursor;
END
GO

--DECHIFFREMENT NOM DE FAMILLE POUR TOUS LES CLIENTS
CREATE PROCEDURE Clients.usp_DechiffrerNomFamilleTousLesClients
AS
BEGIN
    DECLARE @ClientID INT;
    DECLARE @NomFamille NVARCHAR(50);
    DECLARE @NomFamilleChiffre VARBINARY(MAX);

    DECLARE client_cursor CURSOR FOR
    SELECT ClientID, CAST(NomFamille AS VARBINARY(MAX))
    FROM Clients.Client;

    OPEN client_cursor;
    FETCH NEXT FROM client_cursor INTO @ClientID, @NomFamilleChiffre;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        OPEN SYMMETRIC KEY CleDrones
        DECRYPTION BY CERTIFICATE DronesCertificat;
        SET @NomFamille = CAST(DecryptByKey(@NomFamilleChiffre) AS NVARCHAR(50));
        CLOSE SYMMETRIC KEY CleDrones;

        UPDATE Clients.Client
        SET NomFamille = @NomFamille
        WHERE ClientID = @ClientID;

        FETCH NEXT FROM client_cursor INTO @ClientID, @NomFamilleChiffre;
    END;

    CLOSE client_cursor;
    DEALLOCATE client_cursor;
END
GO