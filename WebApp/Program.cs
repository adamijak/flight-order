using System.Text.Json;
using System.Text.Json.Serialization;
using Common;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });


BsonSerializer.RegisterSerializer(new DateOnlySerializer());
builder.Services.AddSingleton<IMongoClient>(new MongoClient("mongodb://db:27017/test"));
builder.Services.AddSingleton(new FlightService("Data/Flights.json"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();

