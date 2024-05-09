using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static UniConnectAPI.Enums.Constants;

namespace UniConnectAPI.Models
{
    public class Student
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
