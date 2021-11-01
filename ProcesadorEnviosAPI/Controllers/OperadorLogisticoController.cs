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
        public IActionResult GetById(long operadorLogisticoId)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<OperadorLogistico>> Create([FromBody] OperadorLogistico operadorLogistico)
        {
            _context.operadoresLogisticos.Add(operadorLogistico);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAll), new { id=operadorLogistico.Id }, operadorLogistico);
            //return Ok();
        }

        [Route("{operadorLogisticoId}")]
        [HttpPut]
        public IActionResult Modify(long operadorLogisticoId, [FromBody] OperadorLogistico operadorLogistico)
        {
            return Ok();
        }

        [Route("{operadorLogisticoId}")]
        [HttpDelete]
        public IActionResult Delete(long operadorLogisticoId)
        {
            return Ok();
        }
    }
}