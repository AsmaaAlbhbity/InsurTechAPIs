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

namespace InsurTech.APIs.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserInquireController : ControllerBase
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper _mapper;

		public UserInquireController(IUnitOfWork unitOfWork, IMapper mapper)
		{
			this.unitOfWork = unitOfWork;
			_mapper = mapper;
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
	}

}

