using System;
using System.Text.Json;
using WebApp.Entities;

namespace WebApp.Services
{
	public class FlightService
	{
        private const int flightCount = 1000;
        private readonly Random random = new Random();
        private readonly string path;

        public IEnumerable<Flight> Flights { get; }

        public FlightService(string path)
		{
            this.path = path;
            using var stream = System.IO.File.OpenRead(path);
            var values = JsonSerializer.Deserialize<Flight[]>(stream);
            if (values is null)
            {
                throw new NullReferenceException();
            }
            Flights = values;
        }

        public Flight? GetFlight(string? id) => Flights.FirstOrDefault(i => string.Equals(i.Id, id, StringComparison.InvariantCultureIgnoreCase));

        public void GenerateDataFile()
        {
            var dateTime = new DateTime(2023, 1, 1);
            var offset = (long)(dateTime.AddMonths(1) - dateTime).TotalMinutes;

            var destinations = Enum.GetValues(typeof(Destination));
            var values = new List<Flight>(flightCount);

            for (var i = 0; i < flightCount; i++)
            {
                values.Add(new Flight
                {
                    Id = Guid.NewGuid().ToString(),
                    From = Destination.Prague,
                    To = (Destination)random.Next(1, destinations.Length),
                    DateTime = dateTime.AddMinutes(random.NextInt64(0, offset)),
                    Price = random.Next(100, 500),
                });
            }

            using var stream = System.IO.File.OpenWrite(path);
            JsonSerializer.Serialize(stream, values);
        }
	}
}

