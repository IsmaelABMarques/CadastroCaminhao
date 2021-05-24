using CadastroCaminhao.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace CadastroCaminhao.Data.Repository
{
    public class RepositoryBase : DbContext
    {
        public RepositoryBase(DbContextOptions<RepositoryBase> options)
            : base(options)
        {
        }

        public DbSet<Caminhao> Caminhao { get; set; }
        public DbSet<Modelo> Modelo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Caminhao>().HasOne(c => c.Modelo).WithMany(m => m.Caminhoes).HasForeignKey(c => c.ModeloId);
        }

    }

    public class RepositoryBaseTest : DbContext
    {
        public RepositoryBaseTest(DbContextOptions<RepositoryBaseTest> options)
            : base(options)
        {
        }

        public DbSet<Caminhao> Caminhao { get; set; }
        public DbSet<Modelo> Modelo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Caminhao>().HasOne(c => c.Modelo).WithMany(m => m.Caminhoes).HasForeignKey(c => c.ModeloId);
        }

    }
}
