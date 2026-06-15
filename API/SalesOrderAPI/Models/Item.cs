namespace SalesOrderAPI.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string UOM { get; set; } = string.Empty;
    }
}
