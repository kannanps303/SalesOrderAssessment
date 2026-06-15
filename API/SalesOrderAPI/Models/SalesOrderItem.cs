namespace SalesOrderAPI.Models
{
    public class SalesOrderItem
    {
        public int ItemId { get; set; }

        public string ItemCode { get; set; } = string.Empty;

        public string ItemName { get; set; } = string.Empty;

        public string UOM { get; set; } = string.Empty;

        public decimal Quantity { get; set; }
    }
}
