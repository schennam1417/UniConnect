using System.ComponentModel.DataAnnotations;
using static UniConnectAPI.Enums.Constants;

namespace UniConnectAPI.Models.DTO
{
    public class UpdateStudentDTO
    {
        [Required]
        public string StudentId { get; set; }
        [Required]
        public string StudentName { get; set; }
        [Required]
        public WelshLanguageProficiency WelshLanguageProficiency { get; set; }
    }
}
