using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Policy = "read:envios")]
        public async Task<ActionResult<Envio>> GetById (long envioId)
        {
            var envio = await _context.envios.FindAsync(envioId);

            if (envio == null)
            {
                return NotFound();
            }
            return envio;
        }

        [Route("{envioId}")]
        [HttpPost]
        [Authorize(Policy = "write:envios")]
        public async Task<ActionResult<Envio>> Create([FromBody] Envio envio)
        {
            _context.envios.Add(envio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAll), new { id=envio.Id }, envio);
        }

        [Route("{envioId}/novedades")]
        [HttpPost]
        [Authorize(Policy = "write:novedades")]
        public IActionResult Create(long envioId, string novedades)
        {
            return Ok();
        }
    }
}