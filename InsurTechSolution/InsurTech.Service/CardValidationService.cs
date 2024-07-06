using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace InsurTech.Core.Service
{
    public class CardValidationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CardValidationService> _logger;

        public CardValidationService(HttpClient httpClient, ILogger<CardValidationService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<bool> ValidateCardAsync(string cardNumber)
        {
            var bin = cardNumber.Substring(0, 6);
            _logger.LogInformation($"Validating card with BIN: {bin}");

            var response = await _httpClient.GetAsync($"https://lookup.binlist.net/{bin}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var cardInfo = JObject.Parse(content);
                _logger.LogInformation($"Binlist response: {cardInfo}");

                bool isLengthValid = cardInfo["number"]?["length"]?.Value<int>() == cardNumber.Length;
                bool isLuhnValid = LuhnCheck(cardNumber);

                _logger.LogInformation($"Length valid: {isLengthValid}, Luhn valid: {isLuhnValid}");

                return isLengthValid && isLuhnValid;
            }

            _logger.LogError($"Failed to validate card with BIN: {bin}");
            return false;
        }

        private bool LuhnCheck(string cardNumber)
        {
            int sum = 0;
            bool alternate = false;

            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int n = int.Parse(cardNumber[i].ToString());

                if (alternate)
                {
                    n *= 2;
                    if (n > 9)
                    {
                        n -= 9;
                    }
                }

                sum += n;
                alternate = !alternate;
            }

            return (sum % 10 == 0);
        }
    }
}
