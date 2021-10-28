using Microsoft.EntityFrameworkCore;

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

