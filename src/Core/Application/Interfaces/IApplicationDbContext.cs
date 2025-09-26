using Microsoft.EntityFrameworkCore;
using SaaS.Domain.Entities;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace SaaS.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Cliente> Clientes { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}