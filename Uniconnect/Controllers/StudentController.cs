using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Uniconnect.Models;
using Uniconnect.Models.DTO;
using Uniconnect.Services.IServices;
using UniConnect.Models.DTO;
using UniConnectUtility;

namespace Uniconnect.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StudentController(IStudentService studentService,IMapper mapper)
        {
            this._studentService = studentService;
            this._mapper = mapper;
        }

        public async Task<ActionResult> IndexStudent() 
        {
            List<StudentDTO> list = new();
            var response = await _studentService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null) 
            {
                list=JsonConvert.DeserializeObject<List<StudentDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
                
        }
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> EnrollStudent()
        {
            
            return View();

        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnrollStudent(AddStudentDTO addStudentDTO)
        {
            if(ModelState.IsValid) 
            {
                var response = await _studentService.CreateAsync<APIResponse>(addStudentDTO, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexStudent));
                }

            }

            return View(addStudentDTO);

        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStudent(string StudentID)
        {
            var response = await _studentService.GetAsync<APIResponse>(StudentID, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                StudentDTO model=JsonConvert.DeserializeObject<StudentDTO>(Convert.ToString(response.Result));
                //return RedirectToAction(nameof(IndexStudent));
                return View(_mapper.Map<UpdateStudentDTO>(model));
            }
            return NotFound();

        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateStudent(UpdateStudentDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _studentService.UpdateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexStudent));
                }

            }

            return View(model);

        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStudent(string StudentID)
        {
            var response = await _studentService.GetAsync<APIResponse>(StudentID, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                StudentDTO model = JsonConvert.DeserializeObject<StudentDTO>(Convert.ToString(response.Result));
                //return RedirectToAction(nameof(IndexStudent));
                return View(model);
            }
            return NotFound();

        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteStudent(StudentDTO model)
        {
                var response = await _studentService.RemoveAsync<APIResponse>(model.StudentID, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexStudent));
                }

            return View(model);

        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        
        public async Task<ActionResult> SearchStudent(string searchString)
        {
            List<StudentDTO> list = new();
            var response = await _studentService.SearchAsync<APIResponse>(searchString, HttpContext.Session.GetString(SD.SessionToken));
            if (response == null)
            { return View(); }
            if (response != null)
            {
                list = JsonConvert.DeserializeObject<List<StudentDTO>>(Convert.ToString(response.Result));
            }
            return View(list);

        }

    }
}
