using Microsoft.AspNetCore.Mvc;
using SmartCampusPortal.DAL;
using SmartCampusPortal.Models;

namespace SmartCampusPortal.Controllers
{
    public class ServiceRequestController : Controller
    {
        private readonly ServiceRequestDAL _dal;

        public ServiceRequestController(ServiceRequestDAL dal)
        {
            _dal = dal;
        }

        // GET: ServiceRequest  (optional ?status=Pending filter)
        public IActionResult Index(string status)
        {
            var all = _dal.GetAll();

            ViewBag.Total = all.Count;
            ViewBag.Pending = all.Count(r => r.Status == "Pending");
            ViewBag.InProgress = all.Count(r => r.Status == "In Progress");
            ViewBag.Completed = all.Count(r => r.Status == "Completed");
            ViewBag.CurrentFilter = status;

            var list = string.IsNullOrEmpty(status)
                ? all
                : all.Where(r => r.Status == status).ToList();

            return View(list);
        }

        // GET: ServiceRequest/Create
        public IActionResult Create()
        {
            return View(new ServiceRequest());
        }

        // POST: ServiceRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ServiceRequest request)
        {
            if (ModelState.IsValid)
            {
                int id = _dal.Insert(request);
                TempData["Message"] = "Request #" + id + " submitted successfully.";
                return RedirectToAction("Index");
            }

            return View(request);
        }

        // GET: ServiceRequest/UpdateStatus/1
        public IActionResult UpdateStatus(int id)
        {
            var request = _dal.GetById(id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // POST: ServiceRequest/UpdateStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateStatus(int id, string status)
        {
            if (!ServiceOptions.Statuses.Contains(status))
            {
                return BadRequest("Invalid status");
            }

            if (!_dal.UpdateStatus(id, status))
            {
                return NotFound();
            }

            TempData["Message"] = "Request #" + id + " status changed to " + status + ".";
            return RedirectToAction("Index");
        }

        // POST: ServiceRequest/Delete/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (_dal.Delete(id))
            {
                TempData["Message"] = "Request #" + id + " deleted.";
            }

            return RedirectToAction("Index");
        }
    }
}
