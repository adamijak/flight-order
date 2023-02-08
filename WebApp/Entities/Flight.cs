using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApp.Entities;

public class Flight
{
	public string? Id { get; set; }
	public DateTime? DateTime { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Destination? From { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Destination? To { get; set; }
	public int Price { get; set; }

}

