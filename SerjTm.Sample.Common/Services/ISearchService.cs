using SerjTm.Sample.TourSearcher.Common.Model;
using SerjTm.Sample.TourSearcher.Common.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SerjTm.Sample.TourSearcher.Common.Services
{
    public enum SearchOrder
    {
        byPrice,
        byPriceDesc,
        byName,
        byDate,
        byDateDesc
    }

    public interface ISearchService
    {
        Task<IEnumerable<Tour>> Search(int? peopleCount, FilterSpecification<Tour> filter, SearchOrder? order, CancellationToken token);
    }
}
