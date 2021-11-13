using System.Data.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProcesadorEnviosAPI.Models
{
    public class Provincias
    {
        [Key]
        public string Nombre { get; set; }
        
        [Required]
        public int OperadorLogisticoAsignado { get; set; }
    }
}