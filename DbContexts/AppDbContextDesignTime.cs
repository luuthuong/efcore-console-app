using System;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;
using Serilog;

namespace practice_app.DbContexts;

public class AppDbContextDesignTime : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        return new AppDbContext(
            new DbContextOptionsBuilder<AppDbContext>()
            .UseLoggerFactory(new LoggerFactory().AddSerilog())
            .ConfigureEFProvider().Options
        );
    }
}
