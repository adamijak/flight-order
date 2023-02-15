using System;
using System.Text.Json;
using Common;
using WebApp.Entities;
using WebAppTest.Entities;

namespace WebAppTest.Unit;

[TestClass]
public class JsonSerializer
{
    [TestMethod]
    public void GlobalOptionsSerialization()
    {
        // Arrange
        var expected = "{\"id\":null,\"dummyEnum\":\"Two\"}";
        var obj = new DummyClass{ DummyEnum = DummyEnum.Two };

        // Act
        var actual = System.Text.Json.JsonSerializer.Serialize(obj, Globals.JsonSerializerOptions);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GlobalOptionsDeserialization()
    {
        // Arrange
        var expected = new DummyClass { DummyEnum = DummyEnum.Two };
        var json = "{\"dummyEnum\":\"Two\"}";

        // Act
        var actual = System.Text.Json.JsonSerializer.Deserialize<DummyClass>(json, Globals.JsonSerializerOptions);

        // Assert
        Assert.AreEqual(expected.Id, actual.Id);
        Assert.AreEqual(expected.DummyEnum, actual.DummyEnum);
    }
}

