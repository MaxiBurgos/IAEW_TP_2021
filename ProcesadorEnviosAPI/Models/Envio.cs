using System;

namespace ProcesadorEnviosAPI.Models
{
    public class Envio
    {
        public int Id { get; set; }

        public string DireccionOrigen { get; set; }

        public string DireccionDestino { get; set; }

        public string ContactoComprador { get; set; }

        public string EstadoEnvio { get; set; }

        public string DetalleProducto { get; set; }
        
        public int OperadorLogistico { get; set; }
    }
}