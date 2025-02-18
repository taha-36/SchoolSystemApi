using Domain.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class StudentCourse
    {
        public Guid Id { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public Group Group { get; set; }

        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; } = null!;

        public int CourseId { get; set; }
        public virtual Course Course { get; set; } = null!;

        public CourseStatus CourseStatus { get; set; }
        public int CourseMark { get; set; }
    }
    public enum Group
    {
        Group1,
        Group2,
        Group3,
        Group4
    }
}
