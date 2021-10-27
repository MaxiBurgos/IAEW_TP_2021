using ProcesadorEnviosAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProcesadorEnviosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperadoresLogisticosController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
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