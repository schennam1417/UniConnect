using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniConnectAPI.Data;
using UniConnectAPI.Models;
using UniConnectAPI.Models.DTO;
using UniConnectAPI.Repository.IRepository;

namespace UniConnectAPI.Controllers
{
    [Route("api/StudentAPI")]
    [ApiController]
    public class StudentAPIController : Controller
    {
        //private readonly ApplicationDbContext _dbContext;
        private readonly IStudentRepository _dbStudent;
        private readonly IMapper _mapper;
        
        public StudentAPIController(IStudentRepository dbStudent,IMapper mapper)
        {
            //this._dbContext = dbContext;
            this._dbStudent = dbStudent;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async  Task<ActionResult<IEnumerable<StudentDTO>>> GetAllStudents()
        {
            IEnumerable<Student> studentlist = await _dbStudent.GetAllAsync();
            return Ok(_mapper.Map<List<StudentDTO>>(studentlist));
           // return Ok(_dbContext.Students.ToList());
        }

        [HttpGet]
        [Route("StudentID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentDTO>> GetStudent(string StudentID)
        {
           // var student= _dbContext.Students.SingleOrDefault(u=>u.StudentID==StudentID);
            //return Ok(student);
            if (StudentID == null) { return BadRequest(); }
            var student = await _dbStudent.GetAsync(u => u.StudentID == StudentID);
            if (student == null) { return NotFound(); }
            return Ok(_mapper.Map<StudentDTO>(student));


        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDTO>> AddStudent([FromBody] AddStudentDTO addstudent)
        {
            try
            {
                // var student=_mapper.Map<Student>(addstudent);
                //student.StudentID = "24000001";
                //student.StudentID = GenerateStudentID();
                //_dbContext.Students.Add(student);
                //_dbContext.SaveChanges();
                //return Ok(CreatedAtAction(nameof(GetStudent), new {StudentID= student.StudentID},student));
                if (await _dbStudent.GetAsync(u => u.StudentName.ToLower() == addstudent.StudentName.ToLower()) != null)
                {
                    ModelState.AddModelError("Custom Error", "Student Already Exists");
                }
                if(addstudent==null)
                { return BadRequest(); }

                Student student=_mapper.Map<Student>(addstudent);
                
                await _dbStudent.CreateAsync(student);
                return Ok(CreatedAtAction(nameof(GetStudent), new { StudentID = student.StudentID }, student));
            }
            catch (Exception)
            {

                throw;
            }
        }

        

        [HttpPut]
        [Route("StudentID")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStudent(string StudentID, [FromBody] UpdateStudentDTO updateStudentDTO)
        {
            //var student=_dbContext.Students.FirstOrDefault(u=>u.StudentID == StudentID);
            //if (student == null) { return NotFound("Student Not found"); }
            //student.StudentName = updateStudentDTO.StudentName;
            //student.WelshLanguageProficiency=updateStudentDTO.WelshLanguageProficiency;
            //_dbContext.SaveChanges();
            //return Ok(student);
            //var student=_dbContext.Students.FirstOrDefault(u=>u.StudentID == StudentID);
            if (updateStudentDTO == null) { return BadRequest(); }

            var stdt = await _dbStudent.GetAsync(u => u.StudentID == StudentID);

            if (stdt == null) { return NotFound(); }
            stdt.StudentName = updateStudentDTO.StudentName;
            stdt.WelshLanguageProficiency=updateStudentDTO.WelshLanguageProficiency;
            //Student student = _mapper.Map<Student>(updateStudentDTO); 

            await _dbStudent.UpdateAsync(stdt);
            
            return NoContent();

        }
                
        [HttpDelete]
        [Route("StudentID")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(String StudentID)
        {
            try
            {
                if(StudentID==null) {  return BadRequest(); }
                var student = await _dbStudent.GetAsync(u => u.StudentID == StudentID);
                if (student == null) { return NotFound() ; }
                await _dbStudent.RemoveAsync(student);
                //var student = _dbContext.Students.FirstOrDefault(u => u.StudentID == StudentID);
                //if (student == null) { return NotFound("Student not found"); }
                //_dbContext.Students.Remove(student);
                //_dbContext.SaveChanges();
                return NoContent();
            }
            catch (Exception ex) 
            {
                throw;
            }
        }
    }
}
