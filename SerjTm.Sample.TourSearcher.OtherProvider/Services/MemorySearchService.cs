using SerjTm.Sample.Common.Model;
using SerjTm.Sample.Common.Services;
using SerjTm.Sample.TourSearcher.OtherProvider.Storages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SerjTm.Sample.TourSearcher.OtherProvider.Services
{
    public class MemorySearchService : ISearchService
    {
        public MemorySearchService(MemoryTourStorage storage)
        {
            this.Storage = storage;
        }
        private readonly MemoryTourStorage Storage;

        public IEnumerable<Tour> Search(ICity_Id startCity, ICity_Id city, DateTime? startDate, int? minDays, int? maxDays, int? peopleCount, SearchOrder? order)
        {
            return Storage.Tours
                .Where(tour => startCity == null || tour.StartCity.Id == startCity.Id)
                .Where(tour => city == null || tour.Hotel.City.Id == city.Id)
                .Where(tour => startDate == null || startDate <= tour.StartDate)
                .Where(tour => (minDays == null || minDays <= tour.Days) && (maxDays == null || tour.Days <= maxDays))
                .Where(tour => peopleCount == null || peopleCount <= tour.MaxRoomPeopleCount)
                .Select(tour => tour.With(fullPrice: tour.PriceByOnePeople * (peopleCount ?? 1)))
                .OrderBy(order ?? SearchOrder.byName)
                .Take(1000)
                .ToArray();
        }
    }

    public static class TourHlp
    {
        public static IOrderedEnumerable<Tour> OrderBy(this IEnumerable<Tour> items, SearchOrder order)
        {
            switch (order)
            {
                case SearchOrder.byName:
                    return items.OrderBy(item => item.Hotel.Name);
                case SearchOrder.byPrice:
                    return items.OrderBy(item => item.FullPrice);
                case SearchOrder.byPriceDesc:
                    return items.OrderByDescending(item => item.FullPrice);
                case SearchOrder.byDate:
                    return items.OrderBy(item => item.StartDate);
                case SearchOrder.byDateDesc:
                    return items.OrderByDescending(item => item.StartDate);
                default:
                    throw new ArgumentException($"Значение {order} не поддерживается(не реализовано)", "order");
            }
        }
    }
}
