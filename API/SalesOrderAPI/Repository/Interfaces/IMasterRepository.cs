using SalesOrderAPI.Models;

namespace SalesOrderAPI.Repository.Interfaces
{
    public interface IMasterRepository
    {
        Task<List<Vendor>> GetVendorsAsync();

        Task<List<Item>> GetItemsAsync();
        Task<List<Item>> GetVendorItemsAsync(int vendorId);
        Task SaveSalesOrderAsync(SalesOrderRequest request);
    }
}
