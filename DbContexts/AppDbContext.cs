using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using practice_app.Common;
using practice_app.Entities;
using practice_app.SeedingData;

namespace practice_app.DbContexts;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<DataSeeder> DataSeeder { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public async Task MigrateAsync()
    {
        var dbContext = Dependencies.ServiceProvider.GetRequiredService<AppDbContext>();
        var appsettings = Dependencies.ServiceProvider.GetRequiredService<AppSettings>();
        if (appsettings.DatabaseProvider != DatabaseTypeProvider.InMemory)
            await dbContext.Database.MigrateAsync();
        var seeders = typeof(Program).Assembly.GetTypes().Where(x => x.GetInterfaces().Any(itf => itf == typeof(ISeedingData)));
        if (seeders.Any())
        {
            foreach (var seeder in seeders)
            {
                var instance = (ISeedingData)Activator.CreateInstance(seeder)!;
                var exist = await dbContext.DataSeeder.AnyAsync(x => x.Key.Contains(instance.Key));
                if (exist)
                    continue;

                await instance.DoAsync(dbContext);
                await dbContext.DataSeeder.AddAsync(new DataSeeder()
                {
                    Key = instance.Key
                });
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
