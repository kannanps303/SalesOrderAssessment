CREATE PROCEDURE sp_Login
(
 @Username VARCHAR(50)
)
AS
BEGIN
    SELECT *
    FROM Users
    WHERE Username = @Username
END