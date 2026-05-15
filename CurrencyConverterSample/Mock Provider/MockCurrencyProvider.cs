using CurrencyConverter.Models;
using CurrencyConverter.Provider;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverterSample.Mock_Provider
{
    /// <summary>
    /// Mock currency provider to limit real usage
    /// </summary>
    internal class MockCurrencyProvider : ICurrencyProvider
    {
        public IEnumerable<CurrencyInfo> SupportedCurrencies => new List<CurrencyInfo>
        {
            new CurrencyInfo { CountryCode = "US", CurrencyCode = "USD", CurrencyName = "US Dollar", Country = "United States", FlagSource = "pack://application:,,,/CurrencyConverter;component/Provider/svg/us.svg" },
            new CurrencyInfo { CountryCode = "EU", CurrencyCode = "EUR", CurrencyName = "Euro", Country = "European Union", FlagSource = "pack://application:,,,/CurrencyConverter;component/Provider/svg/eu.svg" },
            new CurrencyInfo { CountryCode = "GB", CurrencyCode = "GBP", CurrencyName = "British Pound", Country = "United Kingdom", FlagSource = "pack://application:,,,/CurrencyConverter;component/Provider/svg/gb.svg" },
            new CurrencyInfo { CountryCode = "AU", CurrencyCode = "AUD", CurrencyName = "Australian Dollar", Country = "Australia", FlagSource = "pack://application:,,,/CurrencyConverter;component/Provider/svg/au.svg" },
            new CurrencyInfo { CountryCode = "IN", CurrencyCode = "INR", CurrencyName = "Indian Rupee", Country = "India", FlagSource = "pack://application:,,,/CurrencyConverter;component/Provider/svg/in.svg" },
            new CurrencyInfo { CountryCode = "BT", CurrencyCode = "BTN", CurrencyName = "Bhutanese Ngultrum", Country = "Bhutan", FlagSource = "pack://application:,,,/CurrencyConverter;component/Provider/svg/bt.svg" },
            new CurrencyInfo { CountryCode = "JP", CurrencyCode = "JPY", CurrencyName = "Japanese Yen", Country = "Japan", FlagSource = "pack://application:,,,/CurrencyConverter;component/Provider/svg/jp.svg" },
            new CurrencyInfo { CountryCode = "CA", CurrencyCode = "CAD", CurrencyName = "Canadian Dollar", Country = "Canada", FlagSource = "pack://application:,,,/CurrencyConverter;component/Provider/svg/ca.svg" },
            new CurrencyInfo { CountryCode = "CH", CurrencyCode = "CHF", CurrencyName = "Swiss Franc", Country = "Switzerland", FlagSource = "pack://application:,,,/CurrencyConverter;component/Provider/svg/ch.svg" },
            new CurrencyInfo { CountryCode = "CN", CurrencyCode = "CNY", CurrencyName = "Chinese Yuan", Country = "China", FlagSource = "pack://application:,,,/CurrencyConverter;component/Provider/svg/cn.svg" },
            new CurrencyInfo { CountryCode = "KR", CurrencyCode = "KRW", CurrencyName = "South Korean Won", Country = "South Korea", FlagSource = "pack://application:,,,/CurrencyConverter;component/Provider/svg/kr.svg" },
            new CurrencyInfo { CountryCode = "RU", CurrencyCode = "RUB", CurrencyName = "Russian Ruble", Country = "Russia", FlagSource = "pack://application:,,,/CurrencyConverter;component/Provider/svg/ru.svg" },
            new CurrencyInfo { CountryCode = "BR", CurrencyCode = "BRL", CurrencyName = "Brazilian Real", Country = "Brazil", FlagSource = "pack://application:,,,/CurrencyConverter;component/Provider/svg/br.svg" },
            new CurrencyInfo { CountryCode = "ZA", CurrencyCode = "ZAR", CurrencyName = "South African Rand", Country = "South Africa", FlagSource = "pack://application:,,,/CurrencyConverter;component/Provider/svg/za.svg" },
            new CurrencyInfo { CountryCode = "MX", CurrencyCode = "MXN", CurrencyName = "Mexican Peso", Country = "Mexico", FlagSource = "pack://application:,,,/CurrencyConverter;component/Provider/svg/mx.svg" },
            new CurrencyInfo { CountryCode = "SG", CurrencyCode = "SGD", CurrencyName = "Singapore Dollar", Country = "Singapore", FlagSource = "pack://application:,,,/CurrencyConverter;component/Provider/svg/sg.svg" },
            new CurrencyInfo { CountryCode = "NZ", CurrencyCode = "NZD", CurrencyName = "New Zealand Dollar", Country = "New Zealand", FlagSource = "pack://application:,,,/CurrencyConverter;component/Provider/svg/nz.svg" }
        };

        private readonly Dictionary<string, decimal> _mockRates = new Dictionary<string, decimal>
        {
            { "USD", 1.0m },
            { "EUR", 0.92m },
            { "GBP", 0.79m },
            { "AUD", 1.52m },
            { "INR", 83.30m },
            { "BTN", 83.30m },
            { "JPY", 155.20m }
        };

        public Task<decimal> GetConversionRatio(string sourceCode, string targetCode)
        {
            Debug.WriteLine("Hit mock");
            if (!_mockRates.ContainsKey(sourceCode) || !_mockRates.ContainsKey(targetCode))
            {
                return Task.FromResult(1.0m);
            }

            // Convert source to USD base, then USD to target
            decimal ratio = (1 / _mockRates[sourceCode]) * _mockRates[targetCode];

            return Task.FromResult(Math.Round(ratio, 4));
        }
    }
}
