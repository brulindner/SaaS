using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SaaS.Domain.Common;

namespace SaaS.Domain.Entities
{
    public class Produto : BaseEntity
    {
        public string Nome { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public decimal Custo { get; set; }
        public decimal PrecoVenda { get; set; }
        public int QuantidadeEstoque { get; set; }

        public Guid OrcamentoId { get; set; }
        public Orcamento Orcamento { get; set; } = null!;

        public ICollection<OrcamentoProduto> OrcamentoProdutos { get; set; } = new List<OrcamentoProduto>();
    }
}