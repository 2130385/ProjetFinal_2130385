-- Cr�ation de la base de donn�es
IF EXISTS(SELECT * FROM sys.databases WHERE name='DronesDatabase')
BEGIN
	DROP DATABASE DronesDatabase
END
CREATE DATABASE DronesDatabase;
GO