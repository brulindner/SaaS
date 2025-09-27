using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SaaS.Domain.Common;

namespace SaaS.Domain.Entities
{
    public class Servico : BaseEntity
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }

        public Guid OrcamentoId { get; set; }
        public Orcamento Orcamento { get; set; } = null!;

        public ICollection<OrcamentoServico> OrcamentoServicos { get; set; } = new List<OrcamentoServico>();
    }
}