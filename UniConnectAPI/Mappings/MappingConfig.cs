using AutoMapper;
using UniConnectAPI.Models;
using UniConnectAPI.Models.DTO;

namespace UniConnectAPI.Mappings
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Student, StudentDTO>();
            CreateMap<StudentDTO, Student>();

            CreateMap<Student, AddStudentDTO>();
            CreateMap<AddStudentDTO, Student>();
            CreateMap<Student, UpdateStudentDTO>();
            CreateMap<UpdateStudentDTO, Student>();
            
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();


        }
    }
}
