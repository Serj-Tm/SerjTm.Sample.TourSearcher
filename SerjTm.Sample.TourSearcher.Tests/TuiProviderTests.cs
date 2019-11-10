using Microsoft.Extensions.Configuration;
using SerjTm.Sample.Common.Model;
using static SerjTm.Sample.TourSearcher.Common.Model.TourCategory;
using SerjTm.Sample.TourSearcher.TuiProvider.Services;
using SerjTm.Sample.TourSearcher.TuiProvider.Storages;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Threading;
using Xunit;
using System.Linq;

namespace SerjTm.Sample.TourSearcher.Tests
{
    public class TuiProviderTests
    {
        [Fact]
        public async void Filter()
        {
            var configuration = new ConfigurationBuilder()
                .Build();

            var country = new Country(1, "Russia");
            var city = new City(1, country, "Moscow");
            var hotel = new Hotel(Guid.NewGuid(), "Hilton", "Lenina, 1", city, 2019);
            var tour1 = new Tour(Guid.NewGuid(), "Tui", hotel, "luxe", city, DateTime.Today, DateTime.Today.AddDays(10), DateTime.Today, 10, 100.0m, 100.0m, "S7", 2);
            var tour2 = new Tour(Guid.NewGuid(), "Tui", hotel, "deluxe", city, DateTime.Today, DateTime.Today.AddDays(10), DateTime.Today, 10, 100.0m, 100.0m, "S7", 3);

            var tour_extra_peopleCount = new Tour(Guid.NewGuid(), "Tui", hotel, "deluxe", city, DateTime.Today, DateTime.Today.AddDays(10), DateTime.Today, 10, 100.0m, 100.0m, "S7", 1);
            var tour_extra_minDays = new Tour(Guid.NewGuid(), "Tui", hotel, "deluxe", city, DateTime.Today, DateTime.Today.AddDays(3), DateTime.Today, 3, 100.0m, 100.0m, "S7", 1);
            var tour_extra_maxDays = new Tour(Guid.NewGuid(), "Tui", hotel, "deluxe", city, DateTime.Today, DateTime.Today.AddDays(15), DateTime.Today, 15, 100.0m, 100.0m, "S7", 1);

            var cancellation = new CancellationTokenSource().Token;

            var storage = new MemoryTourStorage(new[] { tour1, tour2, tour_extra_peopleCount, tour_extra_minDays, tour_extra_maxDays }.ToImmutableArray());

            var searcher = new MemorySearchService(storage);

            var filter = DaysRange(5, 12) & StartDate(DateTime.Today);

            var result = await searcher.Search(2, filter, null, cancellation);

            Assert.Equal(new[] { tour1, tour2 }.Select(tour => tour.Id), result.Select(tour => tour.Id));
        }
    }
}
