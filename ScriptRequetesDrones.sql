USE DronesDatabase
-- 1)	Une requête sur une table, avec une clause WHERE et une clause ORDER BY
		--	Lesquels des clients ont une date de naissance antérieure au 1er janvier 1985?
SELECT *
FROM Clients.Client
WHERE DateNaissance < '1985-01-01'
ORDER BY DateNaissance DESC;
-- 2)	Une requête avec un INNER JOIN entre 2 tables avec un GROUP BY et une fonction d’agrégation
		--	Combien de commandes chacun des clients a fait?
SELECT c.ClientID, CONCAT(c.Prenom, ' ', c.NomFamille) AS NomComplet, COUNT(cmd.CommandeID) AS [Nombre de commandes]
FROM Clients.Client c
INNER JOIN Magasins.Commande cmd ON c.ClientID = cmd.ClientID
GROUP BY c.ClientID, c.Prenom, c.NomFamille;
-- 3)	Une requête qui part de votre requête en 2) et qui ajoute la clause HAVING
		--	Lesquels des clients ont passé plus qu'une commande?
SELECT c.ClientID, c.Prenom, COUNT(cmd.CommandeID) AS [Nombre de commandes]
FROM Clients.Client c
INNER JOIN Magasins.Commande cmd ON c.ClientID = cmd.ClientID
GROUP BY c.ClientID, c.Prenom
HAVING COUNT(cmd.CommandeID) > 1;
-- 4)	Une requête qui a un INNER JOIN avec vos 3 tables.
SELECT cmd.CommandeID, c.Prenom, m.ModeleID
FROM Magasins.Commande cmd
INNER JOIN Clients.Client c ON cmd.ClientID = c.ClientID
INNER JOIN Magasins.Drone d ON cmd.DroneID = d.DroneID
INNER JOIN Magasins.Modele m ON d.ModeleID = m.ModeleID;
-- 5)	Une requête qui a besoin d’utiliser une sous-requête.
		-- Lesquels des modèles de drones ont un prix supérieur à la moyenne des prix des modèles?
SELECT *
FROM Magasins.Modele
WHERE Prix > (SELECT AVG(Prix) FROM Magasins.Modele);