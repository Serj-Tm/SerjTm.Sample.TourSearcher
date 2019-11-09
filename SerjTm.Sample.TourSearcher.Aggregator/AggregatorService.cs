using SerjTm.Sample.Common.Model;
using SerjTm.Sample.Common.Services;
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
            //TODO делать поиск и merge по всем поисковикам
            var searcher = searchers.First();
            return searcher.Search(startCity, city, startDate, minDays, maxDays, peopleCount, order);
        }
    }
}
