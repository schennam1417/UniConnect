using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using UniConnectAPI.Data;
using UniConnectAPI.Models;
using UniConnectAPI.Models.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniConnectAPI.logging;

namespace UniConnectAPI.Controllers
{
    [Route("api/StudentAPI")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogging _logger;

        public StudentAPIController(IMapper mapper,ILogging logger )
        {
            this._mapper = mapper;
            this._logger = logger;
            
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<StudentDTO>> GetStudents()
        {
            _logger.Log("Getting all Students","");
            return Ok(StudentDataStore.StudentList);
            
        }


        [HttpGet("{StudentID}", Name = "GetStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StudentDTO> GetStudent(string StudentID)
        {
            _logger.Log("Getting Student By ID","");
            var student = StudentDataStore.StudentList.FirstOrDefault(u => u.StudentID == StudentID);
            if (student == null) { return NotFound(); }
            return Ok(student);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<StudentDTO> EnrollStudent([FromBody] StudentDTO studentDto)
        {
            _logger.Log("Adding Student", "");
            if (studentDto == null) 
            {
                _logger.Log("error while adding","Student");
                return BadRequest(studentDto); 
            }
            //studentDto.StudentID = StudentDataStore.StudentList.OrderByDescending(u => u.StudentID).FirstOrDefault().StudentID + 1;
            if (StudentDataStore.StudentList.FirstOrDefault(u => u.StudentName.ToLower() == studentDto.StudentName.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Student Already Exists");
                return BadRequest(ModelState);
            }
            StudentDataStore.StudentList.Add(studentDto);
            return CreatedAtRoute("GetStudent", new { StudentID = studentDto.StudentID }, studentDto);
        }

        [HttpDelete("{StudentID}",Name = "DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteStudent(string StudentID) 
        {
            _logger.Log("Deleting Student By ID","");
            if (StudentID==null)
            { return BadRequest(); }
            var student = StudentDataStore.StudentList.FirstOrDefault(u=>u.StudentID== StudentID);
            if (student == null) { return NotFound(); }
            StudentDataStore.StudentList.Remove(student);
            return NoContent();
        }

        [HttpPut("{StudentID}",Name ="UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdateStudent(string StudentID, [FromBody]UpdateStudentDTO updateStudentDTO)
        {
            _logger.Log("Updating Student By ID", "");
            if (StudentID==null) {  return BadRequest(); }
            var student = StudentDataStore.StudentList.FirstOrDefault(x => x.StudentID== StudentID);

            if (student == null)
            {
                return NotFound();
            }

            student.StudentName = updateStudentDTO.StudentName;
            student.WelshLanguageProficiency = updateStudentDTO.WelshLanguageProficiency;
            return NoContent();
        }


    }
}
