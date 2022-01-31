using System.Linq;
using Dt.EscritorioProxy.Backend.Interfaces.Model.Autenticacion;
using Dt.EscritorioProxy.Backend.Interfaces.Repository.Autenticacion;
using Dt.EscritorioProxy.Backend.Repository.Base;
using Dt.EscritorioProxy.Backend.Repository.Context;

namespace Dt.EscritorioProxy.Backend.Repository.Autenticacion
{
    public class AutenticacionClaveUnicaRepository:BaseRepository<ClaveUnicaInfo>, IAutenticacionClaveUnicaRepository
    {
        public AutenticacionClaveUnicaRepository(PortalInstitucionalContext context) : base(context)
        {
            
        }


        public ClaveUnicaInfo GetClaveUnicaInfoByToken(string token)
        {
            return Entities.FirstOrDefault(x => x.Token == token);
        }
    }
}
