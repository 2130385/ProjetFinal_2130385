USE DronesDatabase
GO
--FONCTION: Quel revenu est g�n�r� pour un certain mod�le de drone donn� en param�tre?
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

--PROC�DURE: R�cup�ration du revenu g�n�r� par les ventes d'un mod�le en question donn� en param�tre qui utilise la fonction Magasins.ufn_VentesModele
CREATE PROCEDURE Magasins.usp_RevenusPourUnModele
    @ModeleID INT,
    @Revenu DECIMAL(10, 2) OUTPUT
AS
BEGIN
    SET @Revenu = Magasins.ufn_RevenusPourUnModele(@ModeleID);
END;
