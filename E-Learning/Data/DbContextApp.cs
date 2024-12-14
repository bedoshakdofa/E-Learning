using E_Learning.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Data
{
    public class DbContextApp:DbContext
    {
        public DbContextApp(DbContextOptions options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasOne(Dept => Dept.Department)
                .WithMany(u => u.Users)
                .HasForeignKey(fk => fk.Dept_id)
                .IsRequired(false);

            modelBuilder.Entity<Course>()
                .HasOne(dept => dept.department)
                .WithMany(c => c.courses)
                .HasForeignKey(fk => fk.Dept_Id_FK);
                

            modelBuilder.Entity<Enrollment>().HasKey(pk => new { pk.Course_ID, pk.User_ID });

            modelBuilder.Entity<Enrollment>()
                .HasOne(c => c.Course)
                .WithMany(e => e.enrollment)
                .HasForeignKey(fk => fk.Course_ID);


            modelBuilder.Entity<Enrollment>()
                .HasOne(u => u.user)
                .WithMany(e => e.Enrollment)
                .HasForeignKey(fk => fk.User_ID);


            modelBuilder.Entity<Lecture>()
                .HasOne(c => c.Course)
                .WithMany(l => l.Lecture)
                .HasForeignKey(FK => FK.Course_ID);

            modelBuilder.Entity<User>()
                .HasIndex(x => x.Email)
                .IsUnique();
               

        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Lecture>lectures { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }
    }
}
