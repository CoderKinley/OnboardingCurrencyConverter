using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CurrencyConverter.Models
{
    public class CurrencyLayerResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; } = string.Empty;

        [JsonPropertyName("quotes")]
        public Dictionary<string, decimal> Quotes { get; set; } = new();

        [JsonPropertyName("error")]
        public ApiError? Error { get; set; }

    }

    public class ApiError
    {
        [JsonPropertyName("info")]
        public string Info { get; set; } = string.Empty;
    }
}
