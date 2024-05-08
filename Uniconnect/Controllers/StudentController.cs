using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Uniconnect.Models;
using Uniconnect.Models.DTO;
using Uniconnect.Services.IServices;
using UniConnect.Models.DTO;

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
            var response = await _studentService.GetAllAsync<APIResponse>();
            if (response != null) 
            {
                list=JsonConvert.DeserializeObject<List<StudentDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
                
        }
        
        public async Task<IActionResult> EnrollStudent()
        {
            
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnrollStudent(AddStudentDTO addStudentDTO)
        {
            if(ModelState.IsValid) 
            {
                var response = await _studentService.CreateAsync<APIResponse>(addStudentDTO);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexStudent));
                }

            }

            return View(addStudentDTO);

        }

        public async Task<IActionResult> UpdateStudent(string StudentID)
        {
            var response = await _studentService.GetAsync<APIResponse>(StudentID);
            if (response != null && response.IsSuccess)
            {
                StudentDTO model=JsonConvert.DeserializeObject<StudentDTO>(Convert.ToString(response.Result));
                //return RedirectToAction(nameof(IndexStudent));
                return View(_mapper.Map<UpdateStudentDTO>(model));
            }
            return NotFound();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateStudent(UpdateStudentDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _studentService.UpdateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexStudent));
                }

            }

            return View(model);

        }

        public async Task<IActionResult> DeleteStudent(string StudentID)
        {
            var response = await _studentService.GetAsync<APIResponse>(StudentID);
            if (response != null && response.IsSuccess)
            {
                StudentDTO model = JsonConvert.DeserializeObject<StudentDTO>(Convert.ToString(response.Result));
                //return RedirectToAction(nameof(IndexStudent));
                return View(model);
            }
            return NotFound();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteStudent(StudentDTO model)
        {
                var response = await _studentService.RemoveAsync<APIResponse>(model.StudentID);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexStudent));
                }

            return View(model);

        }

    }
}
