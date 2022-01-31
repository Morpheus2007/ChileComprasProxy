using Dt.EscritorioProxy.Backend.Interfaces.Model.Autenticacion;
using Dt.EscritorioProxy.Backend.Interfaces.Repository.Autenticacion;
using Dt.EscritorioProxy.Backend.Interfaces.Service;

namespace Dt.EscritorioProxy.Backend.Service
{
    public class AutenticacionService: IAutenticacionService
    {
        private readonly IAutenticacionClaveUnicaRepository _autenticacionClaveUnicaRepository;

        public AutenticacionService(IAutenticacionClaveUnicaRepository autenticacionClaveUnicaRepository)
        {
            _autenticacionClaveUnicaRepository = autenticacionClaveUnicaRepository;
        }


        public void RegistrarClaveUnica(ClaveUnicaInfo claveUnicaInfo)
        {

            _autenticacionClaveUnicaRepository.Insert(claveUnicaInfo);
        }

        public ClaveUnicaInfo GetClaveUnicaInfoByToken(string token)
        {
            return _autenticacionClaveUnicaRepository.GetClaveUnicaInfoByToken(token);
        }


        public void ActualizarClaveUnica(ClaveUnicaInfo claveUnicaInfo)
        {
            _autenticacionClaveUnicaRepository.Update(claveUnicaInfo);
        }

    }
}
