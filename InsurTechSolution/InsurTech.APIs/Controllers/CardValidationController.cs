using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using InsurTech.Core.Service;

[Route("api/[controller]")]
[ApiController]
public class CardValidationController : ControllerBase
{
    private readonly CardValidationService _cardValidationService;

    public CardValidationController(CardValidationService cardValidationService)
    {
        _cardValidationService = cardValidationService;
    }

    [HttpPost("validate-card-number")]
    public async Task<IActionResult> ValidateCardNumber([FromBody] CardNumberRequest request)
    {
        bool isValid = await _cardValidationService.ValidateCardNumberAsync(request.CardNumber);
        return Ok(new { isValid });
    }

    [HttpPost("validate-card-holder-name")]
    public async Task<IActionResult> ValidateCardHolderName([FromBody] CardHolderNameRequest request)
    {
        bool isValid = await _cardValidationService.ValidateCardHolderNameAsync(request.CardHolderName);
        return Ok(new { isValid });
    }

    [HttpPost("validate-expiry-date")]
    public async Task<IActionResult> ValidateExpiryDate([FromBody] ExpiryDateRequest request)
    {
        bool isValid = await _cardValidationService.ValidateExpiryDateAsync(request.ExpiryDate);
        return Ok(new { isValid });
    }

    [HttpPost("validate-cvv")]
    public async Task<IActionResult> ValidateCvv([FromBody] CvvRequest request)
    {
        bool isValid = await _cardValidationService.ValidateCvvAsync(request.Cvv, request.MaxLength);
        return Ok(new { isValid });
    }

    [HttpPost("validate-postal-code")]
    public async Task<IActionResult> ValidatePostalCode([FromBody] PostalCodeRequest request)
    {
        bool isValid = await _cardValidationService.ValidatePostalCodeAsync(request.PostalCode, request.MinLength);
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
