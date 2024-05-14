-- Création de la base de données
IF EXISTS(SELECT * FROM sys.databases WHERE name='DronesDatabase')
BEGIN
	DROP DATABASE DronesDatabase
END
CREATE DATABASE DronesDatabase;
GO

EXEC sp_configure filestream_access_level, 2 RECONFIGURE

ALTER DATABASE DronesDatabase
ADD FILEGROUP FG_Images2130385 CONTAINS FILESTREAM;
GO

ALTER DATABASE DronesDatabase
ADD FILE (
	NAME = FG_Images2130385,
	FILENAME = 'C:\EspaceLabo\FG_Images2130385'
)
TO FILEGROUP FG_Images2130385
GO