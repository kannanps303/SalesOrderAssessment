CREATE OR ALTER PROCEDURE sp_GetVendors
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Code,
        Name
    FROM Vendors
    ORDER BY Name;
END
GO