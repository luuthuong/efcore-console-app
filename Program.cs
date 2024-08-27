using Serilog;
using EFCore.BulkExtensions;

namespace practice_app;

public static partial class Program
{

    public static async Task Main(string[] args)
    {
        await TextConsole.WrapPrintTimeExecution(
            [
                ("[BulkExtensions] Inserting 1.000.000 products", InsertMillionRecordsUsingBulkExtensions),
                ("[Default EF Core] Inserting 1.000.000 products", InsertMillionRecorsUsingDefaultEfCore)
            ]
        );
    }

    public static Task InsertMillionRecordsUsingBulkExtensions()
    {
        var products = Enumerable.Range(1, 1_000_000).Select(x => Product.Create($"Product {x}", $"Product {x} description")).ToList();
        return DBContext.BulkInsertAsync(products, config =>
        {
            config.BatchSize = 1000;
        });
    }

    public static async Task InsertMillionRecorsUsingDefaultEfCore()
    {
        var products = Enumerable.Range(1, 1_000_000).Select(x => Product.Create($"Product {x}", $"Product {x} description")).ToList();
        await DBContext.Products.AddRangeAsync(products);
        await DBContext.SaveChangesAsync();
    }
}
