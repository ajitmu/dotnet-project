using PlacementDashboard.Models;

namespace PlacementDashboard.Services;

// In-memory data source (no database needed for the lab).
public class StudentRepository
{
    private readonly List<Student> _students = new()
    {
        new() { Id = 101, Name = "Aarav Mehta",   Course = "BCA",                  Company = "Google",    PackageLpa = 21.0m },
        new() { Id = 102, Name = "Diya Patel",    Course = "IT",                   Company = "Microsoft", PackageLpa = 18.5m },
        new() { Id = 103, Name = "Kabir Joshi",   Course = "Computer Engineering", Company = null,        PackageLpa = 0m },
        new() { Id = 104, Name = "Ananya Shah",   Course = "BCA",                  Company = "Amazon",    PackageLpa = 16.2m },
        new() { Id = 105, Name = "Rohan Desai",   Course = "MCA",                  Company = "Infosys",   PackageLpa = 7.5m },
        new() { Id = 106, Name = "Isha Trivedi",  Course = "IT",                   Company = "TCS",       PackageLpa = 6.8m },
        new() { Id = 107, Name = "Vivaan Gupta",  Course = "Computer Engineering", Company = "Adobe",     PackageLpa = 19.4m },
        new() { Id = 108, Name = "Meera Nair",    Course = "MCA",                  Company = null,        PackageLpa = 0m },
        new() { Id = 109, Name = "Yash Solanki",  Course = "BCA",                  Company = null,        PackageLpa = 0m },
        new() { Id = 110, Name = "Pooja Chauhan", Course = "IT",                   Company = "Wipro",     PackageLpa = 6.2m },
    };

    public List<string> GetCourses() => _students.Select(s => s.Course).Distinct().OrderBy(c => c).ToList();

    public List<Student> Query(string? status, string? course, string? search)
    {
        var q = _students.AsEnumerable();
        if (status == "Placed") q = q.Where(s => s.IsPlaced);
        else if (status == "Not Placed") q = q.Where(s => !s.IsPlaced);
        if (!string.IsNullOrEmpty(course) && course != "All") q = q.Where(s => s.Course == course);
        if (!string.IsNullOrWhiteSpace(search))
            q = q.Where(s => s.Name.Contains(search, StringComparison.OrdinalIgnoreCase)
                          || (s.Company ?? "").Contains(search, StringComparison.OrdinalIgnoreCase));
        return q.ToList();
    }
}
