using AutoMapper;
using UniConnectAPI.Models;
using UniConnectAPI.Models.DTO;

namespace UniConnectAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Student, StudentDTO>();
            CreateMap<StudentDTO,Student>();

            CreateMap<Student, AddStudentDTO>().ReverseMap();
            CreateMap<StudentDTO, UpdateStudentDTO>().ReverseMap();

        }
    }
}
