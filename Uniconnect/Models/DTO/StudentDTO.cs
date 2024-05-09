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
