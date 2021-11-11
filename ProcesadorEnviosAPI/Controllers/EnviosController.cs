using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using ProcesadorEnviosAPI.Models;

namespace ProcesadorEnviosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnviosController : ControllerBase
    {
        private readonly ApiContext _context;

        public EnviosController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Envio>>> GetAll()
        {
            return await _context.envios.ToListAsync();
        }

        [Route("{envioId}")]
        [HttpGet]
        public IActionResult GetById(long envioId)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Envio>> Create([FromBody] Envio envio)
        {
            _context.envios.Add(envio);
            await _context.SaveChangesAsync();
            //envio = await _context.envios.Include(x => x.Id_estado).FindAsync(envio.id_estado);

            return CreatedAtAction(nameof(GetAll), new { id=envio.Id }, envio);
        }

        [Route("{envioId}/novedades")]
        [HttpPost]
        public IActionResult Create(long envioId, string novedades)
        {
            return Ok();
        }
    }
}