using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Products;

public class Quantity : Entity<int> {
    public Stock? ReferencedStock { get; set; }
    public float Amount { get; set; }

    [NotMapped]
    public float Price =>
        (ReferencedStock ?? 
         throw new NullReferenceException(nameof(ReferencedStock))
        ).Price * Amount;

    public Quantity() : base(default) {
    }
    
    public Quantity(int id, Stock? referencedStock, float amount) : 
        base(id) {
        ReferencedStock = referencedStock;
        Amount = amount;
    }
}