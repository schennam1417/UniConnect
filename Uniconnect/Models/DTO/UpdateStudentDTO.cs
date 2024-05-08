using System.ComponentModel.DataAnnotations;
using static UniConnectUtility.SD;

namespace UniConnect.Models.DTO
{
    public class UpdateStudentDTO
    {
        [Required]
        public string StudentID { get; set;}
        [Required]
        public string StudentName { get; set; }
        [Required]
        public WelshLanguageProficiency WelshLanguageProficiency { get; set; }
    }
}
