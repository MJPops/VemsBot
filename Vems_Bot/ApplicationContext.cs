using Microsoft.EntityFrameworkCore;
using Vems_Bot.Models;

namespace HelloApp
{
    public class ApplicationContext : DbContext
    {
        public DbSet<VemsUser> Users { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=VemsUsers;Trusted_Connection=True;");
        }
    }
}
