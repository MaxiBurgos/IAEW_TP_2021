using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcesadorEnviosAPI.Models
{
    public class MariaDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public MariaDbContext(DbContextOptions<MariaDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<OperadorLogistico> operadoresLogisticos { get; set; }
    }
}

