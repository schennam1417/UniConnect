using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using UniConnectAPI.Data;
using UniConnectAPI.Models;
using UniConnectAPI.Models.DTO;
using UniConnectAPI.Repository;
using UniConnectAPI.Repository.IRepository;

namespace UniConnectAPI.Controllers.v1
{

    [Route("api/v{version:apiVersion}/StudentAPI")]
    [ApiController]
    [ApiVersion("1.0")]

    public class StudentAPIController : Controller
    {
        //private readonly ApplicationDbContext _dbContext;
        protected APIResponse _response;
        private readonly IStudentRepository _dbStudent;
        private readonly IMapper _mapper;

        public StudentAPIController(IStudentRepository dbStudent, IMapper mapper)
        {
            //this._dbContext = dbContext;
            _dbStudent = dbStudent;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ResponseCache(Duration =20)]
        // [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllStudents(int PageSize = 0, int PageNumber = 1)
        {
            try
            {
                IEnumerable<Student> studentlist = await _dbStudent.GetAllAsync(PageSize:PageSize,PageNumber:PageNumber);
                 Pagination pagination=new() { PageNumber= PageNumber,PageSize=PageSize };
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                _response.Result = _mapper.Map<List<StudentDTO>>(studentlist);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
                // return Ok(_dbContext.Students.ToList());
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }



        [HttpGet("{StudentID}", Name = "GetStudent")]
        [ResponseCache(CacheProfileName = "Default30")]
        //[ResponseCache(Location =ResponseCacheLocation.None,NoStore =true)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        // [Route("StudentID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetStudent(string StudentID)
        {
            try
            {
                // var student= _dbContext.Students.SingleOrDefault(u=>u.StudentID==StudentID);
                //return Ok(student);
                if (StudentID == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var student = await _dbStudent.GetAsync(u => u.StudentID == StudentID);

                if (student == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                //return Ok(_mapper.Map<StudentDTO>(student));
                _response.Result = _mapper.Map<StudentDTO>(student);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> AddStudent([FromBody] AddStudentDTO addstudent)
        {
            try
            {
                // var student=_mapper.Map<Student>(addstudent);
                //student.StudentID = "24000001";
                //student.StudentID = GenerateStudentID();
                //_dbContext.Students.Add(student);
                //_dbContext.SaveChanges();
                //return Ok(CreatedAtAction(nameof(GetStudent), new {StudentID= student.StudentID},student));
                //if (await _dbStudent.GetAsync(u => u.StudentName.ToLower() == addstudent.StudentName.ToLower()) != null)
                //{
                //    ModelState.AddModelError("Custom Error", "Student Already Exists");
                //    return BadRequest(ModelState);
                //}
                if (addstudent == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                Student student = _mapper.Map<Student>(addstudent);

                await _dbStudent.CreateAsync(student);
                _response.Result = _mapper.Map<StudentDTO>(student);
                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                //return Ok(_response);
                //return Ok(CreatedAtAction(nameof(GetStudent), new { StudentID = student.StudentID }, student));
                return CreatedAtRoute("GetStudent", new { student.StudentID }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }



        [HttpPut("{StudentID}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateStudent(string StudentID, [FromBody] UpdateStudentDTO updateStudentDTO)
        {
            //var student=_dbContext.Students.FirstOrDefault(u=>u.StudentID == StudentID);
            //if (student == null) { return NotFound("Student Not found"); }
            //student.StudentName = updateStudentDTO.StudentName;
            //student.WelshLanguageProficiency=updateStudentDTO.WelshLanguageProficiency;
            //_dbContext.SaveChanges();
            //return Ok(student);
            //var student=_dbContext.Students.FirstOrDefault(u=>u.StudentID == StudentID);
            try
            {

                if (updateStudentDTO == null || StudentID != updateStudentDTO.StudentId)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                //Student stdt = _mapper.Map<Student>(updateStudentDTO);

                var existingStudent = await _dbStudent.GetAsync(u => u.StudentID == StudentID);

                // If student not found, return 404 Not Found
                if (existingStudent == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                // Update the properties of the existing student entity with values from the DTO
                existingStudent.StudentName = updateStudentDTO.StudentName;
                existingStudent.WelshLanguageProficiency = updateStudentDTO.WelshLanguageProficiency;

                //stdt.StudentName = updateStudentDTO.StudentName;
                //stdt.WelshLanguageProficiency = updateStudentDTO.WelshLanguageProficiency;
                //Student student = _mapper.Map<Student>(updateStudentDTO); 

                await _dbStudent.UpdateAsync(existingStudent);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;

            //return NoContent();

        }

        [HttpDelete("{StudentID}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> Delete(string StudentID)
        {
            try
            {
                if (StudentID == null) { return BadRequest(); }
                var student = await _dbStudent.GetAsync(u => u.StudentID == StudentID);
                if (student == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _dbStudent.RemoveAsync(student);
                //var student = _dbContext.Students.FirstOrDefault(u => u.StudentID == StudentID);
                //if (student == null) { return NotFound("Student not found"); }
                //_dbContext.Students.Remove(student);
                //_dbContext.SaveChanges();
                //return NoContent();
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpGet]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("search")]
        public async Task<ActionResult<APIResponse>> SearchStudentsByIdORName([FromQuery] string searchQuery)
        {
            // Search students by name or ID
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                return BadRequest();
            }
            var searchResults = await _dbStudent.SearchAsync(searchQuery);

            if (searchResults == null || !searchResults.Any())
            {
                return NotFound();
            }

            _response.Result = _mapper.Map<List<StudentDTO>>(searchResults);
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
    }
}
