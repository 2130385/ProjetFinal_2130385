-- Utilisation de la base de données créée
USE DronesDatabase;
GO

-- Création du schéma "Clients"
CREATE SCHEMA Clients;
GO

-- Création du schéma "Magasins"
CREATE SCHEMA Magasins;
GO

-- Création de la table Adresse
CREATE TABLE Adresse (
    AdresseID INT PRIMARY KEY,
    NoPorte VARCHAR(10),
    NoApp VARCHAR(10) NULL,
    Rue VARCHAR(100),
    Ville VARCHAR(100),
    CodePostal VARCHAR(10),
    Province VARCHAR(100),
    Pays VARCHAR(100)
);
GO

-- Création de la table Client
CREATE TABLE Clients.Client (
    ClientID INT PRIMARY KEY,
    AdresseID INT FOREIGN KEY REFERENCES Adresse(AdresseID),
    Prenom NVARCHAR(50),
    NomFamille NVARCHAR(50),
    NoTel1 VARCHAR(20),
    NoTel2 VARCHAR(20) NULL,
    DateNaissance DATE
);
GO

-- Création de la table Courriel
CREATE TABLE Clients.Courriel (
    CourrielID INT PRIMARY KEY,
    ClientID INT FOREIGN KEY REFERENCES Clients.Client(ClientID),
    courriel VARCHAR(100)
);
GO

-- Création de la contrainte UNIQUE pour l'email dans la table Courriel
CREATE UNIQUE INDEX UX_Courriel_Email ON Clients.Courriel (courriel);
GO

---- Création de la table Image
CREATE TABLE [Magasins].[Image] (
    ImageID INT IDENTITY(1,1),
    Nom NVARCHAR(100) NOT NULL,
    Identifiant UNIQUEIDENTIFIER NOT NULL ROWGUIDCOL,
    CONSTRAINT PK_Image_Image_ID PRIMARY KEY (ImageID)
);
GO

ALTER TABLE [Magasins].[Image] ADD CONSTRAINT UC_Image_Identifiant
UNIQUE (Identifiant);
GO

ALTER TABLE [Magasins].[Image] ADD CONSTRAINT DF_Image_Identifiant
DEFAULT newid() FOR Identifiant;
GO

ALTER TABLE [Magasins].[Image] ADD
FichierImage VARBINARY(max) FILESTREAM NULL;
GO

-- Création de la table Modele
CREATE TABLE Magasins.Modele (
    ModeleID INT PRIMARY KEY,
    Nom VARCHAR(100),
    Vitesse INT,
    Prix DECIMAL(10,2),
    DateSortie DATE,
    ImageID INT,
    CONSTRAINT FK_Modele_Image FOREIGN KEY (ImageID) REFERENCES [Magasins].[Image](ImageID)
);
GO

-- Création de la table Magasin
CREATE TABLE Magasins.Magasin (
    MagasinID INT PRIMARY KEY,
    AdresseID INT FOREIGN KEY REFERENCES Adresse(AdresseID)
);
GO

-- Création de la table Drone
CREATE TABLE Magasins.Drone (
    DroneID INT PRIMARY KEY,
    ModeleID INT FOREIGN KEY REFERENCES Magasins.Modele(ModeleID),
    NumSerie VARCHAR(50)
);
GO

-- Création de la table Commande
CREATE TABLE Magasins.Commande (
    CommandeID INT PRIMARY KEY,
    ClientID INT FOREIGN KEY REFERENCES Clients.Client(ClientID),
    DroneID INT FOREIGN KEY REFERENCES Magasins.Drone(DroneID),
    MagasinID INT FOREIGN KEY REFERENCES Magasins.Magasin(MagasinID),
    DateCommande DATE,
    Montant DECIMAL(10,2)
);


