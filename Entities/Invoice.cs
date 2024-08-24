using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace practice_app.Entities;

public class Invoice: BaseEntity
{
    public required DateTime Date { get; set; }
    public string Description { get; set; } = "";
    public required Guid CustomerId { get; set; }

    [ForeignKey("CustomerId")]
    public virtual Customer Customer { get; set; }

    public Invoice(){

    }
}

public class InvoiceLine: BaseEntity
{
    public int InvoiceId { get; set; }

    [ForeignKey("InvoiceId")]
    public virtual required Invoice Invoice { get; set; }

    public int ProductId { get; set; }

    [ForeignKey("ProductId")]
    public virtual required Product Product { get; set; }

    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
}
