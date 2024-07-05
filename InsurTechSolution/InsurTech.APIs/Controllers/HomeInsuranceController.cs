using InsurTech.Core;
using InsurTech.Core.Entities;
using InsurTech.Core.Repositories;
using InsurTech.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InsurTech.APIs.DTOs.HomeInsurancePlanDTO;
using AutoMapper;
using InsurTech.APIs.DTOs.HealthInsurancePlanDTO;
using InsurTech.APIs.Errors;
using InsurTech.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using InsurTech.APIs.Helpers;
using Microsoft.EntityFrameworkCore;

namespace InsurTech.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeInsuranceController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHubContext<NotificationHub> _hubContext;

        public HomeInsuranceController(IUnitOfWork unitOfWork , IMapper mapper, UserManager<AppUser> userManager, IHubContext<NotificationHub> hubContext)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _hubContext = hubContext;
        }
        [HttpPost("AddHomePlan")]
        public async Task<IActionResult> AddHomePlan(CreateHomeInsuranceDTO HomeInsuranceDTO)
        {
            if (ModelState.IsValid)
            {
                Category insuranceCategoury = await unitOfWork.Repository<Category>().GetByIdAsync(HomeInsuranceDTO.CategoryId);

                var HomeInsurancePlan = new HomeInsurancePlan
                {
                    YearlyCoverage = HomeInsuranceDTO.YearlyCoverage,
                    Level = HomeInsuranceDTO.Level,
                    CategoryId = HomeInsuranceDTO.CategoryId,
                    Quotation = HomeInsuranceDTO.Quotation,
                    Category=insuranceCategoury,
                    CompanyId = HomeInsuranceDTO.CompanyId,
                    WaterDamage= HomeInsuranceDTO.WaterDamage,
                    GlassBreakage= HomeInsuranceDTO.GlassBreakage,
                    NaturalHazard= HomeInsuranceDTO.NaturalHazard,
                    AttemptedTheft= HomeInsuranceDTO.AttemptedTheft,
                    FiresAndExplosion= HomeInsuranceDTO.FiresAndExplosion,
                    AvailableInsurance=true

                };

                await unitOfWork.Repository<HomeInsurancePlan>().AddAsync(HomeInsurancePlan);
                await unitOfWork.CompleteAsync();

                var notification = new Notification
                {
                    Body = $"A new Home insurance plan '{HomeInsurancePlan.Level}' has been added by company ID {HomeInsurancePlan.CompanyId}.",
                    UserId = "1",
                    IsRead = false
                };
                await unitOfWork.Repository<Notification>().AddAsync(notification);

                await _hubContext.Clients.Group("admin").SendAsync("ReceiveNotification", notification.Body);



                var approvedCustomers = await _userManager.Users
                    .Where(u => u.UserType == UserType.Customer && u.IsApprove == IsApprove.approved)
                    .ToListAsync();

                foreach (var customer in approvedCustomers)
                {
                    var notificationToCustomer = new Notification
                    {
                        Body = $"A new Home insurance plan '{HomeInsurancePlan.Level}' has been added by  {HomeInsurancePlan.Company.UserName}.",
                        UserId = customer.Id,
                        IsRead = false
                    };

                    await unitOfWork.Repository<Notification>().AddAsync(notificationToCustomer);
                    await _hubContext.Clients.Group("customer").SendAsync("ReceiveNotification", notificationToCustomer.Body);
                }
                await unitOfWork.CompleteAsync();

                return Ok(HomeInsuranceDTO);
            }
            else
            {
                return BadRequest();
            }
        }




        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditHomePlan(int id, EditHomeInsuranceDTO HomeInsuranceDTO)
        {
            if (id <= 0 || HomeInsuranceDTO == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                Category insuranceCategoury = await unitOfWork.Repository<Category>().GetByIdAsync(HomeInsuranceDTO.CategoryId);

                var storedHomeInsurancePlan = await unitOfWork.Repository<HomeInsurancePlan>().GetByIdAsync(id);
                if (storedHomeInsurancePlan == null)
                {
                    return NotFound();
                }

                storedHomeInsurancePlan.YearlyCoverage = HomeInsuranceDTO.YearlyCoverage;
                storedHomeInsurancePlan.Level = HomeInsuranceDTO.Level;
                storedHomeInsurancePlan.CategoryId = HomeInsuranceDTO.CategoryId;
                storedHomeInsurancePlan.Quotation = HomeInsuranceDTO.Quotation;
                storedHomeInsurancePlan.CompanyId = HomeInsuranceDTO.CompanyId;
                storedHomeInsurancePlan.WaterDamage = HomeInsuranceDTO.WaterDamage;
                storedHomeInsurancePlan.GlassBreakage = HomeInsuranceDTO.GlassBreakage;
                storedHomeInsurancePlan.NaturalHazard = HomeInsuranceDTO.NaturalHazard;
                storedHomeInsurancePlan.AttemptedTheft = HomeInsuranceDTO.AttemptedTheft;
                storedHomeInsurancePlan.FiresAndExplosion = HomeInsuranceDTO.FiresAndExplosion;
                storedHomeInsurancePlan.Category = insuranceCategoury;

                await unitOfWork.Repository<HomeInsurancePlan>().Update(storedHomeInsurancePlan);
                await unitOfWork.CompleteAsync();

                var notification = new Notification
                {
                    Body = $"The Home insurance plan '{storedHomeInsurancePlan.Level}' has been updated by company ID {storedHomeInsurancePlan.CompanyId}.",
                    UserId = "1",
                    IsRead = false
                };
                await unitOfWork.Repository<Notification>().AddAsync(notification);
                await unitOfWork.CompleteAsync();

                await _hubContext.Clients.Group("admin").SendAsync("ReceiveNotification", notification.Body);


                return Ok(HomeInsuranceDTO);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHomeInsuranceById(int id)
        {

            var HomeInsurance = await unitOfWork.Repository<HomeInsurancePlan>().GetByIdAsync(id);
            if (HomeInsurance != null || HomeInsurance?.AvailableInsurance != false)
            {
                var NumberOfRequests = HomeInsurance.Requests.Count();
                HomeInsuranceDTO HomeInsuranceDto = new HomeInsuranceDTO()
                {
                    NumberOfUsers = NumberOfRequests,
                    Id = HomeInsurance.Id,
                    Level = HomeInsurance.Level,
                    GlassBreakage = HomeInsurance.GlassBreakage,
                    AttemptedTheft = HomeInsurance.AttemptedTheft,
                    FiresAndExplosion = HomeInsurance.FiresAndExplosion,
                    NaturalHazard = HomeInsurance.NaturalHazard,
                    WaterDamage = HomeInsurance.WaterDamage,
                    Quotation = HomeInsurance.Quotation,
                    YearlyCoverage = HomeInsurance.YearlyCoverage,
                    Category = HomeInsurance.Category.Name,
                    Company = HomeInsurance.Company?.UserName ?? "no comapny"
                };
                return Ok(HomeInsuranceDto);
            }
            else
            {
                return BadRequest("No Matches Insurances found");
            }
        }



        [HttpGet("GetHomeInsurance")]
        public async Task<IActionResult> GetHomeInsurance()
        {
            var HomeInsurance = await unitOfWork.Repository<HomeInsurancePlan>().GetAllAsync();
            HomeInsurance = HomeInsurance.Where(a => a.AvailableInsurance == true).ToList();

            if (HomeInsurance.Count() != 0)
            {
                List<HomeInsuranceDTO> HomeinsuranceDto = new List<HomeInsuranceDTO>();
                foreach (var item in HomeInsurance)
                {
                    var NumberOfRequests = item.Requests.Count();
                    var Homeinsuranceitem = new HomeInsuranceDTO()
                    {
                        Id = item.Id,
                        Level = item.Level,
                        GlassBreakage = item.GlassBreakage,
                        AttemptedTheft = item.AttemptedTheft,
                        FiresAndExplosion = item.FiresAndExplosion,
                        NaturalHazard = item.NaturalHazard,
                        WaterDamage = item.WaterDamage,
                        Quotation = item.Quotation,
                        YearlyCoverage = item.YearlyCoverage,
                        Category = item.Category.Name,
                        Company = item.Company?.UserName ?? "no comapny",
                        NumberOfUsers=NumberOfRequests
                    };
                    HomeinsuranceDto.Add(Homeinsuranceitem);
                }
                return Ok(HomeinsuranceDto);
            }
            else
            {
                return BadRequest("No Insurances Yet");
            }
        }

        [HttpGet("GetHomeInsuranceByCompanyId/{id}")]
        public async Task<IActionResult> GetHomeInsuranceByCompanyId(string id)
        {
            var homeInsurancePlans = await unitOfWork.Repository<HomeInsurancePlan>().GetAllAsync();
            var filteredHomePlans = homeInsurancePlans
                .Where(plan => plan.AvailableInsurance && plan.CompanyId == id).ToList();
            var homeInsuranceDtos = _mapper.Map<List<HomeInsuranceDTO>>(filteredHomePlans);

            if (homeInsuranceDtos.Count == 0)
            {
                return NotFound(new ApiResponse(404, "No Insurances Yet"));
            }
            return Ok(homeInsuranceDtos);
        }

    }
}

