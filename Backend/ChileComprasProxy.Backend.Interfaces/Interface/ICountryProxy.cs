using ChileComprasProxy.Backend.Interfaces.Dto;
using ChileComprasProxy.Backend.Interfaces.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace ChileComprasProxy.Backend.Interfaces.Interface
{
   public interface ICountryProxy
    {
        Task<IList<CountrySummaryDto>> GetCountrySummary();
        Task<IList<CountryDto>> GetSummaryByCountry(string countryName);
    }
}
