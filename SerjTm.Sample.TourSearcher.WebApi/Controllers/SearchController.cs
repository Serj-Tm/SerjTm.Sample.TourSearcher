using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SerjTm.Sample.Common.Model;
using SerjTm.Sample.Common.Services;
using SerjTm.Sample.TourSearcher.Aggregator;

namespace SerjTm.Sample.TourSearcher.WebApi.Controllers
{
    [ApiController]
    public class SearchController : ControllerBase
    {
        public SearchController(AggregatorService searchService)
        {
            this.searchService = searchService;
        }
        public readonly ISearchService searchService;

        [HttpPost("api/search")]
        public async Task<ActionResult<IEnumerable<Tour>>> Search(SearchRequest request, CancellationToken token)
        {
            return Ok(await searchService.Search(request.StartCity, request.City, request.StartDate, request.MinDays, request.MaxDays, request.PeopleCount, request.Order, token));
        }
    }

    public class SearchRequest
    {
        public City_Id StartCity;
        public City_Id City;
        public DateTime? StartDate;
        public int? MinDays;
        public int? MaxDays;
        public int? PeopleCount;
        public SearchOrder? Order;
    }
    public class SearchRequest_Hotel
    {
        public Guid Id;
    }

}
