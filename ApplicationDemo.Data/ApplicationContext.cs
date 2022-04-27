using ApplicationDemo.Domain;
using Microsoft.EntityFrameworkCore;

namespace ApplicationDemo.Data
{
    public class ApplicationContext : DbContext

    {
        public DbSet<Applicant>? Applicants { get; set; }

        public DbSet<ComputerSkills>? ComputerSkills { get; set; }

        public DbSet<JobSkills>? JobSkills { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source= (localdb)\\MSSQLLocalDb; Initial Catalog=ApplicationDemoData_Final1");

        }

    }
}
