using SerjTm.Sample.TourSearcher.Common.Model;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace SerjTm.Sample.TourSearcher.Imitator
{
    public class ImitatorService
    {
        public static (ImmutableArray<Country> countries, ImmutableArray<City> cities, ImmutableArray<Hotel> hotels) ImitateDict()
        {
            var countries = GenerateCountries();
            var cities = GenerateCities(countries);
            var hotels = GenerateHotels(cities);

            return (countries.ToImmutableArray(), cities.ToImmutableArray(), hotels.ToImmutableArray());
        }
        public static ImmutableArray<Tour> ImitateTours(string provider,
            (ImmutableArray<Country> countries, ImmutableArray<City> cities, ImmutableArray<Hotel> hotels) props)
        {
            return GenerateTours(provider, props.cities.ToArray(), props.hotels.ToArray()).ToImmutableArray();
        }

        static Country[] GenerateCountries() => Enumerable.Range(1, 200).Select(i => new Country(i, $"Country_{i}")).ToArray();

        static City[] GenerateCities(Country[] countries)
        {
            var rnd = new Random();

            return Enumerable.Range(1, 1_000)
                .Select(i => new City(i, countries.ByRandom(rnd), $"City_{i}"))
                .ToArray();
        }
        static Hotel[] GenerateHotels(City[] cities)
        {
            var rnd = new Random();

            return Enumerable.Range(1, 1_000)
                .Select(i => new Hotel(Guid.NewGuid(), $"Hotel_{i}", $"Address_{i}", cities.ByRandom(rnd), rnd.Next(2000, 2019)))
                .ToArray();
        }
        static Tour[] GenerateTours(string provider, City[] cities, Hotel[] hotels)
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

                    return new Tour(Guid.NewGuid(), provider, hotels.ByRandom(rnd), RoomKind.All.ByRandom(rnd), cities.ByRandom(rnd), startDate, endDate, startHotelDate, days, priceByOnePeople, null, "S7", rnd.Next(1, 6));
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
