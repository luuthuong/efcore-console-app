using practice_app.Common;

namespace practice_app.Entities;

public class Customer : BaseEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Address { get; set; }
    public virtual List<Invoice> Invoices { get; set; }
}

public static class CustomerExtensions
{
    public static void WriteConsole(this IEnumerable<Customer> customers)
    {
        TableConsole.Write(
            "Customers",
            new TableData<Customer>(
                Columns: ["Id", "FirstName", "LastName", "Address"],
                Items: customers,
                GetValues: (c) => [c.Id.ToString(), c.FirstName, c.LastName, c.Address]
            )
        );
    }
    public static void WriteConsole(this Customer customer) => WriteConsole([customer]);
}
