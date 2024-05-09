using System.ComponentModel.DataAnnotations;
using static UniConnectUtility.SD;
namespace Uniconnect.Models.DTO
{
    public class AddStudentDTO
    {
        [Required]
        public string StudentName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string UniversityCourse { get; set; }
        [Required]
        public WelshLanguageProficiency WelshLanguageProficiency { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
