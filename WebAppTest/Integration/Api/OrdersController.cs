using System;
using System.Net;
using Common;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using WebApp.Entities;

namespace WebAppTest.Integration.Api;

[TestClass]
public class OrdersController
{
    private const string uri = "http://webapp/Api/Orders/";
    private static IMongoCollection<Order> orderCollection = default!;

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        orderCollection = Globals.MongoClient.GetDatabase("test").GetCollection<Order>("orders");
    }


    [TestMethod]
    public async Task DeleteOrder()
    {
        // Arrange
        var order = new Order { Id = Guid.NewGuid().ToString() };
        await orderCollection.InsertOneAsync(order);

        // Act
        var response = await Globals.HttpClient.DeleteAsync($"{uri}{order.Id}");
        var actual = await orderCollection.FindAsync(Builders<Order>.Filter.Eq(o => o.Id, order.Id));

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.AreEqual(0, actual.ToList().Count);
    }
}

