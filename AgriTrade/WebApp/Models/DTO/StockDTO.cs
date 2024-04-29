namespace WebApp.Models.DTO;

public class StockDto {
    public string ProductName { get; set; }
    public string Category { get; set; }
    public float Amount { get; set; }
    public string Unit { get; set; }
    public float Price { get; set; }
    public string ProducerFirstName { get; set; }
    public string ProducerLastName { get; set; }
}