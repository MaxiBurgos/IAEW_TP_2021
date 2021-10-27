using ProcesadorEnviosAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProcesadorEnviosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnviosController : ControllerBase
    {
        [Route("{envioId}")]
        [HttpGet]
        public IActionResult GetById(long envioId)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Envio envio)
        {
            return Ok();
        }

        [Route("{envioId}/novedades")]
        [HttpPost]
        public IActionResult Create(long envioId, string novedades)
        {
            return Ok();
        }
    }
}