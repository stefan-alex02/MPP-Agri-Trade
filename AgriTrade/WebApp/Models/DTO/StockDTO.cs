namespace WebApp.Models.DTO;

public class StockDto(
    int stockId,
    string productName,
    string category,
    float amount,
    string unit,
    float price,
    string producerFirstName,
    string producerLastName) {
    public int StockId { get; set; } = stockId;
    public string ProductName { get; set; } = productName;
    public string Category { get; set; } = category;
    public float Amount { get; set; } = amount;
    public string Unit { get; set; } = unit;
    public float Price { get; set; } = price;
    public string ProducerFirstName { get; set; } = producerFirstName;
    public string ProducerLastName { get; set; } = producerLastName;
}