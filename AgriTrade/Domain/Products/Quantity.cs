using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Products;

public class Quantity : Entity<int> {
    [ForeignKey("Stock")]
    public int StockId { get; set; }
    public Stock? Stock { get; set; }

    [ForeignKey("Order")]
    public int OrderId { get; set; }
    public Order? Order { get; set; }
    public float Amount { get; set; }

    [NotMapped]
    public float Price =>
        (Stock ??
         throw new NullReferenceException(nameof(Stock))
        ).Price * Amount;

    public Quantity() : base(default) {
    }

    public Quantity(int id, Stock? stock, Order? order, float amount) :
        base(id) {
        Stock = stock;
        Order = order;
        StockId = stock?.Id ?? default;
        OrderId = order?.Id ?? default;
        Amount = amount;
    }
}