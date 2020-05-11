using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IncidentTracker.BusinessEntitiy;
using IncidentTracker.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IncidentTracker.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private ILocationDAL _locationDAL;
        private ILogger<LocationController> _logger;
        public LocationController(ILocationDAL locationDAL, ILogger<LocationController> logger)
        {
            _locationDAL = locationDAL;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Location locationObj)
        {
            _logger.LogInformation($"Create location for {locationObj.LocationName}");

            var _savedLocation = await _locationDAL.AddLocation(locationObj);
            if (_savedLocation)
                return Ok();
            else return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation($"GetAll locations");

            var _locatins = await _locationDAL.GetLocations();
            if (_locatins != null && _locatins.Count() > 0)
                return Ok(_locatins);
            else return NotFound();
        }

        [HttpDelete]
        [Route("{locationID:int}")]
        public IActionResult Delete(int locationID)
        {
            _logger.LogInformation($"Delete location {locationID}");

            if (_locationDAL.DeleteLocation(locationID))
                return Ok();
            else
                return NotFound();
        }

    }
}