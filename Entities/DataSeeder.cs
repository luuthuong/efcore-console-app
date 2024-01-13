using System.ComponentModel.DataAnnotations;

namespace practice_app.Entities;

public class DataSeeder: BaseEntity
{
    [Required]
    public string Key { get; set; }
}