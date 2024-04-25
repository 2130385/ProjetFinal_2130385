-- Utilisation de la base de donn�es cr��e
USE DronesDatabase;
GO

-- Cr�ation du sch�ma "Clients"
CREATE SCHEMA Clients;
GO

-- Cr�ation du sch�ma "Magasins"
CREATE SCHEMA Magasins;
GO

-- Cr�ation de la table Adresse
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

-- Cr�ation de la table Client
CREATE TABLE Clients.Client (
    ClientID INT PRIMARY KEY,
    AdresseID INT FOREIGN KEY REFERENCES Adresse(AdresseID),
    Prenom VARCHAR(50),
    NomFamille VARCHAR(50),
    NoTel1 VARCHAR(20),
    NoTel2 VARCHAR(20) NULL,
    DateNaissance DATE
);
GO

-- Cr�ation de la table Courriel
CREATE TABLE Clients.Courriel (
    CourrielID INT PRIMARY KEY,
    ClientID INT FOREIGN KEY REFERENCES Clients.Client(ClientID),
    courriel VARCHAR(100)
);
GO

-- Cr�ation de la contrainte UNIQUE pour l'email dans la table Courriel
CREATE UNIQUE INDEX UX_Courriel_Email ON Clients.Courriel (courriel);
GO

-- Cr�ation de la table Modele
CREATE TABLE Magasins.Modele (
    ModeleID INT PRIMARY KEY,
    Nom VARCHAR(100),
    Vitesse INT,
    Prix DECIMAL(10,2),
    DateSortie DATE
);
GO

-- Cr�ation de la table Magasin
CREATE TABLE Magasins.Magasin (
    MagasinID INT PRIMARY KEY,
    AdresseID INT FOREIGN KEY REFERENCES Adresse(AdresseID)
);
GO

-- Cr�ation de la table Drone
CREATE TABLE Magasins.Drone (
    DroneID INT PRIMARY KEY,
    ModeleID INT FOREIGN KEY REFERENCES Magasins.Modele(ModeleID),
    NumSerie VARCHAR(50)
);
GO

-- Cr�ation de la table Commande
CREATE TABLE Magasins.Commande (
    CommandeID INT PRIMARY KEY,
    ClientID INT FOREIGN KEY REFERENCES Clients.Client(ClientID),
    DroneID INT FOREIGN KEY REFERENCES Magasins.Drone(DroneID),
    MagasinID INT FOREIGN KEY REFERENCES Magasins.Magasin(MagasinID),
    DateCommande DATE,
    Montant DECIMAL(10,2)
);
GO