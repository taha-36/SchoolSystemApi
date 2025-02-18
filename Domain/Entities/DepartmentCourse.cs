using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DepartmentCourse
    {
        public Guid Id { get; set; }
        public Guid DepartmentId { get; set; }
        public virtual Department Department { get; set; } = null!;

        public int CourseId { get; set; }
        public virtual Course Course { get; set; } = null!;
    }
}
