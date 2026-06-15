namespace SalesOrderAPI.Models
{
    public class SalesOrder
    {
        public int Id { get; set; }

        public string DocNo { get; set; } = string.Empty;

        public DateTime DocDate { get; set; }

        public int VendorId { get; set; }

        public string VendorName { get; set; } = string.Empty;

        public List<SalesOrderItem> Items { get; set; }
            = new();
    }
}
