using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SaaS.Domain.Entities;
using SaaS.Infrastructure.Persistence;



namespace SaaS.Presentation.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class ClienteController : ControllerBase

    {
        private readonly AppDbContext _appDbContext;

        public ClienteController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clientes = await _appDbContext.Clientes
            .Include(c => c.Orcamentos) //traz os orçamentos do cliente
            .ToListAsync();

            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var cliente = await _appDbContext.Clientes
            .Include(c => c.Orcamentos)
            .ThenInclude(o => o.Servicos) //dentro do orçamento, traz serviços
            .Include(c => c.Orcamentos)
            .ThenInclude(o => o.Produtos)
            .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente == null)
                return NotFound("Cliente não encontrado");

            return Ok(cliente);
        }

            [HttpPost]
        public async Task<IActionResult> Create([FromBody] Cliente cliente)
        {
            if (cliente == null)
                return BadRequest("Dados inválidos");

            _appDbContext.Clientes.Add(cliente);
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Cliente clienteAtualizado)
        {
            var cliente = await _appDbContext.Clientes.FindAsync(id);
            if (cliente == null)
                return NotFound("Cliente não encontrado");

            cliente.Nome = clienteAtualizado.Nome;
            cliente.Cpf = clienteAtualizado.Cpf;
            cliente.Telefone = clienteAtualizado.Telefone;
            cliente.Email = clienteAtualizado.Email;

            _appDbContext.Clientes.Update(cliente);
            await _appDbContext.SaveChangesAsync();

            return Ok(cliente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var cliente = await _appDbContext.Clientes.FindAsync(id);
            if (cliente == null)
                return NotFound("Cliente não encontrado");

            _appDbContext.Clientes.Remove(cliente);
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }
        }
    }
