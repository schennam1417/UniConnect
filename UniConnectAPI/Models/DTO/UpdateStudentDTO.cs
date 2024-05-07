using static UniConnectAPI.Enums.Constants;

namespace UniConnectAPI.Models.DTO
{
    public class UpdateStudentDTO
    {
        public string StudentName { get; set; }
        public WelshLanguageProficiency WelshLanguageProficiency { get; set; }
    }
}
