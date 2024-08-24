using System;
using practice_app.Common;
using practice_app.DbContexts;
using practice_app.Entities;

namespace practice_app.SeedingData;

class CustomerSeedingItem
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Address { get; set; }
}

public class CustomerSeeding : ISeedingData
{
    public string Key => "1_customer_table";

    public async Task DoAsync(AppDbContext context)
    {
        var customerData = JsonFileLoader.Load<CustomerSeedingItem[]>("customers.json");
        if (customerData is null)
            return;
        var customers = customerData.Select(x => new Customer()
        {
            FirstName = x.FirstName,
            LastName = x.LastName,
            Address = x.Address,
        });
        customers = customers.Select(x =>{
            x.Invoices = [
                new(){
                    CustomerId = x.Id,
                    Description = "1. Invoice for " + x.FirstName + " " + x.LastName,
                    Date = DateTime.Now
                }
            ];
            return x;
        });
        await context.Customers.AddRangeAsync(customers);
        await context.SaveChangesAsync();
    }
}
