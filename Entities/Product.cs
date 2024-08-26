using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using practice_app.Common;
using Spectre.Console;

namespace practice_app.Entities;

public class Product : BaseEntity
{
    [Required]
    public string Name { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;
    [Required]
    public DateTime CreatedDate { get; private set; }
    public DateTime? UpdatedDate { get; private set; }
    public Product() { }

    public static Product Create(string name, string description = "")
    {
        return new Product()
        {
            Name = name,
            Description = description,
            CreatedDate = DateTime.Now
        };
    }
}

public static class ProductExtensions
{
    public static void WriteConsole(this IEnumerable<Product> products)
    {
        TableConsole.Write(
            "Products",
            new TableData<Product>(
                Columns: ["Id", "Name", "Description", "CreatedDate", "UpdatedDate"],
                Items: products,
                GetValues: (p) => [p.Id.ToString(), p.Name, p.Description, p.CreatedDate.ToString(), p.UpdatedDate?.ToString() ?? "NULL"]
            )
        );
    }

    public static void WriteConsole(this Product product) => WriteConsole([product]);
}
