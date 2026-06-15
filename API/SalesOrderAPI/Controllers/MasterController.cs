using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesOrderAPI.Models;
using SalesOrderAPI.Repository.Interfaces;


[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MasterController : ControllerBase
{
    private readonly IMasterRepository _repository;

    public MasterController(IMasterRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("vendors")]
    public async Task<IActionResult> GetVendors()
    {
        var result = await _repository.GetVendorsAsync();

        return Ok(result);
    }

    [HttpGet("items")]
    public async Task<IActionResult> GetItems()
    {
        var result = await _repository.GetItemsAsync();

        return Ok(result);
    }

    [HttpGet("vendor-items/{vendorId}")]
    public async Task<IActionResult> GetVendorItems(int vendorId)
    {
        var result =
            await _repository.GetVendorItemsAsync(vendorId);

        return Ok(result);
    }
    [HttpPost("save")]
    public async Task<IActionResult> Save(
    SalesOrderRequest request)
    {
        await _repository.SaveSalesOrderAsync(request);

        return Ok(new
        {
            success = true,
            message = "Sales Order Saved Successfully"
        });
    }
}