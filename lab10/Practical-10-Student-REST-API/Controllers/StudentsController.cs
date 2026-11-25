using Microsoft.AspNetCore.Mvc;
using Practical_10_Student_REST_API.Models;
using System.Collections;

namespace Practical_10_Student_REST_API.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase {

        // In-memory store (persists across requests because it is static)
        private static List<Student> students = new List<Student> {
            new Student {
                Id = 1,
                Name = "Rahul",
                Email = "rahul@gmail.com",
                Department = "Computer Engineering",
                Semester = 5
            },
            new Student {
                Id = 2,
                Name = "Priya",
                Email = "priya@gmail.com",
                Department = "Information Technology",
                Semester = 5
            }
        };

        // GET: api/students
        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetAllStudents() {
            return Ok(students);
        }

        // GET: api/students/1
        [HttpGet("{id}")]
        public ActionResult<Student> GetStudentById(int id) {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound("Student not found");
            }
            return Ok(student);
        }

        // POST: api/students
        [HttpPost]
        public ActionResult<Student> AddStudent(Student student) {
            student.Id = students.Count == 0 ? 1 : students.Max(s => s.Id) + 1;
            students.Add(student);
            return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
        }

        // PUT: api/students/1
        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, Student updatedStudent) {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null) {
                return NotFound("Student not found");
            }
            student.Name = updatedStudent.Name;
            student.Email = updatedStudent.Email;
            student.Department = updatedStudent.Department;
            student.Semester = updatedStudent.Semester;
            return Ok(student);
        }

        // DELETE: api/students/1
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id) {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null) {
                return NotFound("Student not found");
            }
            students.Remove(student);
            return Ok("Student deleted successfully");
        }

    }

}
