using AutoMapper;
using ChileComprasProxy.Backend.Interfaces.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChileComprasProxy.Backend.Interfaces.Dto
{
    public class ProxyProfile: Profile
    {
        public ProxyProfile()
        {
         
            CreateMap<CountryCovidSummary, CountrySummaryDto>().ReverseMap();
            CreateMap<Countries, CountryDto>().ReverseMap();
        }
    }
}
