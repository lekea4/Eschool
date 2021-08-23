using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eschool.Models
{
    public class CourseTeacher
    {
        public int CourseId { get; set; }

        public long TeacherId { get; set; }

        public Course AssignedCourse { get; set; }

        public Teacher AssignedTeacher { get; set; }
    }
}
