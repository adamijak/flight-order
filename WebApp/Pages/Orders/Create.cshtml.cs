using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using WebApp.Api;
using WebApp.Entities;
using WebApp.Services;

namespace WebApp.Pages.Orders;

public class CreateModel : PageModel
{
    private readonly ILogger<CreateModel> logger;
    private readonly FlightService flightService;
    private readonly IMongoCollection<Order> orderCollection;

    public CreateModel(ILogger<CreateModel> logger, FlightService flightService, IMongoClient mongoClient)
    {
        this.logger = logger;
        this.flightService = flightService;
        orderCollection = mongoClient.GetDatabase("test").GetCollection<Order>("orders"); // TODO ensure all collections are created
    }

    [BindProperty]
    public Models.Order Order { get; set; } = default!;

    public void OnGet()
    {
        logger.LogInformation("Get");
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var flight = flightService.GetFlight(Order.FlightId);
        if(flight is null)
        {
            throw new Exception();
        }


        var order = new Order
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = Order.FirstName,
            LastName = Order.LastName,
            Email = Order.Email,
            BirthDate = Order.BirthDate.Value,
            Coupon = Order.Coupon,
            Discount = Order.Discount,
            Flight = flight,
        };

        if (Order.Coupon is not null)
        {
            order.TotalPrice = (int)(flight.Price * Globals.AcceptedCoupons[Order.Coupon]);
        }

        if (Order.Discount != Discount.None)
        {
            order.TotalPrice = (int)(order.TotalPrice * 0.7);
        }

        await orderCollection.InsertOneAsync(order);
        return RedirectToPage("/Index");
    }
}
