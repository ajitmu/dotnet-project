using Feedback_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Feedback_Management.Controllers
{
    // Temporary storage in memory
    public static class FeedbackStore
    {
        public static List<Feedback> Feedbacks { get; set; } = new List<Feedback>
        {
            new Feedback { Id = 1, Name = "Rahul Sharma", Email = "rahul@example.com", Rating = 5, Comments = "Great platform and smooth navigation!" },
            new Feedback { Id = 2, Name = "Priya Patel", Email = "priya@example.com", Rating = 4, Comments = "Good UI layout, looking forward to updates." }
        };
    }

    public class FeedbackController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitFeedback(Feedback model)
        {
            if (ModelState.IsValid)
            {
                model.Id = FeedbackStore.Feedbacks.Count + 1;
                FeedbackStore.Feedbacks.Insert(0, model); // Add new entry at top

                TempData["SuccessMessage"] = "Thank you! Your feedback has been successfully submitted.";
                return RedirectToAction("Index", "Home"); // Directly go to Dashboard after submission
            }

            return View("Index", model);
        }
    }
}