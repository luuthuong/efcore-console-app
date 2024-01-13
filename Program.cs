using Microsoft.EntityFrameworkCore;
using practice_app.DbContexts;

namespace practice_app;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var context = await AppDbContext.GetDbInstance();
    }
}