using AutoMapper;
using ChileComprasProxy.Backend.Interfaces.Dto;
using ChileComprasProxy.Backend.Interfaces.Interface;
using ChileComprasProxy.Backend.Interfaces.Model;
using ChileComprasProxy.Framework.Utilities.Errors;
using ChileComprasProxy.Framework.Utilities.Exceptions;
using ChileComprasProxy.Framework.Utilities.Json;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;


namespace ChileComprasProxy.Backend.ServiceProxy
{
    public class CountryProxy : ICountryProxy
    {
        private readonly string _covid19ApiEndPoint;
        public IMapper _mapper;

        public CountryProxy(IConfiguration configuration, IMapper mapper)
        {
            _covid19ApiEndPoint = configuration["ExternalApi:Covid19Api"];
            _mapper = mapper;
        }

      

        public async Task<IList<CountrySummaryDto>> GetCountrySummary()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
            {

                client.BaseAddress = new Uri(_covid19ApiEndPoint);

                var response = await client.GetAsync($"summary")
                    .ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var countriesCovidSummary = await JsonUtility.ConvertResponseToObject<CountryCovidSummary>(response);

                   return _mapper.Map<IList<CountrySummaryDto>>(countriesCovidSummary.Countries);

                }
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    EntityNotFoundErrorMessage error =
                        await JsonUtility.ConvertResponseToObject<EntityNotFoundErrorMessage>(response);
                    throw new EntityNotFoundException(error);
                }

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    InvalidRequestParameterErrorMessage error =
                        await JsonUtility.ConvertResponseToObject<InvalidRequestParameterErrorMessage>(response);
                    throw new InvalidParamenterException(error);
                }

                throw new HttpRequestException("Error al establecer la conexión con el servidor \n" +
                                               $"Status Code: {response.StatusCode}\n" +
                                               $"URL: {response.RequestMessage.RequestUri}");
            }
        }

        public async Task<IList<CountryDto>> GetSummaryByCountry(string countryName)
        {

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
            {

                client.BaseAddress = new Uri(_covid19ApiEndPoint);

                var response = await client.GetAsync($"/country/{countryName}")
                    .ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var covidSummaryByCountry = await JsonUtility.ConvertResponseToObject<IList<Countries>>(response);

                    return _mapper.Map<IList<CountryDto>>(covidSummaryByCountry);

                }
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    EntityNotFoundErrorMessage error =
                        await JsonUtility.ConvertResponseToObject<EntityNotFoundErrorMessage>(response);
                    throw new EntityNotFoundException(error);
                }

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    InvalidRequestParameterErrorMessage error =
                        await JsonUtility.ConvertResponseToObject<InvalidRequestParameterErrorMessage>(response);
                    throw new InvalidParamenterException(error);
                }

                throw new HttpRequestException("Error al establecer la conexión con el servidor \n" +
                                               $"Status Code: {response.StatusCode}\n" +
                                               $"URL: {response.RequestMessage.RequestUri}");
            }
        }
    }
}
