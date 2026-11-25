using Microsoft.AspNetCore.Mvc;
using SmartCampusPortal.DAL;
using SmartCampusPortal.Models;

namespace SmartCampusPortal.Controllers
{
    // REST API: /api/ServiceApi
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceApiController : ControllerBase
    {
        private readonly ServiceRequestDAL _dal;

        public ServiceApiController(ServiceRequestDAL dal)
        {
            _dal = dal;
        }

        // GET: api/ServiceApi
        [HttpGet]
        public ActionResult<IEnumerable<ServiceRequest>> GetRequests()
        {
            return Ok(_dal.GetAll());
        }

        // GET: api/ServiceApi/1
        [HttpGet("{id:int}")]
        public ActionResult<ServiceRequest> GetRequest(int id)
        {
            var request = _dal.GetById(id);
            if (request == null)
            {
                return NotFound("Service request not found");
            }

            return Ok(request);
        }

        // POST: api/ServiceApi
        [HttpPost]
        public ActionResult<ServiceRequest> CreateRequest(ServiceRequest request)
        {
            // [ApiController] automatically returns 400 when the model is invalid
            int id = _dal.Insert(request);
            return CreatedAtAction(nameof(GetRequest), new { id = id }, _dal.GetById(id));
        }

        // PUT: api/ServiceApi/UpdateStatus/1   body: { "status": "Completed" }
        [HttpPut("UpdateStatus/{id:int}")]
        public IActionResult UpdateStatus(int id, [FromBody] StatusUpdateDto dto)
        {
            if (!_dal.UpdateStatus(id, dto.Status))
            {
                return NotFound("Service request not found");
            }

            return Ok("Status updated successfully");
        }

        // DELETE: api/ServiceApi/1
        [HttpDelete("{id:int}")]
        public IActionResult DeleteRequest(int id)
        {
            if (!_dal.Delete(id))
            {
                return NotFound("Service request not found");
            }

            return Ok("Service request deleted successfully");
        }
    }
}
