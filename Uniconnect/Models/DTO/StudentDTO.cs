using static UniConnectUtility.SD;
using System.ComponentModel.DataAnnotations;
namespace Uniconnect.Models.DTO
{
    public class StudentDTO
    {
        public string StudentID { get; set; }
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
