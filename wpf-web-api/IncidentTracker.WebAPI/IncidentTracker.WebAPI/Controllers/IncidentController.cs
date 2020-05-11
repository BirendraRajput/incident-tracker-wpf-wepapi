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
    [ApiController]
    [Route("[controller]")]
    public class IncidentController : ControllerBase
    {
        IIncidentDAL _iIncidentDAL;
        private readonly ILogger<IncidentController> _logger;
        public IncidentController(IIncidentDAL iIncidentDAL, ILogger<IncidentController> logger)
        {
            _iIncidentDAL = iIncidentDAL;
            _logger = logger;
        }

        /// <summary>
        /// Create an Incident
        /// </summary>
        /// <param name="incidentObj"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Incident incidentObj)
        {
            _logger.LogInformation($"Create Incident {incidentObj.IncidentID}");
            await _iIncidentDAL.CreateIncident(incidentObj);
            return Ok();
        }

        /// <summary>
        /// Update an incident
        /// </summary>
        /// <param name="incidentObj"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult Put([FromBody] Incident incidentObj)
        {
            _logger.LogInformation($"Update Incident {incidentObj.IncidentID}");

            if (_iIncidentDAL.UpdateIncident(incidentObj))
                return Ok();
            else
                return NotFound();
        }

        /// <summary>
        /// Return an incient by ID
        /// </summary>
        /// <param name="incidentID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{incidentID}")]
        public async Task<IActionResult> Get(int incidentID)
        {
            _logger.LogInformation($"Get incident {incidentID}");

            var result = await _iIncidentDAL.GetIncident(incidentID);
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        /// <summary>
        /// Return all incident
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("GetAll Incident");

            var incidents = await _iIncidentDAL.GetIncidents();
            if (incidents != null && incidents.Count() > 0)
                return Ok(incidents);
            else
                return NotFound();
        }

        /// <summary>
        /// Delete an incident by ID
        /// </summary>
        /// <param name="incidentID"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{incidentID:int}")]
        public IActionResult Delete(int incidentID)
        {
            _logger.LogInformation($"Delete incident {incidentID}");

            if (_iIncidentDAL.DeleteIncident(incidentID))
                return Ok();
            else
                return NotFound();
        }


    }
}