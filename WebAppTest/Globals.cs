using System;
using Common;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace WebAppTest;

[TestClass]
public static class Globals
{
    public static HttpClient HttpClient = default!;
    public static MongoClient MongoClient = default!;

    [AssemblyInitialize]
    public static void AssemblyInitialize(TestContext context)
    {
        HttpClient = new();
        BsonSerializer.RegisterSerializer(new DateOnlySerializer());
        MongoClient = new("mongodb://db:27017/test");
    }

    [AssemblyCleanup]
    public static void AssemblyCleanup()
    {
        HttpClient.Dispose();
    }
}

