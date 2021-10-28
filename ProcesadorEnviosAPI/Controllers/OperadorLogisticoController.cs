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
        private readonly MariaDbContext _context;

        public OperadoresLogisticosController(MariaDbContext context)
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
        public IActionResult Create([FromBody] OperadorLogistico operadorLogistico)
        {
            return Ok();
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