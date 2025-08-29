using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enroll> Enrolls { get; set; } 

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Enroll>().HasKey(cs => new { cs.CourseId, cs.StudentId });

            builder.Entity<Enroll>().HasOne(cs => cs.Course).WithMany(c => c.Enrolls).HasForeignKey(cs => cs.CourseId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Enroll>().HasOne(cs => cs.Student).WithMany(s => s.Enrolls).HasForeignKey(cs => cs.StudentId).OnDelete(DeleteBehavior.Restrict); 

            builder.Entity<Enroll>().HasIndex(e => new { e.StudentId, e.CourseId }).IsUnique();

            builder.Entity<AppUser>().HasOne(u => u.Student).WithOne(s => s.AppUser).HasForeignKey<Student>(s => s.AppUserId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<AppUser>().HasOne(u => u.Teacher).WithOne(t => t.AppUser).HasForeignKey<Teacher>(t => t.AppUserId).OnDelete(DeleteBehavior.Restrict);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" },
                new IdentityRole { Id = "3", Name = "Student", NormalizedName = "STUDENT" },
                new IdentityRole { Id = "4", Name = "Teacher", NormalizedName = "TEACHER" }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }

    }
}