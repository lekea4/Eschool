using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Eschool.Models
{
    public class EschoolContext : DbContext
    {

        public EschoolContext(DbContextOptions<EschoolContext> options) : base (options)
        {   
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseTeacher>()
                .HasKey(ct => new { ct.TeacherId, ct.CourseId});
        }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<CourseTeacher> CourseTeachers { get; set; }
    }
}
