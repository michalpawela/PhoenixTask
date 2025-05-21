using DbManagement.Entities;
using DbManagement;
using Microsoft.EntityFrameworkCore;

namespace DbManagement
{
    public class PhoenixDbContext : DbContext
    {
        
        public DbSet<Employee> Employees { get; set; }


        public PhoenixDbContext(DbContextOptions<PhoenixDbContext> options)
            : base(options)
        {
        }


    }
}
