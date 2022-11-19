using demys_universidade.Domain.Entities;
using demys_universidade.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace demys_universidade.Infrastructure.Contexts
{
    public class UniversidadeContext : DbContext
    {
        public UniversidadeContext(DbContextOptions<UniversidadeContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Perfil> Perfis { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(new UsuarioMap().Configure);
            modelBuilder.Entity<Curso>(new CursoMap().Configure);
            modelBuilder.Entity<Departamento>(new DepartamentoMap().Configure);

        }

    }
}
