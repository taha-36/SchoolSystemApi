using Domain.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public int CreditHours { get; set; }
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Course> Prerequisites { get; set; } = new List<Course>();
        public virtual ICollection<DepartmentCourse> AllowedDepartments { get; set; } = null!;
        public virtual ICollection<StudentCourse> Students { get; set; } = new List<StudentCourse>();
        public virtual ICollection<TeacherCourse> Teachers { get; set; } = new List<TeacherCourse>();
        public virtual ICollection<TeacherAssistantCourse> TeachersAssistants { get; set; } = new List<TeacherAssistantCourse>();
    }
}
