using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Net;
using UniConnectAPI.Data;
using UniConnectAPI.Models;
using UniConnectAPI.Models.DTO;
using UniConnectAPI.Repository;
using UniConnectAPI.Repository.IRepository;

namespace UniConnectAPI.Controllers.v2
{

    [Route("api/v{version:apiVersion}/StudentAPI")]
    [ApiController]

    [ApiVersion("2.0")]
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
        //  [MapToApiVersion("2.0")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }



    }
}
