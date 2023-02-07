using System;
namespace WebApp.Entities;

public class Flight
{
	public string? Id { get; set; }
	public DateTime? DateTime { get; set; }
	public Destination? From { get; set; }
	public Destination? To { get; set; }
	public int? Price { get; set; }

}

