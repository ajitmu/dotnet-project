using System.ComponentModel.DataAnnotations;

namespace Practical_10_Student_REST_API.Models {

    public class Student {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;

        [Range(1, 8)]
        public int Semester { get; set; }

    }

}
