using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentBusinessLayer;
using StudentDataAccess;
using static StudentBusinessLayer.Student;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentApiController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllStudents")] // Marks this method to respond to HTTP GET requests.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //here we used StudentDTO
        public ActionResult<IEnumerable<StudentDTO>> GetAllStudents() // Define a method to get all students.
        {
            List<StudentDTO> StudentsList = StudentBusinessLayer.Student.GetAllStudents();
            if (StudentsList.Count == 0)
            {
                return NotFound("No Students Found!");
            }
            return Ok(StudentsList); // Returns the list of students.
        }


        [HttpGet("Passed", Name = "GetAllPassedStudents")] // Marks this method to respond to HTTP GET requests.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //here we used StudentDTO
        public ActionResult<IEnumerable<StudentDTO>> GetAllPassedStudents() // Define a method to get all Passed students.
        {
            List<StudentDTO> StudentsList = StudentBusinessLayer.Student.GetAllPassedStudents();
            if (StudentsList.Count == 0)
            {
                return NotFound("No Passed Students Found!");
            }
            return Ok(StudentsList); // Returns the list of students.
        }



        [HttpGet("AverageGrade", Name = "GetAverageGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<double> GetAverageGrade()
        {
            //var averageGrade = StudentDataSimulation.StudentsList.Average(student => student.Grade);
            double averageGrade = StudentBusinessLayer.Student.GetAverageGrade();
            return Ok(averageGrade);
        }



        [HttpGet("{id}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StudentDTO> GetStudentById(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }
            StudentBusinessLayer.Student student = StudentBusinessLayer.Student.Find(id);

            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }

            //here we get only the DTO object to send it back.
            StudentDTO SDTO = student.SDTO;

            //we return the DTO not the student object.
            return Ok(SDTO);
        }

    }
}
