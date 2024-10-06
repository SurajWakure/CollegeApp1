using Collegeapp1.Data.Config;
using Microsoft.EntityFrameworkCore;

namespace Collegeapp1.Data
{
    public class CollegeDbContext:DbContext
    {
        public CollegeDbContext( DbContextOptions<CollegeDbContext> options):base (options)
        {
            
        }
        public DbSet<Student> students { get; set; }
        public DbSet<Department> departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.ApplyConfiguration(new StudentConfig());

            modelBuilder.ApplyConfiguration(new DepartmentConfig());
            //iff there is new table you want to addd then you need to add the new linne here
            //
        }
    }
}
