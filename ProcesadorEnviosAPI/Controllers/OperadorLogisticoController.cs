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
    public class OperadoresLogisticosController : ControllerBase
    {
        private readonly ApiContext _context;

        public OperadoresLogisticosController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperadorLogistico>>> GetAll()
        {
            return await _context.operadoresLogisticos.ToListAsync();
        }

        [Route("{operadorLogisticoId}")]
        [HttpGet]
        [Authorize(Policy = "read:operadores")]
        public async Task<ActionResult<OperadorLogistico>> GetById(int operadorLogisticoId)
        {
            var operLogistico = await _context.operadoresLogisticos.FindAsync(operadorLogisticoId);

            if (operLogistico == null)
            {
                return NotFound();
            }

            return operLogistico;
        }

        [HttpPost]
        [Authorize(Policy = "write:operadores")]
        public async Task<ActionResult<OperadorLogistico>> Create([FromBody] OperadorLogistico operadorLogistico)
        {
            _context.operadoresLogisticos.Add(operadorLogistico);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAll), new { id=operadorLogistico.Id }, operadorLogistico);
        }

        [Route("{operadorLogisticoId}")]
        [HttpDelete]
        [Authorize(Policy = "delete:operadores")]
        public async Task<IActionResult> Delete(int operadorLogisticoId)
        {
            var operLogistico = await _context.operadoresLogisticos.FindAsync(operadorLogisticoId);
            if (operLogistico == null)
            {
                return NotFound();
            }
            
            _context.operadoresLogisticos.Remove(operLogistico);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}