using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProcesadorEnviosAPI.Models
{
    public class Envio
    {
        public int Id { get; set; }

        public string Direccion_Origen { get; set; }

        public string Direccion_Destino { get; set; }

        public string Contacto_Comprador { get; set; }

        public Estado Estado { get; set; }
        

        public string Detalle_Producto { get; set; }
        
        public int Id_Operador_Logistico { get; set; }
    }
}