using System;
using WebApp.Entities;

namespace WebAppTest.Unit.FlightService;

[TestClass]
public class General
{
    private static WebApp.Services.FlightService flightService = default!;

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        flightService = new("Data/Flights.json");
    }


    [TestMethod]
    public void FlightsAreNotEmpty()
    {
        Assert.IsTrue(flightService.Flights.Any());
    }

    [TestMethod]
    public void FlightNotFound()
    {
        var flight = flightService.GetFlight("bad-id");
        Assert.AreEqual(null, flight);
    }

    [TestMethod]
    public void FlightFound()
    {
        var flight = flightService.GetFlight("89b5161b-1c9e-4c4b-9061-16e831ec0d95");
        if(flight is null)
        {
            throw new Exception("Flight could not be found.");
        }

        Assert.AreEqual("89b5161b-1c9e-4c4b-9061-16e831ec0d95", flight.Id);
        Assert.AreEqual(new DateTime(2023, 01, 01, 08, 12, 00), flight.DateTime);
        Assert.AreEqual(Destination.Sydney, flight.From);
        Assert.AreEqual(Destination.NewYork, flight.To);
        Assert.AreEqual(416, flight.Price);
    }
}

