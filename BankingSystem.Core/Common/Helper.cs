using System.Text.Json;

namespace BankingSystem.Core.Common;

public static class Helper
{
    public static string ToJson(this Object obj)
    {
        var json  = JsonSerializer.Serialize(obj);
        return json;
    }
    public static T FromJson<T>(this string obj)
    {
        var res = JsonSerializer.Deserialize<T>(obj);
        return res;
    }
}
