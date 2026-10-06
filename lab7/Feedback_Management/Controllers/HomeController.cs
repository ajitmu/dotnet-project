using Feedback_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Feedback_Management.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var feedbacks = FeedbackStore.Feedbacks;

            ViewBag.TotalFeedbacks = feedbacks.Count;
            ViewBag.AverageRating = feedbacks.Any() ? Math.Round(feedbacks.Average(f => f.Rating), 1) : 0;
            ViewBag.ResponseRate = "100%";

            return View(feedbacks);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}