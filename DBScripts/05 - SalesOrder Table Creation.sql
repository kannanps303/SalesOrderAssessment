CREATE TABLE SalesOrders
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DocNo NVARCHAR(50) NOT NULL UNIQUE,
    DocDate DATE NOT NULL,
    VendorId INT NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT(GETDATE()),
    CONSTRAINT FK_SalesOrders_Vendors
        FOREIGN KEY (VendorId)
        REFERENCES Vendors(Id)
);
GO