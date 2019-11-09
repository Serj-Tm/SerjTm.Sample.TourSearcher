using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SerjTm.Sample.Common.Model;
using SerjTm.Sample.Common.Services;

namespace SerjTm.Sample.TourSearcher.WebApi.Controllers
{
    [ApiController]
    public class SearchController : ControllerBase
    {
        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }
        public readonly ISearchService searchService;

        [HttpPost("api/search")]
        public ActionResult<IEnumerable<Tour>> Search(SearchRequest request)
        {
            return Ok(searchService.Search(request.StartCity, request.City, request.StartDate, request.MinDays, request.MaxDays, request.PeopleCount, request.Order));
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
