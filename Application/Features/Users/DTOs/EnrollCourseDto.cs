using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.DTOs
{
    public class EnrollCourseDto
    {
        public int CourseId { get; set; }
        public Group Group { get; set; }
    }
}
