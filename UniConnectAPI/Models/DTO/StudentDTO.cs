﻿using System.ComponentModel.DataAnnotations;
using static UniConnectAPI.Enums.Constants;

namespace UniConnectAPI.Models.DTO
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
