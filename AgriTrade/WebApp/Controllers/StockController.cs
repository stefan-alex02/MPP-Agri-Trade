using Business.Services;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.DTO;

namespace WebApp.Controllers;

public class StockController(StockService stockService) : Controller {
    [HttpGet("api/stock")]
    public async Task<ActionResult<StockDto[]>> GetStocks() {
        try {
            var stocks = stockService.GetAllStocks().Select(s => new StockDto {
                ProductName = s.Product!.Name!,
                Category = s.Product.Category!.Name!,
                Amount = s.Amount,
                Unit = s.Unit!,
                Price = s.Price,
                ProducerFirstName = s.Producer!.FirstName!,
                ProducerLastName = s.Producer!.LastName!
            }).ToArray();
            
            return Ok(stocks);
        }
        catch (Exception e) {
            return BadRequest();
        }
    }
}