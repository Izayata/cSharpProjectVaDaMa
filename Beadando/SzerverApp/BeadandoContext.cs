using Beadando.Contract;
using Microsoft.EntityFrameworkCore;

namespace SzerverApp
{
    public class BeadandoContext : DbContext
    {
        
        public BeadandoContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<Job> Jobs { get; set; }

    }
}
