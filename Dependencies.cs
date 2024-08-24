using Castle.DynamicProxy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using practice_app.Common;
using practice_app.DbContexts;

namespace practice_app;

public static class Dependencies
{
    private static IServiceCollection _services = default!;

    public static IServiceProvider ServiceProvider => _services?.BuildServiceProvider() ?? throw new InvalidOperationException("Services not initialized");

    public static IServiceProvider Load()
    {
        Initialize();
        ConfigureDbContext();
        _services.AddSingleton(AppSettingSetup.Load());
        return _services!.BuildServiceProvider();
    }

    private static void Initialize()
    {
        if (_services is null)
            _services = new ServiceCollection();
        _services.Clear();
    }

    private static void ConfigureDbContext()
    {
        _services.AddDbContext<AppDbContext>(
           options =>
           {
               options.UseInMemoryDatabase("MyDb");
               var settings = AppSettingSetup.Load();
               if (settings.UseLazyLoadingProxies)
               {
                   Console.WriteLine("LazyLoading proxy is enabled");
                   options.UseLazyLoadingProxies();
               }
           });
    }
}
