using System.Diagnostics.Tracing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ProcesadorEnviosAPI.Models;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Text.Json;
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
        [Authorize(Policy = "read:envios")]
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
        
        [HttpPost]
        [Authorize(Policy = "write:envios")]
        public async Task<ActionResult<Envio>> Create([FromBody] Envio envio)        {   
           
            var provincias = await _context.Provincias.ToListAsync();
            
            var ind = provincias.FindIndex(x => x.Nombre.Contains(envio.DireccionDestino));
            
            if(ind != -1){
                envio.OperadorLogistico = provincias[ind].OperadorLogisticoAsignado;
            }else{
                return BadRequest();
            } 
            // en funcion de la direccion de destino del envio, lo cual veo previo a meterlo, le asigno un operador logistico y en funcion de eso  voy aL SWITCH
            _context.envios.Add(envio);
            await _context.SaveChangesAsync();
            string urlApi = "";
            string token = "";
            var logisticOperator="Prueba";
            //HAY QUE HACER UNA LISTA DE PROVINCIAS EN CADA OPERADOR, Y EN FUNCION DE ESO DECIDIR SI ES NORTE O CENTRO PARA BUSCAR LA AUTH
            switch(logisticOperator) 
            {
            case "Norte":
            {
                var client = new RestClient("https://dev-282k6-68.us.auth0.com/oauth/token");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", "{\"client_id\":\"7JsyUSGSsEEqIFKypBZ46flfDpCR3qJZ\",\"client_secret\":\"iAevbkxdVw7je20OAf0ZvAuV4cZtViCGdGfGUfjVGix-hjs7yu5CdBBnP1eafHnq\",\"audience\":\"https://www.example.com/api-logistic-operator-norte\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                dynamic resp = JObject.Parse(response.Content);
                token = resp.access_token;
            }
                break;
            case "Centro":
            {
                var client = new RestClient("https://dev-iy2c6ke5.us.auth0.com/oauth/token");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", "{\"client_id\":\"EexZNbJf5rj2DPfVn8r5JeMdi4eQVFkT\",\"client_secret\":\"gfAmKArmrYwnq7Ly__UdEpZomTp2JW-zWexFOHErBFlb0rSkCghlkVmXYCLdudbD\",\"audience\":\"https://www.example.com/api-logistic-operator-center\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                dynamic resp = JObject.Parse(response.Content);
                token = resp.access_token;
            }
                break;
            case "Prueba":
            {
                //ESTE CASE SE VA LUEGO DE QUE SE CONOZCAN AMBOS ENDPOINTS DE LAS APIS DEL NORTE Y CENTRO
                var client = new RestClient("https://dev-proc-envios.us.auth0.com/oauth/token");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", "{\"client_id\":\"dLj4RGx8oemIGcZX8jsyPqJbRHI0AReW\",\"client_secret\":\"zonDuV6RdCoF7pfg3KEiQoZ4ld9S6CKo6LChRvqzATpkiwGbiq-ot1j9eZlsD4pa\",\"audience\":\"https://www.api-procesador-envios.com/\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);       
                IRestResponse response = client.Execute(request); 
                dynamic resp = JObject.Parse(response.Content);
                token = resp.access_token;
            }
                break;
            default:
                return BadRequest();
            }

            switch(logisticOperator) 
            {
            case "Norte":
            {
                urlApi = "aca va la api del norte";
            }
                break;                
            case "Centro":
            {
                urlApi = "aca va la api del centro";      
            }
                break;
            case "Prueba":
            {
               urlApi = "https://webhook.site/5181130f-4172-4b7a-beb3-5f6d044b84c4";
            }
                break;
            default:
                return BadRequest();
            }  
            var logisticOperatorClient = new RestClient(urlApi);            
            var LogisticOperatorRequest = new RestRequest(Method.POST).AddJsonBody(envio);
            LogisticOperatorRequest.AddHeader("authorization", $"Bearer {token}");          
            logisticOperatorClient.Execute(LogisticOperatorRequest);   

            return CreatedAtAction(nameof(GetAll), new { id=envio.Id }, envio); 
        }

        [Route("{envioId}/novedades")]
        [HttpPost]
        [Authorize(Policy = "write:novedades")]
        public async Task<ActionResult<Novedades>> CreateNews([FromBody] Novedades novedades)
        {

            var recordToModidy = _context.envios.Where(r => r.Id == novedades.IdEnvio).First();
            recordToModidy.EstadoEnvio = novedades.NuevoEstado;
            _context.novedades.Add(novedades);
            await _context.SaveChangesAsync();
            var client = new RestClient("https://webhook.site/5181130f-4172-4b7a-beb3-5f6d044b84c4");
            var request = new RestRequest(Method.POST).AddJsonBody(novedades);
            client.Execute(request);

            return Ok();
        }

        [Route("token")]
        [HttpGet]
        public Task<String> getToken() {
            var client = new RestClient("https://dev-proc-envios.us.auth0.com/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\"client_id\":\"6lrh04PL1gHxx5bIMaC6cQlecXVxanAB\",\"client_secret\":\"Klf7qCWy089vGO5sZLMMhNX4vn9ah4j7DAZKWJck8zWFQ5GWxoP5Cr9S5pb4Ploz\",\"audience\":\"https://www.api-procesador-envios.com/\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            dynamic resp = JObject.Parse(response.Content);
            string token = resp.access_token;
            var output = JsonSerializer.Serialize(new {token = token});
            return Task.FromResult(output);
        }
    }
}