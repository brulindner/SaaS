using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using SaaS.Application.Interfaces;

namespace SaaS.Infrastructure.Services
{
    public class TenantService : ITenantService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        // O nome do Claim (o valor guardado no token JWT) que armazena o TenantId
        public const string TenantClaimType = "tenant_id";

        public TenantService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetTenantId()
        {
            // O HttpContext contém informações sobre a requisição atual.
            var httpContext =  _httpContextAccessor.HttpContext;

            // 1. Verifica se existe um usuário autenticado
            if (httpContext?.User?.Identity?.IsAuthenticated == true)
            {

                // 2. Procura o 'claim' (informação) que guardamos no Token JWT
                var tenantClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == TenantClaimType);

                // 3. Se o claim existir e for um GUID válido, retorna o ID da Oficina
                if (tenantClaim != null && Guid.TryParse(tenantClaim.Value, out var tenantId))
                {
                    return tenantId;
                }
            }

            // Caso não esteja logado ou o ID não foi encontrado, retornamos Guid.Empty
            // IMPORTANTE: Isso forçará o Query Filter do DbContext a retornar ZERO resultados, 
            // garantindo que dados não sejam vazados.

            return Guid.Empty;
        }
    }
}