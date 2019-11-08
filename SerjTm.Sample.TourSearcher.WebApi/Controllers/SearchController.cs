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
        public SearchController(ISearchService searchService, IDictService dictService)
        {
            this.searchService = searchService;
            this.dictService = dictService;
        }
        public readonly ISearchService searchService;
        public readonly IDictService dictService;

        [HttpPost("api/search")]
        public ActionResult<IEnumerable<Tour>> Search(SearchRequest request)
        {
            var startCity = request.StartCity != null ? dictService.FindCity(request.StartCity.Id) : null;
            if (request.StartCity != null && startCity == null)
                return BadRequest("Invalid argument StartCity.Id");//TODO улучшить сообщение об ошибке
            var city = request.City != null ? dictService.FindCity(request.City.Id) : null;
            if (request.City != null && city == null)
                return BadRequest("Invalid argument City.Id");//TODO улучшить сообщение об ошибке

            return Ok(searchService.Search(startCity, city, request.StartDate, request.MinDays, request.MaxDays, request.PeopleCount, request.Order));
        }
    }

    public class SearchRequest
    {
        public SearchRequest_City StartCity;
        public SearchRequest_City City;
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
    public class SearchRequest_City
    {
        public Guid Id;
    }

}
