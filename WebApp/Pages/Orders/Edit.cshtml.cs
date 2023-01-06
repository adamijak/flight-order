using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using WebApp.Entities;

namespace WebApp.Pages.Orders;

[BindProperties]
public class EditModel : PageModel
{
    private readonly ILogger<EditModel> logger;
    private readonly IMongoCollection<Order> orderCollection;

    public EditModel(ILogger<EditModel> logger, IMongoClient mongoClient)
    {
        this.logger = logger;
        orderCollection = mongoClient.GetDatabase("test").GetCollection<Order>("orders");
    }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public DateOnly BirthDate { get; set; }
    public string? Destination { get; set; }
    public Discount Discount { get; set; }

    public async Task OnGet(string id)
    {
        var order = await (await orderCollection.FindAsync<Order>(Builders<Order>.Filter.Eq(o => o.Id, id))).SingleAsync();
        FirstName = order.FirstName;
        LastName = order.LastName;
        Email = order.Email;
        BirthDate = order.BirthDate;
        Destination = order.Destination;
        Discount = order.Discount;
    }

    public async Task<IActionResult> OnPost(string id)
    {
        var order = new Order
        {
            Id = id,
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            BirthDate = BirthDate,
            Destination = Destination,
            Discount = Discount,
        };
        await orderCollection.ReplaceOneAsync<Order>(o => o.Id == id, order);
        return RedirectToPage("/Orders/Index"); 
    }
}
