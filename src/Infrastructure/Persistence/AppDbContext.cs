using Microsoft.EntityFrameworkCore;
using SaaS.Application.Interfaces;
using SaaS.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SaaS.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IApplicationDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSets = tabelas no banco
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Orcamento> Orcamentos { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<Produto> Produtos { get; set; }


        // Implementa a interface
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        // Se quiser configurar mapeamentos fluent
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Clientes -> Orçamentos
            modelBuilder.Entity<Orcamento>()
          .HasOne(o => o.Cliente)
          .WithMany(c => c.Orcamentos) //Cliente tem lista de orçamentos
          .HasForeignKey(o => o.ClienteId)
          .OnDelete(DeleteBehavior.Restrict);

            //Orçamento -> Venda 
            modelBuilder.Entity<Venda>()
          .HasOne(v => v.Orcamento)
          .WithOne(o => o.Venda)
          .HasForeignKey<Venda>(v => v.OrcamentoId)
          .OnDelete(DeleteBehavior.Restrict);

            //Orçamento + serviços
            modelBuilder.Entity<Servico>()
            .HasOne(s => s.Orcamento)
          .WithMany(o => o.Servicos)
          .HasForeignKey(s => s.OrcamentoId)
          .OnDelete(DeleteBehavior.Cascade);

          // Orçamento → Produtos (1:N)
            modelBuilder.Entity<Produto>()
                .HasOne(p => p.Orcamento)
                .WithMany(o => o.Produtos)
                .HasForeignKey(p => p.OrcamentoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

