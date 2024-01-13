using practice_app.DbContexts;

namespace practice_app.SeedingData;

public interface ISeedingData
{
    string Key { get; }
    public Task DoAsync(AppDbContext context);
}