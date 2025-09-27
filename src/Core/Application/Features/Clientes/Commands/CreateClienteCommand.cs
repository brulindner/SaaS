using MediatR;
using SaaS.Application.Interfaces;
using SaaS.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Clientes.Commands
{
    public class CreateClienteCommand : IRequest<Guid>
    {
        public string Nome { get; set; } = null!;
        public string Cpf { get; set; }  = null!;
        public string Telefone { get; set; }  = null!;
        public string Email { get; set; }  = null!;
    }

    public class CreateClienteCommandHandler : IRequestHandler<CreateClienteCommand, Guid>
    {
        private readonly IApplicationDbContext _appDbContext;
        private readonly ITenantService _tenantService;

      public CreateClienteCommandHandler(IApplicationDbContext appDbContext, ITenantService tenantService)
        {
            _appDbContext = appDbContext;
            _tenantService = tenantService;
        }

        public async Task<Guid> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
        {
            // 🛑 Lógica de Segurança 🛑
        var tenantId = _tenantService.GetTenantId();
        if (tenantId == Guid.Empty)
        {
            // Lançar exceção de não-autorizado (será capturada pela API)
            throw new UnauthorizedAccessException("Usuário não autenticado ou TenantId inválido."); 
        }

            var cliente = new Cliente
            {
                Nome = request.Nome,
                Cpf = request.Cpf,
                Telefone = request.Telefone,
                Email = request.Email,
                TenantId = tenantId
            };

            _appDbContext.Clientes.Add(cliente);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return cliente.Id;
        }
    }
}