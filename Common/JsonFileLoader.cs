using System;
using System.Text.Json;

namespace practice_app.Common;

public class JsonFileLoader
{
    public static T? Load<T>(string path) where T : class
    {
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions(){
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
    }
}
