﻿using AutoMapper;
using InsurTech.APIs.DTOs.Company;
using InsurTech.APIs.Errors;
using InsurTech.Core.Entities.Identity;
using InsurTech.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InsurTech.APIs.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompanyController : ControllerBase
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public CompanyController(UserManager<AppUser> userManager, IEmailService emailService, IMapper mapper)
        {
            _userManager = userManager;
            _emailService = emailService;
            _mapper = mapper;
        }



        #region ApproveCompany
        [HttpPost("ApproveCompany/{id}")]
        public async Task<ActionResult> ApproveCompany([FromRoute] string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound(new ApiResponse(404, "User not found"));
            if (user.UserType != UserType.Company) return BadRequest(new ApiResponse(400, "User is not a company"));
            if (user.IsApprove == IsApprove.approved) return BadRequest(new ApiResponse(400, "User is already approved"));
            if (user.IsApprove == IsApprove.rejected) return BadRequest(new ApiResponse(400, "User is rejected"));
            user.IsApprove = IsApprove.approved;
            await _userManager.UpdateAsync(user);
            return Ok();
        }
        #endregion

        #region RejectCompany
        [HttpPost("RejectCompany/{id}")]
        public async Task<ActionResult> RejectCompany([FromRoute] string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound(new ApiResponse(404, "User not found"));
            if (user.UserType != UserType.Company) return BadRequest(new ApiResponse(400, "User is not a company"));
            if (user.IsApprove == IsApprove.rejected) return BadRequest(new ApiResponse(400, "User is already rejected"));
            if (user.IsApprove == IsApprove.approved) return BadRequest(new ApiResponse(400, "User is approved"));
            user.IsApprove = IsApprove.rejected;
            await _userManager.UpdateAsync(user);
            return Ok();
        }
        #endregion

        #region Get Company By Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyById([FromRoute] string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null) return NotFound(new ApiResponse(404, "User not found"));

            if (user.UserType != UserType.Company) return BadRequest(new ApiResponse(400, "User is not a company"));

            var company = _mapper.Map<CompanyByIdOutputDto>(user);
            if (company == null) return NotFound(new ApiResponse(404, "Company not found"));

            // Roles
            var roles = await _userManager.GetRolesAsync(user);

            company.Roles = roles;


            return Ok(company);

        }
        #endregion

    }
}
