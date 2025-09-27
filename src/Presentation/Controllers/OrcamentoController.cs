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
    }
}