using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.Models
{
    public class CurrencyInfo
    {
        /// <summary>
        /// gets or sets the country code
        /// </summary>
        public required string CountryCode {get; set;}

        /// <summary>
        /// gets or sets the currency code 
        /// </summary>
        public required string CurrencyCode { get; set; }

        /// <summary>
        /// gets or sets the currency name
        /// </summary>
        public required string CurrencyName { get; set; }
        
        /// <summary>
        /// Gets or sets the country 
        /// </summary>
        public required string Country { get; set; }
    }
}
