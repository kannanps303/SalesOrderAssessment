CREATE OR ALTER PROCEDURE sp_SaveSalesOrder
(
      @DocNo VARCHAR(50)
    , @DocDate DATE
    , @VendorId INT
    , @Items SalesOrderItemType READONLY
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SalesOrderId INT;

    BEGIN TRY

        BEGIN TRANSACTION;

        ----------------------------------
        -- Check Existing Order
        ----------------------------------
        SELECT @SalesOrderId = Id
        FROM SalesOrders
        WHERE DocNo = @DocNo;

        ----------------------------------
        -- Insert Header
        ----------------------------------
        IF @SalesOrderId IS NULL
        BEGIN

            INSERT INTO SalesOrders
            (
                DocNo,
                DocDate,
                VendorId
            )
            VALUES
            (
                @DocNo,
                @DocDate,
                @VendorId
            );

            SET @SalesOrderId = SCOPE_IDENTITY();

        END
        ELSE
        BEGIN

            UPDATE SalesOrders
            SET
                DocDate = @DocDate,
                VendorId = @VendorId
            WHERE Id = @SalesOrderId;

            DELETE FROM SalesOrderItems
            WHERE SalesOrderId = @SalesOrderId;

        END

        ----------------------------------
        -- Insert Details
        ----------------------------------
        INSERT INTO SalesOrderItems
        (
            SalesOrderId,
            ItemId,
            Quantity
        )
        SELECT
            @SalesOrderId,
            ItemId,
            Quantity
        FROM @Items;

        COMMIT TRANSACTION;

    END TRY

    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;

    END CATCH
END
GO