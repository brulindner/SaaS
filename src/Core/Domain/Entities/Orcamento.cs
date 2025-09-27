using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SaaS.Domain.Common;

namespace SaaS.Domain.Entities
{
    public class Orcamento : BaseEntity
    {
        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; }  = null!;

        public DateTime Data { get; set; } = DateTime.UtcNow;
        public bool Fechado { get; set; } = false;
        public decimal Total { get; set; }

        public ICollection<Servico> Servicos { get; set; } = new List<Servico>();
        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();

        public Venda? Venda { get; set; } //Um orçamento pode virar uma venda
    }
}