using SerjTm.Sample.TourSearcher.Common.Model;
using SerjTm.Sample.TourSearcher.Common.Services;
using SerjTm.Sample.TourSearcher.Common.Specifications;
using static SerjTm.Sample.TourSearcher.Common.Model.TourCategory;
using SerjTm.Sample.TourSearcher.OtherProvider.Storages;
using SerjTm.Sample.TourSearcher.OtherProvider.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace SerjTm.Sample.TourSearcher.OtherProvider.Services
{
    public class MemorySearchService : ISearchService
    {
        public MemorySearchService(MemoryTourStorage storage)
        {
            this.Storage = storage;
        }
        private readonly MemoryTourStorage Storage;

        public async Task<IEnumerable<Tour>> Search(int? peopleCount, FilterSpecification<Tour> filter, SearchOrder? order, CancellationToken cancellation)
        {
            await Task.Delay(TimeSpan.FromSeconds(new Random().NextDouble(3, 17)), cancellation);

            return Storage.Tours
                .Where(filter & PeopleCount(peopleCount))
                .Select(tour => tour.With(fullPrice: tour.PriceByOnePeople * (peopleCount ?? 1)))
                .OrderBy(order)
                .Take(1000)
                .ToArray();
        }
    }

 
}
