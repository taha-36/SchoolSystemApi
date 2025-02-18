using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.DTOs
{
    public class StudentEnrollCourseDto
    {
        [Required]
        public List<EnrollCourseDto> Courses { get; set; } = new List<EnrollCourseDto>();
    }
}
