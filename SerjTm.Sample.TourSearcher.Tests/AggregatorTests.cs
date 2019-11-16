using Microsoft.Extensions.Configuration;
using Moq;
using SerjTm.Sample.TourSearcher.Common.Model;
using static SerjTm.Sample.TourSearcher.Common.Model.TourCategory;
using SerjTm.Sample.TourSearcher.Common.Services;
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
        public async void IgnoreSlowSearcher()
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
            var tour1 = new Tour(Guid.NewGuid(), "Tui", hotel, "luxe", city, DateTime.Today, DateTime.Today.AddDays(10), DateTime.Today, 10, 100.0m, 100.0m, "S7", 2);
            var tour2 = new Tour(Guid.NewGuid(), "Tui", hotel, "deluxe", city, DateTime.Today, DateTime.Today.AddDays(10), DateTime.Today, 10, 100.0m, 100.0m, "S7", 2);

            var searcher1 = new Mock<ISearchService>();
            searcher1.Setup(mock => mock.Search(null, null, null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new[] {tour1}, TimeSpan.FromSeconds(0.25));
            var searcher2 = new Mock<ISearchService>();
            searcher2.Setup(mock => mock.Search(null, null, null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new[] { tour1, tour2 }, TimeSpan.FromSeconds(0.25));
            var slowSearcher1 = new Mock<ISearchService>();
            slowSearcher1.Setup(mock => mock.Search(null, null, null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new[] { tour1, tour2 }, TimeSpan.FromSeconds(3));

            var aggregator = new AggregatorService(new[] { searcher1.Object, searcher2.Object, slowSearcher1.Object }, configuration);

            var watch = new Stopwatch();
            watch.Start();

            var result = await aggregator.Search(null, null, null);

            Assert.Equal(new[] { tour1, tour2 }, result);
            Assert.InRange(watch.Elapsed.TotalSeconds, 0.3, 0.4);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(20)]
        public async void PriorityProviderWithExtraPrice(int extraPricePercent)
        {

            var config = new Dictionary<string, string>
            {
                {"Aggregator:PriorityProvider:Name", "Tui"},
                {"Aggregator:PriorityProvider:ExtraPricePercent", extraPricePercent.ToString()},
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(config)
                .Build();

            var country = new Country(1, "Russia");
            var city = new City(1, country, "Moscow");
            var hotel = new Hotel(Guid.NewGuid(), "Hilton", "Lenina, 1", city, 2019);
            var lowExtraPrice = 4m;
            var tour_other_lowExtraPrice = new Tour(Guid.NewGuid(), "Other", hotel, "luxe", city, DateTime.Today, DateTime.Today.AddDays(10), DateTime.Today, 10, 100.0m, 100.0m, "S7", 2);
            var tour_tui_lowExtraPrice = tour_other_lowExtraPrice.With(provider:"Tui", fullPrice: tour_other_lowExtraPrice.FullPrice * (1 + 0.01m*lowExtraPrice));
            var highExtraPrice = 10m;
            var tour_other_highExtraPrice = new Tour(Guid.NewGuid(), "Other", hotel, "deluxe", city, DateTime.Today, DateTime.Today.AddDays(10), DateTime.Today, 10, 100.0m, 100.0m, "S7", 2);
            var tour_tui_highExtraPrice = tour_other_highExtraPrice.With(provider:"Tui", fullPrice: tour_other_highExtraPrice.FullPrice * (1 +  0.01m * highExtraPrice));

            var searcher1 = new Mock<ISearchService>();
            searcher1.Setup(mock => mock.Search(null, null, null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new[] { tour_other_lowExtraPrice, tour_other_highExtraPrice });
            var searcher2 = new Mock<ISearchService>();
            searcher2.Setup(mock => mock.Search(null, null, null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new[] { tour_tui_lowExtraPrice, tour_tui_highExtraPrice });

            var aggregator = new AggregatorService(new[] { searcher1.Object, searcher2.Object}, configuration);

            var result = await aggregator.Search(null, null, null);

            Assert.Equal(new[] 
                {
                  lowExtraPrice < extraPricePercent ?  tour_tui_lowExtraPrice : tour_other_lowExtraPrice,
                  highExtraPrice < extraPricePercent ?  tour_tui_highExtraPrice : tour_other_highExtraPrice
                }, 
                result);
        }



    }
}
