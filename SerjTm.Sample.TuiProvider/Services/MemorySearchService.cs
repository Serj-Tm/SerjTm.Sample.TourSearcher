using SerjTm.Sample.Common.Model;
using SerjTm.Sample.Common.Services;
using SerjTm.Sample.TourSearcher.Common.Specifications;
using SerjTm.Sample.TuiProvider.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SerjTm.Sample.TuiProvider.Services
{
    public class MemorySearchService : ISearchService
    {
        public MemorySearchService(MemoryTourStorage storage)
        {
            this.Storage = storage;
        }
        private readonly MemoryTourStorage Storage;

        public async Task<IEnumerable<Tour>> Search(ICity_Id startCity, ICity_Id city, DateTime? startDate, int? minDays, int? maxDays, int? peopleCount, SearchOrder? order, CancellationToken token)
        {
            await Task.Delay(TimeSpan.FromSeconds(3 + new Random().NextDouble() * 14), token);

            return Storage.Tours
                .Where(tour => startCity == null || tour.StartCity.Id == startCity.Id)
                .Where(tour => city == null || tour.Hotel.City.Id == city.Id)
                .Where(tour => startDate == null || startDate <= tour.StartDate)
                .Where(tour => (minDays == null || minDays <= tour.Days) && (maxDays == null || tour.Days <= maxDays))
                .Where(tour => peopleCount == null || peopleCount <= tour.MaxRoomPeopleCount)
                .Select(tour => tour.With(fullPrice: tour.PriceByOnePeople * (peopleCount ?? 1)))
                .OrderBy(order)
                .Take(1000)
                .ToArray();
        }
    }

}
