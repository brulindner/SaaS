using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } // Chave primária

        // Identifica a qual oficina o registro pertence
        public Guid TenantId { get; set; }
    }

}