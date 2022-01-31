using System;
using System.Collections.Generic;
using System.Text;

namespace ChileComprasProxy.Backend.Interfaces.Dto
{

    public class CountrySummaryDto
    {
        public string CountryCode { get; set; }
        public string Country { get; set; }
        public int TotalConfirmed { get; set; }
        public int TotalDeaths { get; set; }
        public int TotalRecovered { get; set; }
    }


}
