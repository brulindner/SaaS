using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SaaS.Presentation.Controllers
{
    [Route("api/[controller]")]
    public class OrcamentoController : Controller
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
            var orcamentos = await _appDbContext.orcamentos
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
            var orcamentos = await _appDbContext.Orcamentos
            .Include(o => o.Cliente)
            .Include(o => o.Produtos)
            .Include(o => o.Servicos)
            .Include(o => o.Venda)
            .FirstOrDefaultAsync(o => o.Id == id);

            if (orcamentos == null)
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

            return CreatedAction(nameof(GetById), new { id = orcamento.id }, orcamento);
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

            var venda = new venda
            {
                id = Guid.NewGuid(),
                OrcamentoId = orcamento.id,
                DataVenda = DateTime.UtcNow
            };

            _appDbContext.Vendas.Add(venda);
            await _appDbContext.SaveChangesAsync();

            return Ok(venda);
        }
    }
}