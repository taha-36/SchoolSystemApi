using Domain.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Student : User
    {
        public virtual List<StudentCourse> Courses { get; set; } = new List<StudentCourse>();
        public Guid DepartmentId { get; set; }
        public virtual Department Department { get; set; } = null!;
        public Level Level { get; set; }
    }
}
