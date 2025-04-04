using appApiTareass.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Threading;

namespace appApiTareass.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarea>()
                .HasOne(t => t.UsuarioT)  // Una tarea tiene un usuario
                .WithMany(u => u.TareasU) // Un usuario tiene muchas tareas
                .HasForeignKey(t => t.IdUsuario);
        }

    }
}
