using System;
using System.Collections.Generic;
using System.Text;

namespace ChileComprasProxy.Backend.Interfaces.Dto
{
    public class CountryDto
    {
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public int Active { get; set; }
        public int Confirmed { get; set; }
        public int Deaths { get; set; }
        public int Recovered { get; set; }
        public DateTime Date { get; set; }
    }

 
}
