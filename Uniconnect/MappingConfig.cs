using AutoMapper;
using Uniconnect.Models.DTO;
using UniConnect.Models;
using UniConnect.Models.DTO;

namespace UniConnectAPI.Mappings
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<AddStudentDTO, StudentDTO>();
            CreateMap<StudentDTO, AddStudentDTO>();
            CreateMap<StudentDTO, UpdateStudentDTO>();
            CreateMap<UpdateStudentDTO, StudentDTO>();
            

        }
    }
}
