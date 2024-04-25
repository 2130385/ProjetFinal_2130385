-- Création de la base de données
IF EXISTS(SELECT * FROM sys.databases WHERE name='DronesDatabase')
BEGIN
	DROP DATABASE DronesDatabase
END
CREATE DATABASE DronesDatabase;
GO