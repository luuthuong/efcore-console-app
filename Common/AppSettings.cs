using System;
using System.Text.Json;

namespace practice_app.Common;

public enum DatabaseTypeProvider
{
    SqlServer = 1,
    InMemory = 2
}

public record DatabaseConfig(
    string ConnectionString, 
    DatabaseTypeProvider Provider, 
    bool UseLazyLoadingProxies
);

public record AppSettings
{
    public DatabaseConfig Database { get; set; }
}

public class AppSettingSetup
{
    private static AppSettings? appSettings;
    public static AppSettings Load(){
        if(appSettings is not null)
            return appSettings;

        var config = JsonFileLoader.Load<AppSettings>("appsettings.json");
        if(config is null)
            throw new InvalidOperationException("Cannot read appsettings.json");
        return config;
    }
}
