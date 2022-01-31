using Dt.EscritorioProxy.Backend.Interfaces.Model.Autenticacion;
using Dt.EscritorioProxy.Backend.Repository.Mapping.Autenticacion;
using Microsoft.EntityFrameworkCore;

namespace Dt.EscritorioProxy.Backend.Repository.Context
{
    public class PortalInstitucionalContext : DbContext
    {
        public PortalInstitucionalContext(DbContextOptions<PortalInstitucionalContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new AutenticacionClaveUnicaMap(modelBuilder.Entity<ClaveUnicaInfo>());
        }
    }
}
