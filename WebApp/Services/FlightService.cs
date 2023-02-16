using System;
using System.Text.Json;
using Common;
using WebApp.Entities;

namespace WebApp.Services
{
	public class FlightService
	{
        private static readonly Random random = new();

        public IEnumerable<Flight> Flights { get; }

        public FlightService(string path)
		{
            using var stream = System.IO.File.OpenRead(path);
            var values = JsonSerializer.Deserialize<Flight[]>(stream, Globals.JsonSerializerOptions);
            if (values is null)
            {
                throw new NullReferenceException();
            }
            Flights = values;
        }

        public Flight? GetFlight(string? id) => Flights.FirstOrDefault(i => string.Equals(i.Id, id, StringComparison.InvariantCultureIgnoreCase));

        public static void GenerateDataFile(string path, DateTime startDateTime, int monthOffset, int flightCount = 1000, int priceMin = 100, int priceMax = 500)
        {
            var offset = (long)(startDateTime.AddMonths(monthOffset) - startDateTime).TotalMinutes;

            var destinations = Enum.GetValues(typeof(Destination));
            var values = new List<Flight>(flightCount);

            for (var i = 0; i < flightCount; i++)
            {
                var from = random.Next(destinations.Length);
                var to = (from + 1 + random.Next(destinations.Length-1))%destinations.Length;
                values.Add(new Flight
                {
                    Id = Guid.NewGuid().ToString(),
                    From = (Destination) from,
                    To = (Destination) to,
                    DateTime = startDateTime.AddMinutes(random.NextInt64(0, offset)),
                    Price = random.Next(priceMin, priceMax),
                });
            }

            using var stream = System.IO.File.Open(path, FileMode.Create);
            JsonSerializer.Serialize(stream, values, Globals.JsonSerializerOptions);
        }
	}
}

