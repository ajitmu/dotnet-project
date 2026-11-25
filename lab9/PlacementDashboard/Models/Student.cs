namespace PlacementDashboard.Models;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Course { get; set; } = "";
    public string? Company { get; set; }
    public decimal PackageLpa { get; set; }
    public bool IsPlaced => !string.IsNullOrEmpty(Company);
    public string Status => IsPlaced ? "Placed" : "Not Placed";
}
