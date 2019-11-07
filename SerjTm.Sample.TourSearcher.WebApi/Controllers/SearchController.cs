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

        [HttpGet("api/search")]
        public ActionResult<IEnumerable<Tour>> Search()
        {
            return Ok(searchService.Search(null, null, null, null, null, null, null));
        }
    }
}
