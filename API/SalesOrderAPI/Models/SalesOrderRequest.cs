namespace SalesOrderAPI.Models
{
    public class SalesOrderRequest
    {
        public string DocNo { get; set; } = string.Empty;

        public DateTime DocDate { get; set; }

        public int VendorId { get; set; }

        public List<SalesOrderItemRequest> Items { get; set; }
            = new();
    }
}
