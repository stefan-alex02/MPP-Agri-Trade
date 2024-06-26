﻿namespace Domain.Products;

public class ProductCategory : Entity<int> {
    public string Name { get; set; } = null!;

    public ProductCategory() : base(default) { }    
    
    public ProductCategory(int id, string name) : base(id) {
        Name = name;
    }
}