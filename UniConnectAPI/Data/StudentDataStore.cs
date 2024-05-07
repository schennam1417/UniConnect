using UniConnectAPI.Models;
using UniConnectAPI.Models.DTO;

namespace UniConnectAPI.Data
{
    public static class StudentDataStore
    {
        public static List<StudentDTO> StudentList = new List<StudentDTO>
        {
            new StudentDTO{StudentID="24000000",StudentName="test",UniversityCourse="test"},
            
        };
        
    }
}
