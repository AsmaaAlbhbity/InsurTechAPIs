using AutoMapper;
using InsurTech.APIs.DTOs.HealthInsurancePlanDTO;
using InsurTech.APIs.DTOs.HealthInsurancePlanDTO;
using InsurTech.APIs.DTOs.HealthInsurancePlanDTO;
using InsurTech.APIs.DTOs.HomeInsurancePlanDTO;
using InsurTech.APIs.DTOs.MotorInsurancePlanDTO;
using InsurTech.APIs.Errors;
using InsurTech.Core;
using InsurTech.Core.Entities;
using InsurTech.Core.Repositories;
using InsurTech.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InsurTech.Core.Service;

namespace InsurTech.APIs.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserInquireController : ControllerBase
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper _mapper;
		private readonly IEmailService _emailService;
		public UserInquireController(IUnitOfWork unitOfWork, IMapper mapper, IEmailService _emailService)
		{
			this.unitOfWork = unitOfWork;
			_mapper = mapper;
			this._emailService = _emailService;
		}

		[HttpGet("GetUserInquery")]
		public async Task<IActionResult> GetUserInquery()
		{
			var userInquery = await unitOfWork.Repository<UserInquire>().GetAllAsync();
			return Ok(userInquery);
		}

		[HttpPost("AddUserInquery")]
		public async Task<IActionResult> AddUserInquery(UserInquire userInquireDTO)
		{
			if (ModelState.IsValid)
			{
				var userInquire = new UserInquire
				{
					Email = userInquireDTO.Email,
					Content = userInquireDTO.Content,
					Date = userInquireDTO.Date,
					IsRead = userInquireDTO.IsRead
				};

				await unitOfWork.Repository<UserInquire>().AddAsync(userInquire);
				await unitOfWork.CompleteAsync();
				return Ok(userInquireDTO);
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpPost("SendUserEmail")]
		public async Task<IActionResult> SendUserEmail([FromBody] EmailRequest emailRequest)
		{
			if (string.IsNullOrEmpty(emailRequest.ToEmail) || string.IsNullOrEmpty(emailRequest.Subject) || string.IsNullOrEmpty(emailRequest.Content))
			{
				return BadRequest("Invalid email details provided.");
			}

			try
			{
				await _emailService.SendEmailAsync(emailRequest.ToEmail, emailRequest.Subject, emailRequest.Content);
				return Ok("Email sent successfully.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}
	}

	public class EmailRequest
	{
		public string ToEmail { get; set; }
		public string Subject { get; set; }
		public string Content { get; set; }
	}
}