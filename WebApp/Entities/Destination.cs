using System;
namespace WebApp.Entities;

public enum Destination
{
    Prague, // Prague has to be first since it is considered as default origin destination. Check FlightsController.cs
    Krakow,
    NewYork,
    Sydney,
    SanFrancisco,
}

