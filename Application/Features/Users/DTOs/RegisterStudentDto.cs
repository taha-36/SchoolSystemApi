﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.DTOs
{
    public class RegisterStudentDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public Guid DepartmentId { get; set; }
    }
    public enum Title
    {
        Student,
        Teacher,
        TeacherAssistant,
        Other
    }
}
