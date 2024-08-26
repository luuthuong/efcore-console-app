using System;

namespace practice_app;

public static class Extensions
{
    public static async Task<AppDbContext> InitDbContextAsync(this IServiceProvider services){
        var appSettings = services.GetRequiredService<AppSettings>();
        var dbContext = services.GetRequiredService<AppDbContext>();
        if(appSettings.Database.Provider != DatabaseTypeProvider.InMemory)
            await dbContext.Database.MigrateAsync();
        await dbContext.MigrateDataAsync();
        return dbContext;
    }
}
