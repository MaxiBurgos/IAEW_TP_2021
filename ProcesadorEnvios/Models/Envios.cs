using System;

namespace ProcesadorEnvios
{
    public class Envios
    {
        public string Direccion_Origen { get; set; }

        public string Direccion_Destino { get; set; }

        public Comprador Comprador { get; set; }

        public string Estado_Envio { get; set; }

        public string Detalle_Producto { get; set; }
        
        public OperadorLogistico OperadorLogistico { get; set; }
    }
}
