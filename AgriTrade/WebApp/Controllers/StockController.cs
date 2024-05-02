using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.DTO;

namespace WebApp.Controllers;

public class StockController(StockService stockService) : Controller {
    [HttpGet("api/stocks")]
    [Authorize]
    public async Task<ActionResult<StockDto[]>> GetStocks() {
        if (!HttpContext.User.Identity.IsAuthenticated) {
            return StatusCode(469);
        }
        
        try {
            var stocks = stockService.GetAllStocks().Select(s => new StockDto {
                StockId = s.Id,
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
    
    [HttpGet("api/stock/{id}")]
    [Authorize]
    public async Task<ActionResult<StockDto>> GetStock(int id) {
        if (!HttpContext.User.Identity.IsAuthenticated) {
            return Unauthorized();
        }
        
        try {
            var stock = stockService.GetStockById(id);
            
            var stockDto = new StockDto {
                StockId = stock.Id,
                ProductName = stock.Product!.Name!,
                Category = stock.Product.Category!.Name!,
                Amount = stock.Amount,
                Unit = stock.Unit!,
                Price = stock.Price,
                ProducerFirstName = stock.Producer!.FirstName!,
                ProducerLastName = stock.Producer!.LastName!
            };
            
            return Ok(stockDto);
        }
        catch (Exception e) {
            return BadRequest();
        }
    }
}