using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TeacherCourse
    {
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; } = null!;

        public int CourseId { get; set; }
        public virtual Course Course { get; set; } = null!;
    }
}
