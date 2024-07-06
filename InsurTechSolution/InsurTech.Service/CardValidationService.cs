using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace InsurTech.Core.Service
{
    public class CardValidationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CardValidationService> _logger;
        private readonly string _apiBaseUrl;
        private readonly string _apiKey;

        public CardValidationService(HttpClient httpClient, IConfiguration configuration, ILogger<CardValidationService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _apiBaseUrl = configuration["CardValidator:ApiBaseUrl"];
            _apiKey = configuration["CardValidator:ApiKey"];
        }

        private async Task<JObject> PostRequestAsync(string endpoint, JObject payload)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_apiBaseUrl}/{endpoint}")
            {
                Content = new StringContent(payload.ToString(), System.Text.Encoding.UTF8, "application/json")
            };

            request.Headers.Add("x-openapihub-key", _apiKey);

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Error calling {endpoint}: {response.StatusCode}");
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            return JObject.Parse(content);
        }

        public async Task<bool> ValidateCardNumberAsync(string cardNumber)
        {
            var payload = new JObject
            {
                ["number"] = cardNumber,
                ["luhnValidateUnionPay"] = true,
                ["maxLength"] = 16
            };
            var response = await PostRequestAsync("number-check", payload);
            return response?["isValid"]?.Value<bool>() ?? false;
        }

        public async Task<bool> ValidateCardHolderNameAsync(string cardHolderName)
        {
            var payload = new JObject { ["name"] = cardHolderName };
            var response = await PostRequestAsync("holder-name-check", payload);
            return response?["isValid"]?.Value<bool>() ?? false;
        }

        public async Task<bool> ValidateExpiryDateAsync(string expiryDate)
        {
            var payload = new JObject { ["date"] = expiryDate, ["maxElapsedYear"] = (DateTime.Now.Year + 10).ToString() };
            var response = await PostRequestAsync("expire-date-check", payload);
            return response?["isValid"]?.Value<bool>() ?? false;
        }

        public async Task<bool> ValidateCvvAsync(string cvv, int maxLength = 3)
        {
            var payload = new JObject { ["cvv"] = cvv, ["maxLength"] = maxLength };
            var response = await PostRequestAsync("cvv-check", payload);
            return response?["isValid"]?.Value<bool>() ?? false;
        }

        public async Task<bool> ValidatePostalCodeAsync(string postalCode, int minLength = 3)
        {
            var payload = new JObject { ["postalCode"] = postalCode, ["minLength"] = minLength };
            var response = await PostRequestAsync("postal-code-check", payload);
            return response?["isValid"]?.Value<bool>() ?? false;
        }
    }
}
