using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InsurTech.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardValidationController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CardValidationController> _logger;
        private readonly string _apiBaseUrl;
        private readonly string _apiKey;

        public CardValidationController(HttpClient httpClient, IConfiguration configuration, ILogger<CardValidationController> logger)
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
                Content = new StringContent(payload.ToString(), Encoding.UTF8, "application/json")
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

        [HttpPost("validate-card-number")]
        public async Task<IActionResult> ValidateCardNumber([FromBody] CardNumberRequest request)
        {
            var payload = new JObject
            {
                ["number"] = request.CardNumber,
                ["luhnValidateUnionPay"] = true,
                ["maxLength"] = 16
            };
            var response = await PostRequestAsync("number-check", payload);
            bool isValid = response?["isValid"]?.Value<bool>() ?? false;
            return Ok(new { isValid  });
        }

        [HttpPost("validate-card-holder-name")]
        public async Task<IActionResult> ValidateCardHolderName([FromBody] CardHolderNameRequest request)
        {
            var payload = new JObject { ["name"] = request.CardHolderName };
            var response = await PostRequestAsync("holder-name-check", payload);
            bool isValid = response?["isValid"]?.Value<bool>() ?? false;
            return Ok(new { isValid });
        }

        [HttpPost("validate-expiry-date")]
        public async Task<IActionResult> ValidateExpiryDate([FromBody] ExpiryDateRequest request)
        {
            var payload = new JObject { ["date"] = request.ExpiryDate, ["maxElapsedYear"] = (System.DateTime.Now.Year + 10).ToString() };
            var response = await PostRequestAsync("expire-date-check", payload);
            bool isValid = response?["isValid"]?.Value<bool>() ?? false;
            return Ok(new { isValid });
        }

        [HttpPost("validate-cvv")]
        public async Task<IActionResult> ValidateCvv([FromBody] CvvRequest request)
        {
            var payload = new JObject { ["cvv"] = request.Cvv, ["maxLength"] = request.MaxLength };
            var response = await PostRequestAsync("cvv-check", payload);
            bool isValid = response?["isValid"]?.Value<bool>() ?? false;
            return Ok(new { isValid });
        }

        [HttpPost("validate-postal-code")]
        public async Task<IActionResult> ValidatePostalCode([FromBody] PostalCodeRequest request)
        {
            var payload = new JObject { ["postalCode"] = request.PostalCode, ["minLength"] = request.MinLength };
            var response = await PostRequestAsync("postal-code-check", payload);
            bool isValid = response?["isValid"]?.Value<bool>() ?? false;
            return Ok(new { isValid });
        }
    }

    public class CardNumberRequest
    {
        public string CardNumber { get; set; }
    }

    public class CardHolderNameRequest
    {
        public string CardHolderName { get; set; }
    }

    public class ExpiryDateRequest
    {
        public string ExpiryDate { get; set; }
    }

    public class CvvRequest
    {
        public string Cvv { get; set; }
        public int MaxLength { get; set; } = 3;
    }

    public class PostalCodeRequest
    {
        public string PostalCode { get; set; }
        public int MinLength { get; set; } = 3;
    }
}
