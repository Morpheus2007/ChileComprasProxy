using Dt.EscritorioProxy.Backend.Interfaces.Model.Autenticacion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dt.EscritorioProxy.Backend.Repository.Mapping.Autenticacion
{
    public class AutenticacionClaveUnicaMap
    {

        public AutenticacionClaveUnicaMap(EntityTypeBuilder<ClaveUnicaInfo> entityBuilder)
        {
            entityBuilder.HasKey(t => t.ClaveUnicaInfoId);
            entityBuilder.Property(t => t.ClaveUnicaInfoId).HasColumnName("AutenticacionClaveUnicaId").IsRequired();
            entityBuilder.Property(t => t.ClaveUnicaInfoId).UseSqlServerIdentityColumn();
            entityBuilder.Property(t => t.Token).HasColumnName("Token").IsRequired();
            entityBuilder.Property(t => t.TokenAcceso).HasColumnName("TokenAcceso");
            entityBuilder.Property(t => t.Callback).HasColumnName("Callback").IsRequired();
            entityBuilder.Property(t => t.IsValido).HasColumnName("Valido");
            entityBuilder.Property(t => t.FechaRegistro).HasColumnName("FechaRegistro").IsRequired();
            entityBuilder.Property(t => t.Ip).HasColumnName("Ip");
            
            entityBuilder.ToTable("EI_AutenticacionClaveUnica");
        }
    }
}
