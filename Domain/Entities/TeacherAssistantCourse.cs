using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TeacherAssistantCourse
    {
        public Guid Id { get; set; }
        public Guid TeacherAssistantId { get; set; }
        public virtual TeacherAssistant TeacherAssistant { get; set; } = null!;

        public int CourseId { get; set; }
        public virtual Course Course { get; set; } = null!;
    }
}
