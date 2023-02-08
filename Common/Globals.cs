using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common;

public static class Globals
{
    public static readonly Dictionary<string, double> AcceptedCoupons = new()
    {
        ["BLACK-FRIDAY20"] = 0.8,
        ["CHRISTMAS10"] = 0.9,
    };

    public static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        Converters = { new JsonStringEnumConverter() },
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
}

