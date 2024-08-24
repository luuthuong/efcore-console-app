using System;
using practice_app.Common;
using Spectre.Console;

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
        TableConsole.Print(
            ["Id", "FirstName", "LastName", "Address"],
            customers,
            (c) => [c.Id.ToString(), c.FirstName, c.LastName, c.Address]
        );
    }
}
