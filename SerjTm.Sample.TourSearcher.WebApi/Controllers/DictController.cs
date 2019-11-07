using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SerjTm.Sample.Common.Model;
using SerjTm.Sample.Common.Services;

namespace SerjTm.Sample.TourSearcher.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictController : ControllerBase
    {
        public DictController(IDictService dictService)
        {
            this.dictService = dictService;
        }
        public readonly IDictService dictService;

        [HttpGet("countries")]
        public ActionResult<IEnumerable<Country>> Countries()
        {
            return Ok(dictService.Countries());
        }
        [HttpGet("cities")]
        public ActionResult<IEnumerable<City>> Cities()
        {
            return Ok(dictService.Cities());
        }
        [HttpGet("hotes")]
        public ActionResult<IEnumerable<Hotel>> Hotels()
        {
            return Ok(dictService.Hotels());
        }
        [HttpGet("hotel/{id}")]
        public IActionResult Hotel(Guid id)
        {
            var hotel = dictService.FindHotel(id);
            if (hotel == null)
                return NotFound();
            return Ok(hotel);
        }
    }
}
