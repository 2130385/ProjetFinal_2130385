USE DronesDatabase
GO
-- Insérer des enregistrements dans la table Adresse
INSERT INTO Adresse (AdresseID, NoPorte, Rue, Ville, CodePostal, Province, Pays)
VALUES 
    (1, '123', 'Rue Principale', 'Montréal', 'H1A 1A1', 'Québec', 'Canada'),
    (2, '456', 'Avenue Centrale', 'Toronto', 'M2N 2N2', 'Ontario', 'Canada'),
    (3, '789', 'Boulevard Secondaire', 'Vancouver', 'V6C 1A1', 'Colombie-Britannique', 'Canada'),
    (4, '101', 'Rue de la Rivière', 'Québec', 'G1V 4M2', 'Québec', 'Canada'),
    (5, '222', 'Place des Artistes', 'Ottawa', 'K1P 1J1', 'Ontario', 'Canada'),
    (6, '333', 'Chemin des Montagnes', 'Calgary', 'T2P 1A1', 'Alberta', 'Canada'),
    (7, '444', 'Rue du Commerce', 'Edmonton', 'T5J 1A1', 'Alberta', 'Canada'),
    (8, '555', 'Avenue des Arbres', 'Halifax', 'B3H 1A1', 'Nouvelle-Écosse', 'Canada'),
    (9, '666', 'Chemin de la Plage', 'Victoria', 'V8W 1A1', 'Colombie-Britannique', 'Canada'),
    (10, '777', 'Boulevard des Bois', 'Regina', 'S4P 1A1', 'Saskatchewan', 'Canada'),
	(11, '888', 'Rue de la Paix', 'Montréal', 'H2H 2H2', 'Québec', 'Canada'),
    (12, '999', 'Avenue de la Liberté', 'Toronto', 'M3M 3M3', 'Ontario', 'Canada'),
    (13, '111', 'Boulevard de l''Espoir', 'Vancouver', 'V7V 7V7', 'Colombie-Britannique', 'Canada'),
    (14, '222', 'Rue de la Renaissance', 'Québec', 'G2G 2G2', 'Québec', 'Canada'),
    (15, '333', 'Place du Bonheur', 'Ottawa', 'K2K 2K2', 'Ontario', 'Canada'),
    (16, '444', 'Chemin de la Révolution', 'Calgary', 'T3T 3T3', 'Alberta', 'Canada'),
    (17, '555', 'Rue de la Découverte', 'Edmonton', 'T6T 6T6', 'Alberta', 'Canada'),
    (18, '666', 'Avenue de la Concorde', 'Halifax', 'B4B 4B4', 'Nouvelle-Écosse', 'Canada'),
    (19, '777', 'Chemin de l''Avenir', 'Victoria', 'V9V 9V9', 'Colombie-Britannique', 'Canada'),
    (20, '888', 'Boulevard de la Prospérité', 'Regina', 'S5S 5S5', 'Saskatchewan', 'Canada');
GO

-- Insérer des enregistrements dans la table Modele
INSERT INTO Magasins.Modele (ModeleID, Nom, Vitesse, Prix, DateSortie)
VALUES 
    (1, 'ModelA', 50, 1000.00, '2023-01-01'),
    (2, 'ModelB', 60, 1200.00, '2023-02-01'),
    (3, 'ModelC', 55, 1100.00, '2023-03-01'),
    (4, 'ModelD', 65, 1300.00, '2023-04-01'),
    (5, 'ModelE', 70, 1400.00, '2023-05-01');
GO

-- Insérer des enregistrements dans la table Magasin
INSERT INTO Magasins.Magasin (MagasinID, AdresseID)
VALUES 
    (1, 1),
    (2, 2),
    (3, 3),
    (4, 4),
    (5, 5),
    (6, 6),
    (7, 7),
    (8, 8),
    (9, 9),
    (10, 10);
GO

-- Insérer des enregistrements dans la table Drone
INSERT INTO Magasins.Drone (DroneID, ModeleID, NumSerie)
VALUES 
    (1, 1, 'ABC123'),
    (2, 2, 'DEF456'),
    (3, 3, 'GHI789'),
    (4, 4, 'JKL012'),
    (5, 5, 'MNO345'),
    (6, 3, 'PQR678'),
    (7, 4, 'STU901'),
    (8, 5, 'VWX234'),
    (9, 3, 'YZA567'),
    (10, 4, 'BCD890');
GO

-- Insérer des enregistrements dans la table Client

EXEC Clients.usp_AjoutClient 1, 11, 'Jean', 'Dupont', '123-456-7890', NULL, '1990-05-15'
EXEC Clients.usp_AjoutClient 2, 12, 'Marie', 'Tremblay', '234-567-8901', '345-678-9012', '1985-10-20'
EXEC Clients.usp_AjoutClient 3, 13, 'Pierre', 'Leblanc', '345-678-9012', NULL, '1980-03-25'
EXEC Clients.usp_AjoutClient 4, 14, 'Julie', 'Lamontagne', '456-789-0123', NULL, '1975-08-30'
EXEC Clients.usp_AjoutClient 5, 15, 'Luc', 'Bouchard', '567-890-1234', '678-901-2345', '1970-01-05'

-- Insérer des enregistrements dans la table Courriel
INSERT INTO Clients.Courriel (CourrielID, ClientID, courriel)
VALUES 
    (1, 1, 'jean.dupont@example.com'),
    (2, 2, 'marie.tremblay@example.com'),
    (3, 3, 'pierre.leblanc@example.com'),
    (4, 4, 'julie.lamontagne@example.com'),
    (5, 5, 'luc.bouchard@example.com')
GO

-- Insérer des enregistrements dans la table Commande
INSERT INTO Magasins.Commande (CommandeID, ClientID, DroneID, MagasinID, DateCommande, Montant)
VALUES 
    (1, 1, 1, 1, '2023-01-02', 1000.00),
    (2, 2, 2, 2, '2023-01-03', 1200.00),
    (3, 3, 3, 3, '2023-01-04', 1100.00),
    (4, 4, 4, 4, '2023-01-05', 1300.00),
    (5, 5, 5, 5, '2023-01-06', 1400.00),
    (6, 3, 6, 6, '2023-01-07', 1500.00),
    (7, 3, 7, 7, '2023-01-08', 1600.00),
    (8, 3, 8, 8, '2023-01-09', 1700.00),
    (9, 4, 9, 9, '2023-01-10', 1800.00),
    (10, 5, 10, 10, '2023-01-11', 1900.00);
GO