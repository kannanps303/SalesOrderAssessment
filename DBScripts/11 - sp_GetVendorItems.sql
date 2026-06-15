CREATE PROCEDURE sp_GetVendorItems
(
    @VendorId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT
           I.Id,
           I.Code,
           I.Name,
           I.UOM
    FROM SalesOrderItems SOI
    INNER JOIN SalesOrders SO
        ON SOI.SalesOrderId = SO.Id
    INNER JOIN Items I
        ON SOI.ItemId = I.Id
    WHERE SO.VendorId = @VendorId
    ORDER BY I.Name;
END
GO