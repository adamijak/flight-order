using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Entities;
using WebApp.Services;

namespace WebApp.Api;

[Route("api/[controller]")]
[ApiController]
public class FlightsController : ControllerBase
{
    private readonly FlightService flightService;

    public FlightsController(FlightService flightService)
    {
        this.flightService = flightService;
    }

    [HttpGet]
    public IEnumerable<Flight> Get([FromQuery]Destination? destination, [FromQuery]DateTime dateTime)
    {
        if (destination is null && dateTime == default)
        {
            return Array.Empty<Flight>();
        }

        return flightService.Flights.Where(i => (i.To == destination) && (dateTime.Date <= i.DateTime && i.DateTime <= dateTime.Date.AddDays(1)));
    }

    [HttpGet("{id}")]
    public ActionResult<Flight> Get(string? id)
    {
        var flight = flightService.GetFlight(id);
        if (flight is null)
        {
            return NotFound();
        }

        return flight;
    }
}

