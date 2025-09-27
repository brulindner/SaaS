using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SaaS.Domain.Common;

namespace SaaS.Domain.Entities
{
    public class OrcamentoProduto : BaseEntity
    {
        public int OrcamentoId { get; set; }
        public Orcamento Orcamento { get; set; } = null!;

        public int ProdutoId { get; set; }
        public Produto Produto { get; set; } = null!;

        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}