using Microsoft.Extensions.Logging;
using Serilog;

namespace practice_app;

public static class Dependencies
{
    private static IServiceCollection services = default!;

    public static IServiceProvider ServiceProvider => services?.BuildServiceProvider() ?? throw new InvalidOperationException("Services not initialized");

    public static IServiceProvider Load()
    {
        Initialize();
        services.AddSingleton(AppSettingSetup.Load());
        ConfigureSerilog();
        ConfigureDbContext();
        return services!.BuildServiceProvider();
    }

    private static void Initialize()
    {
        if (services is null)
            services = new ServiceCollection();
        services.Clear();
    }

    private static void ConfigureDbContext()
    {
        services.AddDbContext<AppDbContext>(
           options =>
           {
               var settings = AppSettingSetup.Load();
               options.ConfigureEFProvider();
               options.UseLoggerFactory(new LoggerFactory().AddSerilog());
               if (settings.Database.UseLazyLoadingProxies)
               {
                   TextConsole.WriteLine("LazyLoading proxy is enabled");
                   options.UseLazyLoadingProxies();
               }
           });
    }

    private static void ConfigureSerilog()
    {
        var logger = new LoggerConfiguration();
        logger.WriteTo.Console();
        Log.Logger = logger.CreateLogger();
        services.AddSingleton(Log.Logger);
    }
}
