INSERT INTO Users
(
    UserName,
    PasswordHash,
    FullName,
    RoleName
)
VALUES
('admin',
 'Admin@123',
 'System Administrator',
 'Admin'),

('user1',
 'User@123',
 'Normal User',
 'User');
GO

INSERT INTO Vendors(Code, Name)
VALUES
('V001', 'ABC Traders'),
('V002', 'XYZ Suppliers');
GO

INSERT INTO Items(Code, Name, UOM)
VALUES
('ITM001', 'Laptop', 'PCS'),
('ITM002', 'Mouse', 'PCS'),
('ITM003', 'Keyboard', 'PCS');
GO