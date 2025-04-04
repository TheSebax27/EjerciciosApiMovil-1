using ApiSensores.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiSensores.Context
{
    public class AppDbContext : DbContext 
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<AlertaSonido> AlertaSonidos { get; set; }
        public DbSet<AlertaGPS> AlertaGPSS { get; set; }
        public DbSet<AlertaNivel> AlertaNiveles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlertaSonido>().ToTable("AlertaSonido");
            modelBuilder.Entity<AlertaGPS>().ToTable("AlertaGPS");
            modelBuilder.Entity<AlertaNivel>().ToTable("AlertaNivel");
        }
    }
}
