using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace practice_app.Entities;

public class Product: BaseEntity
{
    [Required]
    public string Name { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;
    [Required]
    public DateTime CreatedDate { get; private set; }
    public DateTime? UpdatedDate { get; private set; }
    private Product(){}

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