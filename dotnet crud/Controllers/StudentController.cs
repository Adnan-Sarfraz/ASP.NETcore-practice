using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        // Temporary in-memory student list
        private static List<Student> students = new List<Student>()
        {
            new Student { Id = 1, Name = "Ali", Age = 20 },
            new Student { Id = 2, Name = "Sara", Age = 22 }
        };

        // GET: api/student
        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetAllStudents()
        {
            return students;
        }

        // POST: api/student
        [HttpPost]
        public ActionResult<Student> CreateStudent([FromBody] Student student)
        {
            // Auto-generate ID
            student.Id = students.Max(s => s.Id) + 1;

            // Add new student to the list
            students.Add(student);

            // Return 201 Created with the new student
            return CreatedAtAction(nameof(GetAllStudents), new { id = student.Id }, student);
        }


        // PUT: api/student/1
        [HttpPut("id")]
        public IActionResult UpdateStudent(int id, [FromBody] Student updatedStudent)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound("Student not found");
            }

            // Update student properties
            student.Name = updatedStudent.Name;
            student.Age = updatedStudent.Age;

            return NoContent(); // 204 No Content
        }

        // DELETE: api/student/1
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound("Student not found");
            }

            students.Remove(student);
            return NoContent(); // 204 No Content
        }


    }
}
