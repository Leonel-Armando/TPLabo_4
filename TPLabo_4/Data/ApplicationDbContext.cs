using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TPLabo_4.Models;

namespace TPLabo_4.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    public DbSet<Carpinteria> carpinterias { get; set; }
    public DbSet<Calidad> calidades { get; set; }
    public DbSet<Empleado> empleados { get; set; }
    public DbSet<Ferreteria> ferreterias { get; set; }
    }
}