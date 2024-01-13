using Microsoft.EntityFrameworkCore;
using practice_app.Entities;
using practice_app.SeedingData;

namespace practice_app.DbContexts;

public class AppDbContext: DbContext
{
    public  DbSet<Product> Product { get; set; }
    public  DbSet<DataSeeder> DataSeeder { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(
            "Encrypt=False;TrustServerCertificate=True;Server=localhost;Database=MyDB;User Id=sa;Password=@123",
            config =>
            {
                config.EnableRetryOnFailure(2, TimeSpan.FromSeconds(5), null);
            }
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    
    public static async Task<AppDbContext> GetDbInstance()
    {
        var dbContext = new AppDbContext();
        await dbContext.Database.MigrateAsync();
        var seeders = typeof(Program).Assembly.GetTypes().Where(x => x.GetInterfaces().Any(itf => itf == typeof(ISeedingData)));
        if (seeders.Any())
        {
            foreach (var seeder in seeders)
            {
                var instance = (ISeedingData)Activator.CreateInstance(seeder)!;
                var exist = await dbContext.DataSeeder.AnyAsync(x => x.Key.Contains(instance.Key));
                if(exist)
                    continue;
                
                await instance.DoAsync(dbContext);
                await dbContext.DataSeeder.AddAsync(new DataSeeder()
                {
                    Key = instance.Key 
                });
                await dbContext.SaveChangesAsync();
            }
        }
        return dbContext;
    }
}