using System.Text;
using Microsoft.AspNetCore.Mvc;
using PlacementDashboard.Models;
using PlacementDashboard.Services;

namespace PlacementDashboard.Controllers;

public class HomeController : Controller
{
    private readonly StudentRepository _repo;
    public HomeController(StudentRepository repo) => _repo = repo;

    private DashboardViewModel Build(string? status, string? course, string? search) => new()
    {
        Students = _repo.Query(status, course, search),
        Courses = _repo.GetCourses(),
        Status = status ?? "All",
        Course = course ?? "All",
        Search = search
    };

    public IActionResult Index(string? status, string? course, string? search)
        => View(Build(status, course, search));

    public IActionResult Report(string? status, string? course, string? search)
        => View(Build(status, course, search));

    public IActionResult Export(string? status, string? course, string? search)
    {
        var sb = new StringBuilder("Id,Name,Course,Company,PackageLPA,Status\n");
        foreach (var s in _repo.Query(status, course, search))
            sb.AppendLine($"{s.Id},{s.Name},{s.Course},{s.Company ?? "Not Assigned"},{s.PackageLpa},{s.Status}");
        return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "placement-report.csv");
    }

    public IActionResult Error() => Content("Something went wrong.");
}
