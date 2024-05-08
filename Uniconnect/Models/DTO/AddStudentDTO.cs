using static UniConnectUtility.SD;
namespace Uniconnect.Models.DTO
{
    public class AddStudentDTO
    {
        public string StudentName { get; set; }
        public string DateOfBirth { get; set; }

        public int Occupancy { get; set; }
        public string UniversityCourse { get; set; }
        public WelshLanguageProficiency WelshLanguageProficiency { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
