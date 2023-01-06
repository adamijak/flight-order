using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApp.Entities;

public class Order
{
    public string? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public DateOnly BirthDate { get; set; }
    //TODO use enum
    public string? Destination { get; set; }
    //TODO use enum
    [BsonRepresentation(BsonType.String)]
    public Discount Discount { get; set; }
}

