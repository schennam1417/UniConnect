using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniConnectAPI.Data;
using UniConnectAPI.Models;
using UniConnectAPI.Models.DTO;

namespace UniConnectAPI.Controllers
{
    [Route("api/StudentAPI")]
    [ApiController]
    public class StudentAPIController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private int studentIDCounter = 0;
        private static readonly object lockObject = new object();
        public StudentAPIController(ApplicationDbContext dbContext,IMapper mapper)
        {
            this._dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_dbContext.Students.ToList());
        }

        [HttpGet]
        [Route("StudentID")]
        public ActionResult GetStudent(string StudentID)
        {
            var student= _dbContext.Students.SingleOrDefault(u=>u.StudentID==StudentID);
            return Ok(student);
        }

        [HttpPost]
        public ActionResult AddStudent([FromBody] AddStudentDTO addstudent)
        {
            try
            {
                var student=_mapper.Map<Student>(addstudent);
                //student.StudentID = "24000001";
                student.StudentID = GenerateStudentID();
                _dbContext.Students.Add(student);
                _dbContext.SaveChanges();
                return Ok(CreatedAtAction(nameof(GetStudent), new {StudentID= student.StudentID},student));
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string GenerateStudentID()
        {
            
            lock (lockObject)
            {
                // Getting the current year as first 2 digits
                string currentYear = DateTime.Now.Year.ToString().Substring(2);

        // Fetch all student IDs from the database
            var studentIds = _dbContext.Students
            .Where(s => s.StudentID.StartsWith(currentYear)) // Assuming student IDs start with the current year
            .Select(s => s.StudentID)
            .ToList();

                // If no student IDs are found, set the counter to 0
                if (studentIds.Count == 0)
                {
                    studentIDCounter = 0;
                }
                else
                {
                    // Get the maximum numeric part of the student IDs
                    var maxNumericID = studentIds
                        .Select(id => int.Parse(id.Substring(2))) // Assuming '24' as prefix
                        .Max();

                // Set the student ID counter to the maximum numeric ID
                studentIDCounter = maxNumericID;
                }

                // Increment the counter for the next student ID
                studentIDCounter++;

                // Formatting student ID with current year and six-digit number
                string studentID = $"{currentYear}{studentIDCounter:D6}";
                return studentID;
            }
        }

        [HttpPut]
        [Route("StudentID")]
        public ActionResult Edit(string StudentID, [FromBody] UpdateStudentDTO updateStudentDTO)
        {
            var student=_dbContext.Students.FirstOrDefault(u=>u.StudentID == StudentID);
            if (student == null) { return NotFound("Student Not found"); }
            student.StudentName = updateStudentDTO.StudentName;
            student.WelshLanguageProficiency=updateStudentDTO.WelshLanguageProficiency;
            _dbContext.SaveChanges();
            return Ok(student);

        }
                
        [HttpDelete]
        [Route("StudentID")]
        public ActionResult Delete(String StudentID)
        {
            try
            {
                var student = _dbContext.Students.FirstOrDefault(u => u.StudentID == StudentID);
                if (student == null) { return NotFound("Student not found"); }
                _dbContext.Students.Remove(student);
                _dbContext.SaveChanges();
                return NoContent();
            }
            catch(Exception ex) 
            {
                throw;
            }
        }
    }
}
