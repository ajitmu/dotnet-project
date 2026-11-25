namespace PlacementDashboard.Models;

public class DashboardViewModel
{
    public List<Student> Students { get; set; } = new();
    public List<string> Courses { get; set; } = new();
    public string Status { get; set; } = "All";
    public string Course { get; set; } = "All";
    public string? Search { get; set; }

    public int Total => Students.Count;
    public int Placed => Students.Count(s => s.IsPlaced);
    public int NotPlaced => Total - Placed;
    public int CompanyCount => Students.Where(s => s.IsPlaced).Select(s => s.Company).Distinct().Count();
    public double Rate => Total == 0 ? 0 : Math.Round(Placed * 100.0 / Total, 1);
    public decimal Highest => Students.Any() ? Students.Max(s => s.PackageLpa) : 0;
    public decimal Average => Placed == 0 ? 0 : Math.Round(Students.Where(s => s.IsPlaced).Average(s => s.PackageLpa), 1);
}
