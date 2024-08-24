using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using practice_app.Common;
using practice_app.DbContexts;
using practice_app.Entities;

namespace practice_app;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var serviceProvider = Dependencies.Load();
        var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
        await dbContext.MigrateAsync();

        var products = await dbContext.Products.ToListAsync();
        products.WriteConsole();
    }
}
