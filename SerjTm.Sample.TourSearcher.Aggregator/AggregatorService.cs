using NitroBolt.Functional;
using SerjTm.Sample.Common.Model;
using SerjTm.Sample.Common.Services;
using SerjTm.Sample.TourSearcher.Common.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SerjTm.Sample.TourSearcher.Aggregator
{
    public class AggregatorService : ISearchService
    {
        public AggregatorService(IEnumerable<ISearchService> searchers)
        {
            this.searchers = searchers;
        }
        IEnumerable<ISearchService> searchers;

        public IEnumerable<Tour> Search(ICity_Id startCity, ICity_Id city, DateTime? startDate, int? minDays, int? maxDays, int? peopleCount, SearchOrder? order)
        {
            return searchers.SelectMany(searcher => searcher.Search(startCity, city, startDate, minDays, maxDays, peopleCount, order))
                .GroupBy(tour => new { HotelId = tour.Hotel.Id, StartCityId = tour.StartCity.Id, tour.StartDate, tour.StartHotelDate, tour.Days, tour.RoomKind })
                .Select(group => group.MinObject(tour => tour.FullPrice))
                .OrderBy(order);
        }
    }
}
