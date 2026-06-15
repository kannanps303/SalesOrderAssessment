CREATE TABLE SalesOrderItems
(
    Id INT IDENTITY(1,1) PRIMARY KEY,

    SalesOrderId INT NOT NULL,
    ItemId INT NOT NULL,

    Quantity DECIMAL(18,2) NOT NULL,

    CONSTRAINT FK_SalesOrderItems_SalesOrders
        FOREIGN KEY (SalesOrderId)
        REFERENCES SalesOrders(Id)
        ON DELETE CASCADE,

    CONSTRAINT FK_SalesOrderItems_Items
        FOREIGN KEY (ItemId)
        REFERENCES Items(Id)
);
GO