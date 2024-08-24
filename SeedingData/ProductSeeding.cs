using practice_app.DbContexts;
using practice_app.Entities;

namespace practice_app.SeedingData;

public class ProductSeeding: ISeedingData
{
    public string Key => $"1_product_table";
    
    public Task DoAsync(AppDbContext context)
    {
        var products = new List<Product>()
        {
            Product.Create("Iphone", "Sleek design, powerful performance, innovative features—iPhone redefines premium smartphone experience"),
            Product.Create("Laptop", "Portable computing powerhouse with versatility for work, play, and creativity"),
            Product.Create("Samsung","Innovative technology and sleek design define the Samsung experience globally."),
            Product.Create("IPad","Versatile tablet for productivity, entertainment, and creative pursuits with precision"),
            Product.Create("Dell", "Reliable performance and cutting-edge technology encapsulate the essence of Dell."),
            Product.Create("MSI", "MSI: Gaming excellence, powerful hardware, and sleek design for enthusiasts."),
            Product.Create("Monitor","Crystal-clear display, vibrant visuals – the epitome of immersive viewing."),
        };
        context.Products.AddRangeAsync(products);
        return context.SaveChangesAsync();
    }
}