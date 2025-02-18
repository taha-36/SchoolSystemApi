using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Staff> Staff {  get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherAssistant> TeacherAssistants { get; set; }
        public DbSet<SystemUser> SystemUsers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<StudentCourse> StudentsCourses { get; set; }
        public DbSet<TeacherCourse> TeachersCourses  { get; set; }
        public DbSet<TeacherAssistantCourse> TeacherAssistantsCourses { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<User>()
                .HasOne(x => x.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(x => x.RoleId);
            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("UserType")
                .HasValue<Student>("Student")
                .HasValue<Staff>("Staff")
                .HasValue<SystemUser>("SystemUser");
            
            modelBuilder.Entity<Role>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(x => x.Course)
                .WithMany(c => c.Students)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<StudentCourse>()
                .HasOne(x => x.Student)
                .WithMany(s => s.Courses)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Department)
                .WithMany(d => d.Students)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TeacherCourse>()
                .HasOne(x => x.Teacher)
                .WithMany(t => t.Courses)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TeacherCourse>()
                .HasOne(x => x.Course)
                .WithMany(c => c.Teachers)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TeacherAssistantCourse>()
                .HasOne(x => x.TeacherAssistant)
                .WithMany(t => t.Courses)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TeacherAssistantCourse>()
                .HasOne(x => x.Course)
                .WithMany(c => c.TeachersAssistants)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Prerequisites)
                .WithMany()
                .UsingEntity(j => j.ToTable("CoursePrerequisites"));
        }
    }
}
