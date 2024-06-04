using Business.Services;
using Domain.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.DTO;

namespace WebApp.Controllers;

public class StockController(StockService stockService) : Controller {
    [HttpGet("api/stocks")]
    [Authorize]
    public ActionResult<StockDto[]> GetStocks() {
        if (HttpContext.User.Identity is not { IsAuthenticated: true }) {
            return Unauthorized();
        }
        
        try {
            IEnumerable<StockDto> stocks = stockService.GetAllStocks().Select(s => new StockDto(
                s.Id, s.Product!.Name!, s.Product.Category!.Name!,
                s.Amount, s.Unit!, s.Price, s.Producer!.FirstName!, s.Producer!.LastName!,s.Description,s.Producer!.Username!
            ));
            
            return Ok(stocks.ToArray());
        }
        catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("api/stocks/{id}")]
    [Authorize]
    public ActionResult<StockDto> GetStock(int id) {
        if (HttpContext.User.Identity is not { IsAuthenticated: true }) {
            return Unauthorized();
        }
        
        try {
            Stock stock = stockService.GetStockById(id);
            
            StockDto stockDto = new StockDto (
                stock.Id,
                stock.Product!.Name!,
                stock.Product.Category!.Name!,
                stock.Amount,
                stock.Unit!,
                stock.Price,
                stock.Producer!.FirstName!,
                stock.Producer!.LastName!,
                stock.Description,
                stock.Producer!.Username!
            );
            
            return Ok(stockDto);
        }
        catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
}