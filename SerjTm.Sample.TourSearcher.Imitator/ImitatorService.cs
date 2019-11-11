using SerjTm.Sample.TourSearcher.Common.Model;
using SerjTm.Sample.TourSearcher.Imitator.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace SerjTm.Sample.TourSearcher.Imitator
{
    public class ImitatorService
    {
        public static (ImmutableArray<Country> countries, ImmutableArray<City> cities, ImmutableArray<City> flyCities, ImmutableArray<Hotel> hotels) ImitateDict()
        {
            var countries = GenerateCountries(200);
            var cities = GenerateCities(1_000, countries);
            var flyCities = cities.Take(200).ToImmutableArray();//города прилета и вылета
            var hotels = GenerateHotels(1_000, cities);

            return (countries.ToImmutableArray(), cities.ToImmutableArray(), flyCities, hotels.ToImmutableArray());
        }
        public static ImmutableArray<Tour> ImitateTours(string provider,
            (ImmutableArray<Country> countries, ImmutableArray<City> cities, ImmutableArray<City> flyCities, ImmutableArray<Hotel> hotels) props)
        {
            var (_, cities, flyCities, hotels) = props;

            return GenerateTours(1_000_000, provider, cities, flyCities, hotels).ToImmutableArray();
        }

        static Country[] GenerateCountries(int count) => Enumerable.Range(1, count).Select(i => new Country(i, $"Country_{i}")).ToArray();

        static City[] GenerateCities(int count, Country[] countries)
        {
            var rnd = new Random();

            return Enumerable.Range(1, count)
                .Select(i => new City(i, countries.ByRandom(rnd), $"City_{i}"))
                .ToArray();
        }
        static Hotel[] GenerateHotels(int count, City[] cities)
        {
            var rnd = new Random();

            return Enumerable.Range(1, count)
                .Select(i => new Hotel(Guid.NewGuid(), $"Hotel_{i}", $"Address_{i}", cities.ByRandom(rnd), rnd.Next(2000, 2019)))
                .ToArray();
        }
        static Tour[] GenerateTours(int count, string provider, ImmutableArray<City> cities, ImmutableArray<City> flyCities, ImmutableArray<Hotel> hotels)
        {
            var rnd = new Random();

            return Enumerable.Range(1, count)
                .Select(i =>
                {
                    var days = rnd.Next(1, 30);
                    var startDay = rnd.Next(0, 50);
                    var startDate = DateTime.Today.AddDays(startDay);
                    var startHotelDate = startDate.AddDays(rnd.Next(0, 2));
                    var endDate = startHotelDate.AddDays(days);

                    var dayPrice = (decimal)(2_000 + rnd.NextDouble() * 10_000);

                    var priceByOnePeople = Math.Round(dayPrice * days, 2);

                    return new Tour(Guid.NewGuid(), provider, hotels.ByRandom(rnd), RoomKind.All.ByRandom(rnd), flyCities.ByRandom(rnd), startDate, endDate, startHotelDate, days, priceByOnePeople, null, "S7", rnd.Next(1, 6));
                }
                )
                .ToArray();
        }
    }

}
