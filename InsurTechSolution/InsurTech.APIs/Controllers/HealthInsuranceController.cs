using AutoMapper;
using InsurTech.APIs.DTOs.HealthInsurancePlanDTO;
using InsurTech.APIs.DTOs.HomeInsurancePlanDTO;
using InsurTech.APIs.DTOs.MotorInsurancePlanDTO;
using InsurTech.APIs.Errors;
using InsurTech.APIs.Helpers;
using InsurTech.Core;
using InsurTech.Core.Entities;
using InsurTech.Core.Entities.Identity;
using InsurTech.Core.Repositories;
using InsurTech.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InsurTech.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthInsuranceController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHubContext<NotificationHub> _hubContext;

        public HealthInsuranceController(IUnitOfWork unitOfWork , IMapper mapper,UserManager<AppUser> userManager , IHubContext<NotificationHub> hubContext)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _hubContext = hubContext;
        }
        [HttpPost("AddHealthPlan")]
        public async Task<IActionResult> AddHealthPlan(CreateHealthInsuranceDTO healthInsuranceDTO)
        {
            if (ModelState.IsValid)
            {
                Category insuranceCategoury=await unitOfWork.Repository<Category>().GetByIdAsync(healthInsuranceDTO.CategoryId);

                var healthInsurancePlan = new HealthInsurancePlan
                {
                    YearlyCoverage = healthInsuranceDTO.YearlyCoverage,
                    Level = healthInsuranceDTO.Level,
                    CategoryId = healthInsuranceDTO.CategoryId,
                    Category= insuranceCategoury,
                    Quotation = healthInsuranceDTO.Quotation,
                    CompanyId = healthInsuranceDTO.CompanyId,
                    MedicalNetwork = healthInsuranceDTO.MedicalNetwork,
                    ClinicsCoverage = healthInsuranceDTO.ClinicsCoverage,
                    HospitalizationAndSurgery = healthInsuranceDTO.HospitalizationAndSurgery,
                    OpticalCoverage = healthInsuranceDTO.OpticalCoverage,
                    DentalCoverage = healthInsuranceDTO.DentalCoverage,
                    AvailableInsurance = true

                };

                await unitOfWork.Repository<HealthInsurancePlan>().AddAsync(healthInsurancePlan);
                await unitOfWork.CompleteAsync();

                var company = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));


                var notification = new Notification()
                {
                    Body = $"A new health insurance plan '{healthInsurancePlan.Level}' has been added by  {company.Name}.",
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
                    var notificationToCustomer = new Notification()
                    {
                        Body = $"A new health insurance plan '{healthInsurancePlan.Level}' has been added by  {company.Name}.",
                        UserId = customer.Id,
                        IsRead = false
                    };

                    await unitOfWork.Repository<Notification>().AddAsync(notificationToCustomer);
                    await _hubContext.Clients.Group("customer").SendAsync("ReceiveNotification", notificationToCustomer.Body);
                }
                await unitOfWork.CompleteAsync();


                return Ok(healthInsuranceDTO);
            }
            else
            {
                return BadRequest();
            }
        }



        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditHealthPlan(int id, EditHealthInsuranceDTO HealthInsuranceDTO)
        {
            if (id <= 0 || HealthInsuranceDTO == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                Category insuranceCategoury = await unitOfWork.Repository<Category>().GetByIdAsync(HealthInsuranceDTO.CategoryId);
                var storedHealthInsurancePlan = await unitOfWork.Repository<HealthInsurancePlan>().GetByIdAsync(id);
                if (storedHealthInsurancePlan == null)
                {
                    return NotFound();
                }

                storedHealthInsurancePlan.YearlyCoverage = HealthInsuranceDTO.YearlyCoverage;
                storedHealthInsurancePlan.Level = HealthInsuranceDTO.Level;
                storedHealthInsurancePlan.CategoryId = HealthInsuranceDTO.CategoryId;
                storedHealthInsurancePlan.Quotation = HealthInsuranceDTO.Quotation;
                storedHealthInsurancePlan.CompanyId = HealthInsuranceDTO.CompanyId;
                storedHealthInsurancePlan.MedicalNetwork = HealthInsuranceDTO.MedicalNetwork;
                storedHealthInsurancePlan.ClinicsCoverage = HealthInsuranceDTO.ClinicsCoverage;
                storedHealthInsurancePlan.HospitalizationAndSurgery = HealthInsuranceDTO.HospitalizationAndSurgery;
                storedHealthInsurancePlan.OpticalCoverage = HealthInsuranceDTO.OpticalCoverage;
                storedHealthInsurancePlan.DentalCoverage = HealthInsuranceDTO.DentalCoverage;
                storedHealthInsurancePlan.Category = insuranceCategoury;
                

                await unitOfWork.Repository<HealthInsurancePlan>().Update(storedHealthInsurancePlan);
                await unitOfWork.CompleteAsync();

                var company = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));


                var notification = new Notification()
                {
                    Body = $"The Health insurance plan '{storedHealthInsurancePlan.Level}' has been updated by  {company.Name}.",
                    UserId = "1" ,
                    IsRead = false
                };
                await unitOfWork.Repository<Notification>().AddAsync(notification);
                await unitOfWork.CompleteAsync();

                await _hubContext.Clients.Group("admin").SendAsync("ReceiveNotification", notification.Body);


                return Ok(HealthInsuranceDTO);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetHealthInsuranceById(int id)
        {

            var HealthInsurance = await unitOfWork.Repository<HealthInsurancePlan>().GetByIdAsync(id);
            if (HealthInsurance != null || HealthInsurance?.AvailableInsurance != false)
            {
                var NumberOfRequests = HealthInsurance.Requests.Count();
                HealthInsuranceDTO HealthInsuranceDto = new HealthInsuranceDTO()
                {
                    NumberOfUsers = NumberOfRequests,
                    Id = HealthInsurance.Id,
                    Level = HealthInsurance.Level,
                    HospitalizationAndSurgery = HealthInsurance.HospitalizationAndSurgery,
                    ClinicsCoverage = HealthInsurance.ClinicsCoverage,
                    DentalCoverage = HealthInsurance.DentalCoverage,
                    OpticalCoverage = HealthInsurance.OpticalCoverage,
                    MedicalNetwork = HealthInsurance.MedicalNetwork,
                    Quotation = HealthInsurance.Quotation,
                    YearlyCoverage = HealthInsurance.YearlyCoverage,
                    Category = HealthInsurance.Category.Name,
                    Company = HealthInsurance.Company?.UserName ?? "no comapny"
                };
                return Ok(HealthInsuranceDto);
            }
            else
            {
                return BadRequest("No Matches Insurances found");
            }
        }


        [HttpGet("GetHealthInsurance")]
        public async Task<IActionResult> GetHealthInsurance()
        {
            var HealthInsurance = await unitOfWork.Repository<HealthInsurancePlan>().GetAllAsync();
            HealthInsurance=HealthInsurance.Where(a=>a.AvailableInsurance==true).ToList();
            if (HealthInsurance.Count()!=0)
            {
                List<HealthInsuranceDTO> HealthinsuranceDto = new List<HealthInsuranceDTO>();
                foreach (var item in HealthInsurance)
                {
                    var NumberOfRequests = item.Requests.Count();
                    var Healthinsuranceitem = new HealthInsuranceDTO()
                    {
                        Id = item.Id,
                        Level = item.Level,
                        HospitalizationAndSurgery = item.HospitalizationAndSurgery,
                        ClinicsCoverage = item.ClinicsCoverage,
                        DentalCoverage = item.DentalCoverage,
                        OpticalCoverage = item.OpticalCoverage,
                        MedicalNetwork = item.MedicalNetwork,
                        Quotation = item.Quotation,
                        YearlyCoverage = item.YearlyCoverage,
                        Category = item.Category.Name,
                        Company = item.Company?.UserName ?? "no comapny",
                        NumberOfUsers=NumberOfRequests
                    };
                    HealthinsuranceDto.Add(Healthinsuranceitem);
                }
                return Ok(HealthinsuranceDto);
            }
            else
            {
                return BadRequest("No Insurances Yet");
            }
        }

        [HttpGet("GetHealthInsuranceByCompanyId/{id}")]
        public async Task<IActionResult> GetHealthInsuranceByCompanyId(string id)
        {
            var healthInsurancePlans = await unitOfWork.Repository<HealthInsurancePlan>().GetAllAsync();
            var filteredHealthPlans = healthInsurancePlans
                .Where(plan => plan.AvailableInsurance && plan.CompanyId == id).ToList();
            var healthInsuranceDtos = _mapper.Map<List<HealthInsuranceDTO>>(filteredHealthPlans);

            if (healthInsuranceDtos.Count == 0)
            {
                return NotFound(new ApiResponse(404, "No Insurances Yet"));
            }
            return Ok(healthInsuranceDtos);
        }



    }
}

