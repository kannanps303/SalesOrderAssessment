CREATE OR ALTER PROCEDURE sp_GetItems
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Code,
        Name,
        UOM
    FROM Items
    ORDER BY Name;
END
GO