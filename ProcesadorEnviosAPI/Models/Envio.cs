using System;

namespace ProcesadorEnviosAPI.Models
{
    public class Envio
    {
        public long Id { get; set; }

        public string Direccion_Origen { get; set; }

        public string Direccion_Destino { get; set; }

        public string ContactoComprador { get; set; }

        public string Estado_Envio { get; set; }

        public string Detalle_Producto { get; set; }
        
        public OperadorLogistico OperadorLogistico { get; set; }
    }
}