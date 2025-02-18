using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Teacher : Staff
    {
        public virtual List<TeacherCourse> Courses { get; set; } = new List<TeacherCourse>();
    }
}
