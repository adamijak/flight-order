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
public class NewModel : PageModel
{
    private readonly ILogger<NewModel> logger;
    private readonly IMongoCollection<Order> orderCollection;


    public NewModel(ILogger<NewModel> logger, IMongoClient mongoClient)
    {
        this.logger = logger;
        orderCollection = mongoClient.GetDatabase("test").GetCollection<Order>("orders"); // TODO ensure all collections are created
    }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public DateOnly BirthDate { get; set;}
    public string? Destination { get; set; }
    public Discount Discount { get; set; }

    public void OnGet()
    {
        logger.LogInformation("Get");
    }

    public Task OnPost()
    {
        
        return orderCollection.InsertOneAsync(new Order
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            BirthDate = BirthDate,
            Destination = Destination,
            Discount = Discount
        });
    }
}
