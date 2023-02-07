using System;
namespace Common;

public static class Globals
{
    public static readonly Dictionary<string, double> AcceptedCoupons = new()
    {
        ["BLACK-FRIDAY20"] = 0.8,
        ["CHRISTMAS10"] = 0.9,
    };
}

