using Serilog;

namespace practice_app;

public static class Program
{
    private static AppDbContext DbContext;
    static Program()
    {
        var serviceProvider = Dependencies.Load();
        DbContext = serviceProvider.InitDbContextAsync().Result;
        AnsiConsole.Clear();
        AnsiConsole.Write(
            new FigletText("EFCore Console").Centered().Color(Color.Blue)
        );
    }

    public static async Task Main(string[] args)
    {
        var products = await DbContext.Products.Where(
            p => p.Description.Contains("performance")
        ).ToListAsync();
        products.WriteConsole();
    }
}
