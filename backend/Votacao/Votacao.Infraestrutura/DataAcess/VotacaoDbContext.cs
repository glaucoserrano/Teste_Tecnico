using Microsoft.EntityFrameworkCore;
using Votacao.Dominio.Entities;

namespace Votacao.Infraestrutura.DataAcess
{
    public class VotacaoDbContext : DbContext
    {
        public VotacaoDbContext(DbContextOptions<VotacaoDbContext> options) : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Restaurante> Restaurantes { get; set; }
        public DbSet<Voto> Votos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Relacionamento Voto Usuario
            modelBuilder.Entity<Voto>()
                .HasOne(voto => voto.Usuario)
                .WithMany()
                .HasForeignKey(voto => voto.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            //Relacionamento Voto Restaurante
            modelBuilder.Entity<Voto>()
                .HasOne(voto => voto.Restaurante)
                .WithMany()
                .HasForeignKey(voto => voto.RestauranteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
