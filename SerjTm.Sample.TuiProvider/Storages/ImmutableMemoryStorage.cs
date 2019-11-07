using SerjTm.Sample.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerjTm.Sample.TuiProvider.Storages
{
    public class ImmutableMemoryStorage
    {
        public ImmutableMemoryStorage()
        {
            this.Countries = GenerateCountries();
            this.Cities = GenerateCities(this.Countries);
            this.Hotels = GenerateHotels(this.Cities);
            this.Tours = GenerateTours(this.Cities, this.Hotels);
        }

        public readonly Country[] Countries;
        public readonly City[] Cities;
        public readonly Hotel[] Hotels;
        public readonly Tour[] Tours;


        static Country[] GenerateCountries() => Enumerable.Range(1, 200).Select(i => new Country(Guid.NewGuid(), $"Country_{i}")).ToArray();

        static City[] GenerateCities(Country[] countries)
        {
            var rnd = new Random();

            return Enumerable.Range(1, 1_000)
                .Select(i => new City(Guid.NewGuid(), countries.ByRandom(rnd), $"City_{i}"))
                .ToArray();
        }
        static Hotel[] GenerateHotels(City[] cities)
        {
            var rnd = new Random();

            return Enumerable.Range(1, 1_000)
                .Select(i => new Hotel(Guid.NewGuid(), $"Hotel_{i}", $"Address_{i}", cities.ByRandom(rnd), rnd.Next(2000, 2019)))
                .ToArray();
        }
        static Tour[] GenerateTours(City[] cities, Hotel[] hotels)
        {
            var rnd = new Random();

            return Enumerable.Range(1, 1_000_000)
                .Select(i =>
                 {
                     var days = rnd.Next(1, 30);
                     var startDay = rnd.Next(0, 50);
                     var startDate = DateTime.Today.AddDays(startDay);
                     var startHotelDate = startDate.AddDays(rnd.Next(0, 2));
                     var endDate = startHotelDate.AddDays(days);

                     var dayPrice = (decimal)(2_000 + rnd.NextDouble() * 10_000);

                     var priceByOnePeople = Math.Round(dayPrice * days, 2);

                     return new Tour(Guid.NewGuid(), "Tui", hotels.ByRandom(rnd), RoomKind.All.ByRandom(rnd), cities.ByRandom(rnd), startDate, endDate, startHotelDate, days, priceByOnePeople, null, "S7", rnd.Next(1, 6));
                 }
                )
                .ToArray();
        }
    }
    public static class ArrayHlp
    {
        public static T ByRandom<T>(this T[] items, Random rnd) => items[rnd.Next(0, items.Length)];
    }
}
