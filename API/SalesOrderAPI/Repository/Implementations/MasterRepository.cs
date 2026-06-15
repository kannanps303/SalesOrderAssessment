using Microsoft.Data.SqlClient;
using SalesOrderAPI.Data;
using SalesOrderAPI.Models;
using SalesOrderAPI.Repository.Interfaces;
using System.Data;


public class MasterRepository : IMasterRepository
{
    private readonly DbContextADO _db;

    public MasterRepository(DbContextADO db)
    {
        _db = db;
    }

    public async Task<List<Vendor>> GetVendorsAsync()
    {
        List<Vendor> vendors = new();

        using SqlConnection con = _db.CreateConnection();

        using SqlCommand cmd =
            new SqlCommand("sp_GetVendors", con);

        cmd.CommandType = CommandType.StoredProcedure;

        await con.OpenAsync();

        using SqlDataReader reader =
            await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            vendors.Add(new Vendor
            {
                Id = Convert.ToInt32(reader["Id"]),
                Code = reader["Code"].ToString()!,
                Name = reader["Name"].ToString()!
            });
        }

        return vendors;
    }

    public async Task<List<Item>> GetItemsAsync()
    {
        List<Item> items = new();

        using SqlConnection con = _db.CreateConnection();

        using SqlCommand cmd =
            new SqlCommand("sp_GetItems", con);

        cmd.CommandType = CommandType.StoredProcedure;

        await con.OpenAsync();

        using SqlDataReader reader =
            await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            items.Add(new Item
            {
                Id = Convert.ToInt32(reader["Id"]),
                Code = reader["Code"].ToString()!,
                Name = reader["Name"].ToString()!,
                UOM = reader["UOM"].ToString()!
            });
        }

        return items;
    }
    public async Task<List<Item>> GetVendorItemsAsync(int vendorId)
    {
        List<Item> items = new();

        using SqlConnection con = _db.CreateConnection();

        using SqlCommand cmd =
            new SqlCommand("sp_GetVendorItems", con);

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@VendorId", vendorId);

        await con.OpenAsync();

        using SqlDataReader reader =
            await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            items.Add(new Item
            {
                Id = Convert.ToInt32(reader["Id"]),
                Code = reader["Code"].ToString()!,
                Name = reader["Name"].ToString()!,
                UOM = reader["UOM"].ToString()!
            });
        }

        return items;
    }

    public async Task SaveSalesOrderAsync(
    SalesOrderRequest request)
    {
        using SqlConnection con =
            _db.CreateConnection();

        using SqlCommand cmd =
            new SqlCommand("sp_SaveSalesOrder", con);

        cmd.CommandType =
            CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue(
            "@DocNo",
            request.DocNo);

        cmd.Parameters.AddWithValue(
            "@DocDate",
            request.DocDate);

        cmd.Parameters.AddWithValue(
            "@VendorId",
            request.VendorId);

        DataTable dt = new DataTable();

        dt.Columns.Add("ItemId", typeof(int));
        dt.Columns.Add("Quantity", typeof(decimal));

        foreach (var item in request.Items)
        {
            dt.Rows.Add(
                item.ItemId,
                item.Quantity);
        }

        SqlParameter tvp =
            cmd.Parameters.AddWithValue(
                "@Items",
                dt);

        tvp.SqlDbType = SqlDbType.Structured;
        tvp.TypeName = "SalesOrderItemType";

        await con.OpenAsync();

        await cmd.ExecuteNonQueryAsync();
    }
}