using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Entrega_Act4.Models
{
    public class EnvioContext: DbContext
    {
        public EnvioContext(DbContextOptions options) : base(options) { }

        public DbSet<Envio> Envios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<DetalleEnvio> DetalleEnvios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DetalleEnvio>(entity =>
            {
                entity.HasKey(de => de.Id);

                entity.HasOne(de => de.EnvioNavegation)
                .WithMany(e => e.DetalleEnviosNavegation)
                .HasForeignKey(p => p.IdEnvio);

                modelBuilder.Entity<DetalleEnvio>()
                .HasOne(d => d.ProductoNavegation)
                .WithMany(p => p.DetalleEnvioNavegation)
                .HasForeignKey(d => d.IdProducto);
            });
                
            modelBuilder.Entity<Envio>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasMany(e => e.DetalleEnviosNavegation)
                .WithOne(de => de.EnvioNavegation)
                .HasForeignKey(de => de.IdEnvio);
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.HasMany(p => p.DetalleEnvioNavegation)
                .WithOne(de => de.ProductoNavegation)
                .HasForeignKey(de => de.IdProducto);
            });
        }
    }
}
