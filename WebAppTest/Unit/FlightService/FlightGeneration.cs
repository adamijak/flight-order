using System;
using WebApp.Services;
using WebApp.Entities;

namespace WebAppTest.Unit.FlightService;

[TestClass]
public class FlightGeneration
{
    private static WebApp.Services.FlightService flightService = default!;
    private static DateTime startDateTime = new(2023, 1, 1);
    private static int monthOffset = 1;
    private static int flightCount = 1000;
    private static int priceMin = 100;
    private static int priceMax = 500;

    private static DateTime endDateTime;

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        endDateTime = startDateTime.AddMonths(monthOffset);
        WebApp.Services.FlightService.GenerateDataFile("Data/GeneratedFlights.json", startDateTime, monthOffset, flightCount, priceMin, priceMax);
        flightService = new WebApp.Services.FlightService("Data/GeneratedFlights.json");
    }

    [TestMethod]
    public void FlightCount()
    {
        Assert.AreEqual(flightCount, flightService.Flights.Count());
    }

    [TestMethod]
    public void DestinationsNotSame()
    {
        Assert.IsTrue(flightService.Flights.All(f => f.From != f.To));
    }

    [TestMethod]
    public void DateTimeInRange()
    {
        Assert.IsTrue(flightService.Flights.All(f => startDateTime <= f.DateTime && f.DateTime < endDateTime));
    }

    [TestMethod]
    public void PriceInRange()
    {
        Assert.IsTrue(flightService.Flights.All(f => priceMin <= f.Price && f.Price < priceMax));
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
        File.Delete("Data/GeneratedFlights.json");
    }
}

