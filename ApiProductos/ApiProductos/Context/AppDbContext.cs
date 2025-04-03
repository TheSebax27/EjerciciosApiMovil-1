using ApiProductos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiProductos.Context
{
  

        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {

            }
            public DbSet<Producto> productos { get; set; }
            public DbSet<Categoria> categorias { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<Producto>()
                    .HasOne(p => p.categoria)
                    .WithMany(c => c.Productos)
                    .HasForeignKey(p => p.idCategoria);
            }
           
        }
    
}
