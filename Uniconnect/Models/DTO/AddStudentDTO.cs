using System.ComponentModel.DataAnnotations;
using static UniConnectUtility.SD;
namespace Uniconnect.Models.DTO
{
    public class AddStudentDTO
    {
        [Required]
        public string StudentName { get; set; }
        [Required]
        public string DateOfBirth { get; set; }
        [Required]
        public string UniversityCourse { get; set; }
        [Required]
        public WelshLanguageProficiency WelshLanguageProficiency { get; set; }
        [Required]
        public string StartDate { get; set; }
        [Required]
        public string EndDate { get; set; }
    }
}
