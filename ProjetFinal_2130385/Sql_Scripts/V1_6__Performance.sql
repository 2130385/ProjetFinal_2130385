-- Cr�ation d'un index sur la colonne Ville
CREATE INDEX IX_Adresse_Ville
ON Adresse (Ville);

-- Cr�ation d'un index sur la colonne CodePostal
CREATE INDEX IX_Adresse_Province
ON Adresse (Province);