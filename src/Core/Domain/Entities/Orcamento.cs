using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SaaS.Domain.Common;

namespace SaaS.Domain.Entities
{
    public class Orcamento : BaseEntity
    {
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }  = null!;

        public DateTime Data { get; set; } = DateTime.UtcNow;
        public bool Fechado { get; set; } = false;
        public decimal Total { get; set; }

        public ICollection<OrcamentoProduto> Produtos { get; set; } = new List<OrcamentoProduto>();

        public ICollection<OrcamentoServico> Serviços { get; set; } = new List<OrcamentoServico>();

        public Venda? Venda { get; set; } //Um orçamento pode virar uma venda
    }
}