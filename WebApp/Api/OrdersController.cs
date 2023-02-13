using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApp.Entities;
using WebApp.Pages;

namespace WebApp.Api;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> logger;
    private readonly IMongoCollection<Order> orderCollection;

    public OrdersController(ILogger<OrdersController> logger, IMongoClient mongoClient)
    {
        this.logger = logger;
        orderCollection = mongoClient.GetDatabase("test").GetCollection<Order>("orders");
    }

    [HttpGet]
    public async Task<IEnumerable<Order>> OnGet(string? search)
    {
        if (search is null)
        {
            return (await orderCollection.FindAsync<Order>(FilterDefinition<Order>.Empty)).ToEnumerable<Order>();
        }

        var regex = BsonRegularExpression.Create(search);
        var builder = Builders<Order>.Filter;

        // TODO fix search, mongodb can not regex search on non string values (o.Flight.DateTime, o.Flight.Price, o.TotalPrice)
        return (await orderCollection.FindAsync<Order>(
            builder.Regex(o => o.Id, regex) |
            builder.Regex(o => o.FirstName, regex) |
            builder.Regex(o => o.LastName, regex) |
            builder.Regex(o => o.Email, regex) |
            builder.Regex(o => o.BirthDate, regex) |
            builder.Regex(o => o.Coupon, regex) |
            builder.Regex(o => o.Discount,regex) |
            builder.Regex(o => o.Flight.From, regex) |
            builder.Regex(o => o.Flight.To, regex) |
            builder.Regex(o => o.Flight.DateTime, regex) |
            builder.Regex(o => o.Flight.Price, regex) |
            builder.Regex(o => o.TotalPrice, regex))).ToEnumerable<Order>();
    }

    [HttpDelete("{id}")]
    public Task OnDelete(string id)
    {
        return orderCollection.DeleteOneAsync(Builders<Order>.Filter.Eq(order => order.Id, id));
    }
}
