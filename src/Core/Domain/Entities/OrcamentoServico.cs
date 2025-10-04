using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SaaS.Domain.Common;

namespace SaaS.Domain.Entities
{
    public class OrcamentoServico : BaseEntity
    {
        public Guid OrcamentoId { get; set; }
        public Orcamento Orcamento { get; set; } = null!;

        public Guid ServicoId { get; set; }
        public Servico Servico { get; set; } = null!;

        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
    }
}