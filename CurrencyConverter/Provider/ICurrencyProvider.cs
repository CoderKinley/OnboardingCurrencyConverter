using CurrencyConverter.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyConverter.Provider
{
    /// <summary>
    /// Defines the contract for a currency conversion provider, 
    /// allowing for rate retrieval and supported currency metadata.
    /// </summary>
    public interface ICurrencyProvider
    {
        /// <summary>
        /// Gets the collection of currencies supported by this provider.
        /// </summary>
        IEnumerable<CurrencyInfo> GetSupportedCurrencies { get; }

        /// <summary>
        /// Retrieves the conversion ratio between the specified source and target currencies.
        /// </summary>
        /// <param name="sourceCode">The ISO currency code to convert from (e.g., "USD").</param>
        /// <param name="targetCode">The ISO currency code to convert to (e.g., "EUR").</param>
        /// <returns> 
        /// The task result contains the conversion ratio as a <see cref="decimal"/>.
        /// </returns>
        Task<decimal> GetConversionRatio(string sourceCode, string targetCode);
    }
}