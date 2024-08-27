namespace practice_app.DbContexts;

public static class DbContextExtension
{
    public static DbContextOptionsBuilder ConfigureEFProvider(this DbContextOptionsBuilder builder)
    {
        var appsettings = AppSettingSetup.Load();
        switch (appsettings.Database.Provider)
        {
            case DatabaseTypeProvider.SqlServer:
                TextConsole.WriteLine("Using Sql Server");
                builder.UseSqlServer(
                    appsettings.Database.ConnectionString,
                    sqlOptions =>{
                        sqlOptions.CommandTimeout(3_000);
                    }
                );
                break;
            case DatabaseTypeProvider.InMemory:
                TextConsole.WriteLine("Using InMemory Database");
                builder.UseInMemoryDatabase("EFCoreConsoleDB");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        return builder;
    }
    public static async Task MigrateDataAsync(this AppDbContext dbContext)
    {
        var seeders = typeof(Program).Assembly.DefinedTypes.Where(x => x.GetInterfaces().Any(itf => itf == typeof(ISeedingData)));
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
