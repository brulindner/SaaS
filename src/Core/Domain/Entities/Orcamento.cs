using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SaaS.Domain.Common;

namespace SaaS.Domain.Entities
{
    public class Orcamento : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;

        public DateTime Data { get; set; } = DateTime.UtcNow;
        public bool Fechado { get; set; } = false;

        public ICollection<OrcamentoServico> Servicos { get; set; } = new List<OrcamentoServico>();
        public ICollection<OrcamentoProduto> Produtos { get; set; } = new List<OrcamentoProduto>();

        public Venda? Venda { get; set; } //Um orçamento pode virar uma venda

            public decimal Total =>
            (Produtos?.Sum(op => op.Quantidade * op.PrecoUnitario) ?? 0) +
            (Servicos?.Sum(os => os.Quantidade * os.Valor) ?? 0);

    }
}