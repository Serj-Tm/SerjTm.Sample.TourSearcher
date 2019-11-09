using SerjTm.Sample.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SerjTm.Sample.Common.Services
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
        IEnumerable<Tour> Search(ICity_Id startCity, ICity_Id city, DateTime? startDate, int? minDays, int? maxDays, int? peopleCount, SearchOrder? order);
    }
}
