using System.ComponentModel.DataAnnotations.Schema;
using Domain.Users;

namespace Domain.Products;

public class Order : Entity<int> {
    public Consumer OrderedBy { get; set; } = null!;
    public IEnumerable<Quantity> Products { get; set; } = new List<Quantity>();
    public DateTime Date { get; set; }
    
    public Order() : base(default) {
    }
    
    public Order(int id, Consumer orderedBy, IEnumerable<Quantity> products, DateTime date) : 
        base(id) {
        OrderedBy = orderedBy;
        Products = products;
        Date = date;
    }
}