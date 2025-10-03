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

    public class OrcamentoController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public OrcamentoController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //GET api/orçamentos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orcamentos = await _appDbContext.Orcamentos
                .Include(o => o.Cliente)
                .Include(o => o.Produtos)
                .Include(o => o.Servicos)
                .Include(o => o.Venda)
                .ToListAsync();

            return Ok(orcamentos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var orcamento = await _appDbContext.Orcamentos
                .Include(o => o.Cliente)
                .Include(o => o.Produtos)
                .Include(o => o.Servicos)
                .Include(o => o.Venda)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (orcamento == null)
                return NotFound("Orçamento não encontrado");

            return Ok(orcamento);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Orcamento orcamento)
        {
            if (orcamento == null)
                return BadRequest("Dados inválidos");

            _appDbContext.Orcamentos.Add(orcamento);
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = orcamento.Id }, orcamento);
        }

        [HttpPost("{id}/fechar")]
        public async Task<IActionResult> Fechar(Guid id)
        {
            var orcamento = await _appDbContext.Orcamentos
                .Include(o => o.Venda)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (orcamento == null)
                return NotFound("Orçamento não encontrado");

            if (orcamento.Venda != null)
                return BadRequest("Este orçamento já foi fechado em uma venda");

            var venda = new Venda
            {
                Id = Guid.NewGuid(),
                OrcamentoId = orcamento.Id,
                Data = DateTime.UtcNow,
                ValorTotal = orcamento.Total,
                FormaPagamento = "A definir"
            };

            _appDbContext.Vendas.Add(venda);
            await _appDbContext.SaveChangesAsync();

            return Ok(venda);
        }
    }
}
