using StudentDiaryWPF.Models.Configurations;
using StudentDiaryWPF.Models.Domains;
using System;
using System.Data.Entity;
using System.Linq;

namespace StudentDiaryWPF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("name=ApplicationDbContext")
        {
        }

        public ApplicationDbContext(string connectionString)
            : base(connectionString)
        {   
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StudentConfiguration());
            modelBuilder.Configurations.Add(new RatingConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration()); 
        }
    }
}
