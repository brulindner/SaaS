using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SaaS.Domain.Common;

namespace SaaS.Domain.Entities
{
    public class Venda : BaseEntity
    {
        public int OrcamentoId { get; set; }
        public Orcamento Orcamento { get; set; } = null!;

        public DateTime Data { get; set; } = DateTime.UtcNow;
        public decimal ValorTotal { get; set; }
        public string FormaPagamento { get; set; } = string.Empty;

    }
}