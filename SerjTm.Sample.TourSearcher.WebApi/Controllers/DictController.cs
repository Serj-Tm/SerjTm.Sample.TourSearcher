using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SerjTm.Sample.TourSearcher.Common.Model;
using SerjTm.Sample.TourSearcher.Common.Services;

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
        /// <summary>
        /// Города вылета
        /// </summary>
        /// <returns></returns>
        [HttpGet("startCities")]
        public ActionResult<IEnumerable<City>> StartCities()
        {
            return Ok(dictService.FlyCities());
        }
        /// <summary>
        /// Города прилета
        /// </summary>
        /// <returns></returns>
        [HttpGet("endCities")]
        public ActionResult<IEnumerable<City>> EndCities()
        {
            return Ok(dictService.FlyCities());
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
