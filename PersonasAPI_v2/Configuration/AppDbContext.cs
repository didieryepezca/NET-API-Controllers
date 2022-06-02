using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PersonasAPI_v2.Entities;

namespace PersonasAPI_v2.Configuration
{
    public class AppDbContext : DbContext
    {    
        public AppDbContext(DbContextOptions options) : base(options)
        {           
        }

        public AppDbContext()
        {            
            //Database.SetCommandTimeout(0);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public virtual DbSet<Productos> Productos { get; set; }
        public virtual DbSet<ProductoCategoria> ProductoCategoria { get; set; }

    }
}
