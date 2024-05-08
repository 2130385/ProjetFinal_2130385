USE DronesDatabase
GO
--FONCTION: Quel revenu est généré pour un certain modèle de drone donné en paramètre?
CREATE FUNCTION Magasins.ufn_RevenusPourUnModele (@ModeleID INT)
RETURNS DECIMAL(10, 2)
AS
BEGIN
    DECLARE @Revenu DECIMAL(10, 2);

    SELECT @Revenu = ISNULL(SUM(M.Prix * CMD.NombreVentes), 0)
    FROM Magasins.Modele M
    LEFT JOIN (
        SELECT D.ModeleID, COUNT(*) AS NombreVentes
        FROM Magasins.Commande CMD
        INNER JOIN Magasins.Drone D ON CMD.DroneID = D.DroneID
        GROUP BY D.ModeleID
    ) CMD ON M.ModeleID = CMD.ModeleID
    WHERE M.ModeleID = @ModeleID;

    RETURN @Revenu;
END;
GO

--PROCÉDURE: Récupération du revenu généré par les ventes d'un modèle en question donné en paramètre qui utilise la fonction Magasins.ufn_VentesModele
CREATE PROCEDURE Magasins.usp_RevenusPourUnModele
    @ModeleID INT,
    @Revenu DECIMAL(10, 2) OUTPUT
AS
BEGIN
    SET @Revenu = Magasins.ufn_RevenusPourUnModele(@ModeleID);
END;
