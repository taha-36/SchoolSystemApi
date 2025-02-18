using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Department
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual List<Student> Students { get; set; } = new List<Student>();
        public virtual List<DepartmentCourse> DepartmentCourses { get; set; } = new List<DepartmentCourse>();
    }
}
