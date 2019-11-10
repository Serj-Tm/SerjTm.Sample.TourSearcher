using Microsoft.Extensions.Configuration;
using NitroBolt.Functional;
using SerjTm.Sample.Common.Model;
using SerjTm.Sample.Common.Services;
using SerjTm.Sample.TourSearcher.Common.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SerjTm.Sample.TourSearcher.Aggregator
{
    public class AggregatorService : ISearchService
    {
        public AggregatorService(IEnumerable<ISearchService> searchers, IConfiguration configuration)
        {
            this.searchers = searchers;
            this.SearchTimeout = TimeSpan.FromSeconds(configuration.GetValue<double>("Aggregator:Search:Timeout", 15));

            var priorityConfig = new PriorityProviderConfig();
            configuration.GetSection("Aggregator:PriorityProvider").Bind(priorityConfig);
            this.PriorityProviderConfig = priorityConfig;
        }
        readonly IEnumerable<ISearchService> searchers;
        readonly TimeSpan SearchTimeout;
        readonly PriorityProviderConfig PriorityProviderConfig;

        public async Task<IEnumerable<Tour>> Search(ICity_Id startCity, ICity_Id city, DateTime? startDate, int? minDays, int? maxDays, int? peopleCount, SearchOrder? order, CancellationToken token)
        {
            var cancelSource = CancellationTokenSource.CreateLinkedTokenSource(token);

            var searchTasks = searchers.Select(searcher => Task.Run(() => searcher.Search(startCity, city, startDate, minDays, maxDays, peopleCount, order, cancelSource.Token))).ToArray();
            await Task.WhenAny(Task.WhenAll(searchTasks), Task.Delay(SearchTimeout));

            cancelSource.Cancel();

            var answers = searchTasks.Where(task => task.IsCompleted).Select(task => task.Result);
            if (!answers.Any())
                throw new TimeoutException($"Ни один из ISearchService-ов не ответил за время {SearchTimeout})");

            return answers.SelectMany(answer => answer)
                .GroupBy(tour => new { HotelId = tour.Hotel.Id, StartCityId = tour.StartCity.Id, tour.StartDate, tour.StartHotelDate, tour.Days, tour.RoomKind })
                .Select(group => MinPriceTour(group))
                .OrderBy(order)
                .ToArray();
        }
        Tour MinPriceTour(IEnumerable<Tour> tours)
        {
            var minTour = tours.MinObject(tour => tour.FullPrice);
            if (this.PriorityProviderConfig?.Name != null)
            {
                var minPriorityTour = tours.Where(tour => tour.Provider == PriorityProviderConfig.Name).MinObject(tour => tour.FullPrice);
                if (minPriorityTour != null && minPriorityTour.FullPrice <= minTour.FullPrice * (1m + 0.01m * PriorityProviderConfig.ExtraPricePercent))
                    return minPriorityTour;
            }
            return minTour;
        }
    }

    public class PriorityProviderConfig
    {
        public string Name { get; set; } = null;
        public decimal ExtraPricePercent { get; set; } = 5;
    }
}
