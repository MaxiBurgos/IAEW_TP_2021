using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcesadorEnviosAPI.Models
{
    public class ApiContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<OperadorLogistico> operadoresLogisticos { get; set; }
        public virtual DbSet<Envio> envios { get; set; }
        public virtual DbSet<Novedades> novedades { get; set; }
        public virtual DbSet<Provincias> Provincias { get; set; }        

    }
}