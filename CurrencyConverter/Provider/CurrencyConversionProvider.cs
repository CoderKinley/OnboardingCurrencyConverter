using CurrencyConverter.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Resources;

namespace CurrencyConverter.Provider
{
    /// <summary>
    /// Provides currency conversion rates via CurrencyLayer API and loads currency metadata from CSV.
    /// </summary>
    public class CurrencyConversionProvider : ICurrencyProvider
    {
        #region Var
        private readonly Uri _resourceUri = new Uri("pack://application:,,,/CurrencyConverter;component/Resources/country-code-to-currency-code-mapping.csv");
        private Dictionary<string, decimal>? _cachedRates;
        private IEnumerable<CurrencyInfo>? _cachedCurrencyInfo;
        private readonly HttpClient _client;
        private readonly string _apiKey;
        private const string BaseUrl = "http://api.currencylayer.com/live";
        #endregion

        #region Property
        /// <summary>
        /// Gets the list of supported currencies, loading them from the local CSV resource on first access.
        /// </summary>
        public IEnumerable<CurrencyInfo> SupportedCurrencies
        {
            get { 
                if(_cachedCurrencyInfo == null)
                {
                    _cachedCurrencyInfo = GetSupportedCurrencies();
                }
                return _cachedCurrencyInfo;
            }
        }
        #endregion
        /// <summary>
        /// Initializes a new instance of the provider with a default HTTP client.
        public CurrencyConversionProvider(string apiKey, HttpClient client)
        {
            _apiKey = apiKey;
            _client = client;
            _client.Timeout = TimeSpan.FromSeconds(10);
        }

        /// <summary>
        /// Calculates the conversion ratio between two currencies. 
        /// Uses cached rates if available to minimize API usage.
        /// </summary>
        /// <param name="sourceCode">The source currency ISO code (e.g., USD).</param>
        /// <param name="targetCode">The target currency ISO code (e.g., EUR).</param>
        /// <returns>The conversion ratio as a decimal.</returns>
        public async Task<decimal> GetConversionRatio(string sourceCode, string targetCode)
        {
            if (_cachedRates == null)
            {
                await RefreshRatesAsync();
            }

            if (_cachedRates == null || _cachedRates.Count == 0)
            {
                throw new Exception("Unable to retrieve currency rates.");
            }

            string keyFrom = $"USD{sourceCode.ToUpper()}";
            string keyTo = $"USD{targetCode.ToUpper()}";

            decimal rateFrom = sourceCode.ToUpper() == "USD" ? 1m : _cachedRates[keyFrom];
            decimal rateTo = targetCode.ToUpper() == "USD" ? 1m : _cachedRates[keyTo];

            return rateTo / rateFrom;
        }

        private IEnumerable<CurrencyInfo> GetSupportedCurrencies()
        {
            var currency = new List<CurrencyInfo>();

            StreamResourceInfo resourceInfo = Application.GetResourceStream(_resourceUri);
            using (var reader = new StreamReader(resourceInfo.Stream))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string? line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] parts = line.Split(',');

                    if (parts.Length >= 3)
                    {
                        currency.Add(new CurrencyInfo
                        {
                            Country = parts[0].Trim(),
                            CountryCode = parts[1].Trim(),
                            CurrencyName = parts[2].Trim(), 
                            CurrencyCode = parts[3].Trim()
                        });
                    }
                }
            }
            return currency;
        }
        private async Task RefreshRatesAsync()
        {
            try
            {
                string url = $"{BaseUrl}?access_key={_apiKey}";
                var responseString = await _client.GetStringAsync(url);

                var data = JsonSerializer.Deserialize<CurrencyLayerResponse>(responseString);
                if (data != null && data.Success)
                {
                    _cachedRates = data.Quotes;
                    Debug.WriteLine("DATA");
                    foreach (var dat in _cachedRates)
                    {
                        Debug.WriteLine(dat);
                    }
                    Debug.WriteLine("Currency rates cached successfully.");
                }
                else
                {
                    Debug.WriteLine($"API Error: {data?.Error?.Info}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Network Error: {ex.Message}");
            }
        }
    }
}
