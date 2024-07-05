﻿using AutoMapper;
using InsurTech.APIs.DTOs.HomeInsurancePlanDTO;
using InsurTech.APIs.DTOs.MotorInsurancePlanDTO;
using InsurTech.APIs.Errors;
using InsurTech.APIs.Helpers;
using InsurTech.Core;
using InsurTech.Core.Entities;
using InsurTech.Core.Entities.Identity;
using InsurTech.Core.Repositories;
using InsurTech.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using System.Numerics;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace InsurTech.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotorInsuranceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHubContext<NotificationHub> _hubContext;

        public MotorInsuranceController(IUnitOfWork unitOfWork, IMapper mapper , UserManager<AppUser> userManager, IHubContext<NotificationHub> hubContext)
        {
            
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _hubContext = hubContext;
        }

        [HttpPost("AddMotorPlan")]
        public async Task<IActionResult> AddMotorPlan(CreateMotorInsuranceDTO motorInsuranceDTO)
        {
            if (ModelState.IsValid)
            {
                Category insuranceCategoury = await _unitOfWork.Repository<Category>().GetByIdAsync(motorInsuranceDTO.CategoryId);
                var motorInsurancePlan = new MotorInsurancePlan
                {
                    YearlyCoverage = motorInsuranceDTO.YearlyCoverage,
                    Level = motorInsuranceDTO.Level,
                    CategoryId = motorInsuranceDTO.CategoryId,
                    Quotation = motorInsuranceDTO.Quotation,
                    CompanyId = motorInsuranceDTO.CompanyId,
                    PersonalAccident = motorInsuranceDTO.PersonalAccident,
                    Theft = motorInsuranceDTO.Theft,
                    ThirdPartyLiability = motorInsuranceDTO.ThirdPartyLiability,
                    OwnDamage = motorInsuranceDTO.OwnDamage,
                    LegalExpenses = motorInsuranceDTO.LegalExpenses,
                    Category=insuranceCategoury,
                    AvailableInsurance=true
                };

                await _unitOfWork.Repository<MotorInsurancePlan>().AddAsync(motorInsurancePlan);
                await _unitOfWork.CompleteAsync();

                var notification = new Notification
                {
                    Body = $"A new Motor insurance plan '{motorInsurancePlan.Level}' has been added by company ID {motorInsurancePlan.CompanyId}.",
                    UserId = "1" ,
                    IsRead = false
                };
                await _unitOfWork.Repository<Notification>().AddAsync(notification);

                await _hubContext.Clients.Group("admin").SendAsync("ReceiveNotification", notification.Body);



                var approvedCustomers = await _userManager.Users
                    .Where(u => u.UserType == UserType.Customer && u.IsApprove == IsApprove.approved)
                    .ToListAsync();

                foreach (var customer in approvedCustomers)
                {
                    var notificationToCustomer = new Notification
                    {
                        Body = $"A new Home insurance plan '{motorInsurancePlan.Level}' has been added by  {motorInsurancePlan.Company.UserName}.",
                        UserId = customer.Id,
                        IsRead = false
                    };

                    await _unitOfWork.Repository<Notification>().AddAsync(notificationToCustomer);
                    await _hubContext.Clients.Group("customer").SendAsync("ReceiveNotification", notificationToCustomer.Body);
                }
                await _unitOfWork.CompleteAsync();


                return Ok(motorInsuranceDTO);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("EditMotorPlan/{id}")]
        public async Task<IActionResult> EditMotorPlan(int id, EditMotorInsuranceDTO motorInsuranceDTO)
        {
            if (id <= 0 || motorInsuranceDTO == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                Category insuranceCategoury = await _unitOfWork.Repository<Category>().GetByIdAsync(motorInsuranceDTO.CategoryId);
                var storedMotorInsurancePlan = await _unitOfWork.Repository<MotorInsurancePlan>().GetByIdAsync(id);
                if (storedMotorInsurancePlan == null)
                {
                    return NotFound();
                }

                storedMotorInsurancePlan.YearlyCoverage = motorInsuranceDTO.YearlyCoverage;
                storedMotorInsurancePlan.Level = motorInsuranceDTO.Level;
                storedMotorInsurancePlan.CategoryId = motorInsuranceDTO.CategoryId;
                storedMotorInsurancePlan.Quotation = motorInsuranceDTO.Quotation;
                storedMotorInsurancePlan.CompanyId = motorInsuranceDTO.CompanyId;
                storedMotorInsurancePlan.PersonalAccident = motorInsuranceDTO.PersonalAccident;
                storedMotorInsurancePlan.Theft = motorInsuranceDTO.Theft;
                storedMotorInsurancePlan.ThirdPartyLiability = motorInsuranceDTO.ThirdPartyLiability;
                storedMotorInsurancePlan.OwnDamage = motorInsuranceDTO.OwnDamage;
                storedMotorInsurancePlan.LegalExpenses = motorInsuranceDTO.LegalExpenses;
                storedMotorInsurancePlan.Category = insuranceCategoury;

                await _unitOfWork.Repository<MotorInsurancePlan>().Update(storedMotorInsurancePlan);
                await _unitOfWork.CompleteAsync();
                var notification = new Notification
                {
                    Body = $"The Motor insurance plan '{storedMotorInsurancePlan.Level}' has been updated by company ID { storedMotorInsurancePlan.CompanyId }.",
                    UserId = "1" ,
                    IsRead = false
                };
                await _unitOfWork.Repository<Notification>().AddAsync(notification);
                await _unitOfWork.CompleteAsync();

                await _hubContext.Clients.Group("admin").SendAsync("ReceiveNotification", notification.Body);


                return Ok(motorInsuranceDTO);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMotorInsuranceById(int id)
        {

            var motorInsurance = await _unitOfWork.Repository<MotorInsurancePlan>().GetByIdAsync(id);
            if(motorInsurance!=null || motorInsurance?.AvailableInsurance != false)
            {
                var NumberOfRequests = motorInsurance.Requests.Count();
                MotorInsuranceDTO motorinsuranceDto = new MotorInsuranceDTO()
                {
                    NumberOfUsers = NumberOfRequests,
                    Id = motorInsurance.Id,
                    Level = motorInsurance.Level,
                    LegalExpenses = motorInsurance.LegalExpenses,
                    OwnDamage = motorInsurance.OwnDamage,
                    PersonalAccident = motorInsurance.PersonalAccident,
                    ThirdPartyLiability = motorInsurance.ThirdPartyLiability,
                    Theft = motorInsurance.Theft,
                    Quotation = motorInsurance.Quotation,
                    YearlyCoverage = motorInsurance.YearlyCoverage,
                    Category = motorInsurance.Category.Name,
                    Company = motorInsurance.Company?.UserName ?? "no comapny"
                };
                return Ok(motorinsuranceDto);
            }
            else
            {
                return BadRequest("No Matches Insurances found");
            }
        }

        [HttpGet("GetMotorInsurance")]
        public async Task<IActionResult> GetMotorInsurance()
        {
            var motorInsurance = await _unitOfWork.Repository<MotorInsurancePlan>().GetAllAsync();
            motorInsurance = motorInsurance.Where(a => a.AvailableInsurance == true).ToList();
            if (motorInsurance.Count() != 0)
            {
                List<MotorInsuranceDTO> motorinsuranceDto = new List<MotorInsuranceDTO>();

                foreach (var item in motorInsurance)
                {
                    var NumberOfRequests = item.Requests.Count();
                    var motorinsuranceitem =new MotorInsuranceDTO()
                    {
                        Id = item.Id,
                        Level = item.Level,
                        LegalExpenses = item.LegalExpenses,
                        OwnDamage = item.OwnDamage,
                        PersonalAccident = item.PersonalAccident,
                        ThirdPartyLiability = item.ThirdPartyLiability,
                        Theft = item.Theft,
                        Quotation = item.Quotation,
                        YearlyCoverage = item.YearlyCoverage,
                        Category = item.Category.Name,
                        Company = item.Company?.UserName ?? "no comapny",
                        NumberOfUsers = NumberOfRequests,


                    };
                    motorinsuranceDto.Add(motorinsuranceitem);
                }
                return Ok(motorinsuranceDto);
            }
            else
            {
                return BadRequest("No Insurances Yet");
            }
        }

        [HttpGet("GetMotorInsuranceByCompanyId/{id}")]
        public async Task<IActionResult> GetMotorInsuranceByCompanyId(string id)
        {
            var motorInsurancePlans = await _unitOfWork.Repository<MotorInsurancePlan>().GetAllAsync();
            var filteredMotorPlans = motorInsurancePlans
                .Where(plan => plan.AvailableInsurance && plan.CompanyId == id).ToList();
            var motorInsuranceDtos = _mapper.Map<List<MotorInsuranceDTO>>(filteredMotorPlans);

            if (motorInsuranceDtos.Count == 0)
            {
                return NotFound(new ApiResponse(404, "No Insurances Yet"));
            }
            return Ok(motorInsuranceDtos);
        }

           
        }
}
