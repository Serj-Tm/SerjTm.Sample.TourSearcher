using Microsoft.Extensions.Configuration;
using Moq;
using SerjTm.Sample.Common.Model;
using SerjTm.Sample.Common.Services;
using SerjTm.Sample.TourSearcher.Aggregator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SerjTm.Sample.TourSearcher.Tests
{
    public class AggregatorTests
    {
        [Fact]
        public async void TestIgnoreSlowSearcher()
        {
            var config = new Dictionary<string, string>
            {
                {"Aggregator:Search:Timeout", "0.3"},
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(config)
                .Build();

            var country = new Country(1, "Russia");
            var city = new City(1, country, "Moscow");
            var hotel = new Hotel(Guid.NewGuid(), "Hilton", "Lenina, 1", city, 2019);
            var tour1 = new Tour(Guid.NewGuid(), "Tui", hotel, "luxe", city, DateTime.Today, DateTime.Today.AddDays(10), DateTime.Today, 10, 100.0m, null, "S7", 2);
            var tour2 = new Tour(Guid.NewGuid(), "Tui", hotel, "deluxe", city, DateTime.Today, DateTime.Today.AddDays(10), DateTime.Today, 10, 100.0m, null, "S7", 2);

            var cancellation = new CancellationTokenSource().Token;

            var searcher1 = new Mock<ISearchService>();
            searcher1.Setup(mock => mock.Search(null, null, null, null, null, null, null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new[] {tour1}, TimeSpan.FromSeconds(0.25));
            var searcher2 = new Mock<ISearchService>();
            searcher2.Setup(mock => mock.Search(null, null, null, null, null, null, null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new[] { tour1, tour2 }, TimeSpan.FromSeconds(0.25));
            var slowSearcher1 = new Mock<ISearchService>();
            slowSearcher1.Setup(mock => mock.Search(null, null, null, null, null, null, null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new[] { tour1, tour2 }, TimeSpan.FromSeconds(3));

            var aggregator = new AggregatorService(new[] { searcher1.Object, searcher2.Object, slowSearcher1.Object }, configuration);

            var watch = new Stopwatch();
            watch.Start();

            var result = await aggregator.Search(null, null, null, null, null, null, null, cancellation);

            Assert.Equal(new[] { tour1, tour2 }, result);
            Assert.InRange(watch.Elapsed.TotalSeconds, 0.3, 0.4);
        }
    }
}
