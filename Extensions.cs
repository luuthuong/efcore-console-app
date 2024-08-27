namespace practice_app;

public static partial class Program
{
    static AppDbContext DBContext;
    static Program()
    {
        var serviceProvider = Dependencies.Load();
        DBContext = serviceProvider.InitDbContextAsync().Result;
        AnsiConsole.Clear();
        AnsiConsole.Write(
            new FigletText("EFCore Console").Centered().Color(Color.Blue)
        );
    }
}


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
