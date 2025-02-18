using Domain.Repos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.DTOs
{
    public class AddCourseDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int CreditHours { get; set; }
        [Required]
        public List<Guid> AllowedDepartmentsId { get; set; } = new List<Guid>();
        public List<int>? PrerequisitesId { get; set; }
    }
}
