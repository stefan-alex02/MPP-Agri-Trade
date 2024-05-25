namespace Domain.Products;

public class Product : Entity<int> {
    public string Name { get; set; } = null!;
    public ProductCategory Category { get; set; } = null!;

    public Product() : base(default) {
    }
    
    public Product(int id, string name, ProductCategory category) : base(id) {
        Name = name;
        Category = category;
    }
}