using BackEnd.Model;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options):base(options) { }

        public DbSet<PessoaFisicaModel> Pessoa_Fisica { get; set; }
        public DbSet<PessoaJuridicaModel> Pessoa_Juridica { get; set; }
        public DbSet<TelefoneModel> Telefones { get; set; }
        public DbSet<EmpresaModel> Empresa_Model { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<PessoaJuridicaModel>()
                .HasIndex(p => p.CNPJ)
                .IsUnique();

            modelBuilder.Entity<PessoaFisicaModel>()
                .HasIndex(p => p.CPF)
                .IsUnique();

            modelBuilder.Entity<PessoaFisicaModel>()
                .HasIndex(p => p.RG)
                .IsUnique();

            
                

        }
    }
}
