using System.ComponentModel.DataAnnotations;

namespace SmartCampusPortal.Models
{
    public class ServiceRequest
    {
        public int RequestId { get; set; }

        [Required(ErrorMessage = "Student name is required")]
        [StringLength(100)]
        [Display(Name = "Student Name")]
        public string StudentName { get; set; }

        [Required(ErrorMessage = "Department is required")]
        [StringLength(100)]
        public string Department { get; set; }

        [Required(ErrorMessage = "Service type is required")]
        [AllowedValues("IT Support", "Library", "Maintenance", "Transport",
            ErrorMessage = "Invalid service type")]
        [Display(Name = "Service Type")]
        public string ServiceType { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, MinimumLength = 5,
            ErrorMessage = "Description must be 5 to 500 characters")]
        public string Description { get; set; }

        [AllowedValues("Low", "Medium", "High", ErrorMessage = "Invalid priority")]
        public string Priority { get; set; } = "Medium";

        public string Status { get; set; } = "Pending";

        [Display(Name = "Date")]
        public DateTime RequestDate { get; set; } = DateTime.Now;
    }

    // Body of PUT api/ServiceApi/UpdateStatus/{id}
    public class StatusUpdateDto
    {
        [Required]
        [AllowedValues("Pending", "In Progress", "Completed",
            ErrorMessage = "Status must be Pending, In Progress or Completed")]
        public string Status { get; set; }
    }

    // Dropdown values shared by views and validation
    public static class ServiceOptions
    {
        public static readonly string[] ServiceTypes = { "IT Support", "Library", "Maintenance", "Transport" };
        public static readonly string[] Priorities = { "Low", "Medium", "High" };
        public static readonly string[] Statuses = { "Pending", "In Progress", "Completed" };
    }
}
