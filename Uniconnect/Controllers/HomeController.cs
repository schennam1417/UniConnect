using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using Uniconnect.Models;
using Uniconnect.Models.DTO;
using Uniconnect.Services.IServices;
using UniConnectUtility;

namespace Uniconnect.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public HomeController(IStudentService studentService, IMapper mapper)
        {
            this._studentService = studentService;
            this._mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            List<StudentDTO> list = new();
            var response = await _studentService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null)
            {
                list = JsonConvert.DeserializeObject<List<StudentDTO>>(Convert.ToString(response.Result));
            }
            return View(list);

        }

        
    }
}
