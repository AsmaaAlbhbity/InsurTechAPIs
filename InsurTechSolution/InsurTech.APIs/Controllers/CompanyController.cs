using AutoMapper;
using InsurTech.APIs.DTOs.Company;
using InsurTech.APIs.DTOs.CompanyRequests;
using InsurTech.APIs.DTOs.CompanyUpdateDto;
using InsurTech.APIs.DTOs.RequestDTO;
using InsurTech.APIs.Errors;
using InsurTech.Core;
using InsurTech.Core.Entities;
using InsurTech.Core.Entities.Identity;
using InsurTech.Core.Repositories;
using InsurTech.Core.Service;
using InsurTech.Repository;
using InsurTech.Repository.Data.Migrations;
using InsurTech.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace InsurTech.APIs.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompanyController : ControllerBase
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly IRequestService _requestService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IEmailService emailService, IMapper mapper, IRequestService requestService)

        {
            _userManager = userManager;
            _emailService = emailService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _requestService = requestService;

        }


        #region ApproveCompany
        [HttpPost("ApproveCompany/{id}")]
        public async Task<ActionResult> ApproveCompany([FromRoute] string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound(new ApiResponse(404, "User not found"));
            if (user.UserType != UserType.Company) return BadRequest(new ApiResponse(400, "User is not a company"));
            //if (user.IsApprove == IsApprove.approved) return BadRequest(new ApiResponse(400, "User is already approved"));
            //if (user.IsApprove == IsApprove.rejected) return BadRequest(new ApiResponse(400, "User is rejected"));
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
            //if (user.IsApprove == IsApprove.rejected) return BadRequest(new ApiResponse(400, "User is already rejected"));
            //if (user.IsApprove == IsApprove.approved) return BadRequest(new ApiResponse(400, "User is approved"));
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

            if (user.IsDeleted == true) return NotFound(new ApiResponse(404, "User is deleted"));

            if (user.UserType != UserType.Company) return BadRequest(new ApiResponse(400, "User is not a company"));

            var company = _mapper.Map<CompanyByIdOutputDto>(user);
            if (company == null) return NotFound(new ApiResponse(404, "Company not found"));

            // Roles
            var roles = await _userManager.GetRolesAsync(user);

            company.Roles = roles;


            return Ok(company);

        }
        #endregion


        #region Get All Companies
        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            //get all companies where isDeleted is false
            var users = await _userManager.GetUsersInRoleAsync("Company");
            var notDeleted = users.Where(c => c.IsDeleted == false).ToList();
            var companies = _mapper.Map<List<CompanyByIdOutputDto>>(notDeleted);
            return Ok(companies);
        }
        #endregion

        #region Get All Companies by Status
        [HttpGet("status/{status}")]

        public async Task<IActionResult> GetAllCompaniesByStatus([FromRoute] string status)
        {
            var users = await _userManager.GetUsersInRoleAsync("Company");
            users = users.Where(c => c.IsDeleted == false).ToList();
            var companies = _mapper.Map<List<CompanyByIdOutputDto>>(users);

            if (!Enum.TryParse<IsApprove>(status.ToString(), true, out var isApprove))
            {
                return BadRequest(new ApiResponse(400, $"Invalid status, status must be one of {string.Join(", ", Enum.GetNames(typeof(IsApprove)))}"));
            }

            companies = companies.Where(c => c.IsApprove == isApprove).ToList();

            return Ok(companies);
        }
        #endregion

        #region Delete Company
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound(new ApiResponse(404, "User not found"));
            if (user.UserType != UserType.Company) return BadRequest(new ApiResponse(400, "User is not a company"));
            user.IsDeleted = true;
            await _userManager.UpdateAsync(user);
            return Ok();
        }
        #endregion

        #region Update Company
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany([FromRoute] string id, [FromBody] CompanyUpdateDto company)
        {
            if (id != company.Id)
            {
                return BadRequest("ID in the route does not match the ID in the request body");
            }

            var existingCompany = await _userManager.FindByIdAsync(id);
            if (existingCompany == null)
            {
                return NotFound("Company not found");
            }

            _mapper.Map(company, existingCompany);

            await _userManager.UpdateAsync(existingCompany);

            return Ok();
        }
        #endregion

        #region Get All Requests By Company Id
        [HttpGet("requests/{id}")]
        public async Task<IActionResult> GetAllRequestsByCompanyId([FromRoute] string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound(new ApiResponse(404, "User not found"));
            if (user.UserType != UserType.Company) return BadRequest(new ApiResponse(400, "User is not a company"));

            var requests = await _requestService.GetRequestsByCompanyId(id);
            if (requests == null)
            {
                return NotFound(new ApiResponse(404, "No requests found for the company"));
            }

            var requestDtos = _mapper.Map<List<UserRequestDto>>(requests);
            if (requestDtos == null)
            {
                // Add logging here
                return StatusCode(500, new ApiResponse(500, "Error mapping requests"));
            }

            return Ok(requestDtos);
        }
        #endregion


        #region  Change Status 

        [HttpPut("ChangeStatus/{id}")]
        public async Task<IActionResult> ChangeRequestStatus([FromRoute] string id, [FromBody] RequestUpdatedDto request)
        {
            if (id != request.Id)
            {
                return BadRequest("ID in the route does not match the ID in the request body");
            }

            var existingRequest = await _unitOfWork.Repository<UserRequest>().GetByIdAsync(int.Parse(id));
            if (existingRequest == null)
            {
                return NotFound("Request not found");
            }
            existingRequest.Status = request.Status;

            await _unitOfWork.Repository<UserRequest>().Update(existingRequest);

            string ResaultOfRequest = (existingRequest.Status == RequestStatus.Approved && existingRequest.Status != RequestStatus.Pending) ? "congratulations ..! Your request has been approved " : "Oops..! Your request has been Rejected";

            await _unitOfWork.Repository<Notification>().AddAsync(new Notification() { UserId = existingRequest.CustomerId, Body = ResaultOfRequest });
            await _unitOfWork.CompleteAsync();


            return Ok();
        }
        #endregion

        #region get Company's Users
        [HttpGet("Users/{id}")]
        public async Task<IActionResult> GetCompanyUsers(string id)
        {
            IEnumerable<UserRequest> requestList = await _unitOfWork.Repository<UserRequest>().GetAllAsync();
            List<UserRequest> result = requestList
             .Where(r => r.InsurancePlan.CompanyId == id && r.Status == RequestStatus.Approved)
             .GroupBy(r => r.Customer.Email)
             .Select(g => g.First())
             .ToList();


            if (result.Count == 0)
            {
                return NotFound(new ApiResponse(404, "No Users yet"));
            }

            List<CompanyUsersDTO> users = result.Select(user => new CompanyUsersDTO
            {
                name = user.Customer?.Name,
                email = user.Customer?.Email,
                phone = user.Customer?.PhoneNumber
            }).ToList();

            return Ok(users);
        }
        #endregion


        #region DownloadUserData
        [HttpGet("UserPdf")]
        public async Task<IActionResult> GetUserDataAsPdf(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound(new ApiResponse(404, "User not found"));

            var userRequests = await _requestService.GetRequestsByUserId(id);

            using (var stream = new MemoryStream())
            {
                var document = new PdfDocument();
                var page = document.AddPage();
                var gfx = XGraphics.FromPdfPage(page);

                var fontBold = new XFont("Verdana", 12, XFontStyle.Bold);
                var fontRegular = new XFont("Verdana", 10, XFontStyle.Regular);

                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image", "logo.png");
                XImage image = XImage.FromFile(imagePath);
                gfx.DrawImage(image, 520, 20, 50, 50);

                gfx.DrawString("Insure Tech", fontBold, XBrushes.DarkGreen, new XRect(0, 20, page.Width, 20), XStringFormats.TopCenter);
                gfx.DrawString("User Details:", fontBold, XBrushes.Black, new XRect(20, 80, page.Width, 20), XStringFormats.TopLeft); // Adjusted Y coordinate
                gfx.DrawString($"Name: {user.Name}", fontRegular, XBrushes.Black, new XRect(20, 100, page.Width, 20), XStringFormats.TopLeft); // Adjusted Y coordinate
                gfx.DrawString($"Email: {user.Email}", fontRegular, XBrushes.Black, new XRect(20, 120, page.Width, 20), XStringFormats.TopLeft); // Adjusted Y coordinate
                gfx.DrawString($"Phone: {user.PhoneNumber}", fontRegular, XBrushes.Black, new XRect(20, 140, page.Width, 20), XStringFormats.TopLeft); // Adjusted Y coordinate

                gfx.DrawString("Insurance Plans:", fontBold, XBrushes.Black, new XRect(20, 180, page.Width, 20), XStringFormats.TopLeft); // Adjusted Y coordinate
                gfx.DrawLine(XPens.Black, 20, 200, page.Width - 20, 200); 

                int yPoint = 220; 
                foreach (var request in userRequests)
                {
                    if (yPoint + 220 > page.Height - 40)
                    {
                        page = document.AddPage();
                        gfx = XGraphics.FromPdfPage(page);
                        yPoint = 40;
                    }

                    string categoryName = GetCategoryName(request.CategoryId);
                    XBrush planNameBrush = GetPlanNameBrush(request.CategoryId);
                    XBrush levelBrush = GetLevelBrush((int)request.Level);

                    gfx.DrawRectangle(XPens.Black, 20, yPoint, page.Width - 40, 220); 
                    gfx.DrawString("Plan Name:", fontBold, XBrushes.Black, new XRect(30, yPoint + 10, 100, 20), XStringFormats.TopLeft);
                    gfx.DrawString(categoryName, fontRegular, planNameBrush, new XRect(130, yPoint + 10, 200, 20), XStringFormats.TopLeft);

                    gfx.DrawString("Plan Level:", fontBold, XBrushes.Black, new XRect(30, yPoint + 30, 100, 20), XStringFormats.TopLeft);
                    gfx.DrawString(request.Level.ToString(), fontRegular, levelBrush, new XRect(130, yPoint + 30, 100, 20), XStringFormats.TopLeft);

                    gfx.DrawString("Date:", fontBold, XBrushes.Black, new XRect(30, yPoint + 50, 100, 20), XStringFormats.TopLeft);
                    gfx.DrawString(request.Date.ToString(), fontRegular, XBrushes.Black, new XRect(130, yPoint + 50, 100, 20), XStringFormats.TopLeft);

                    gfx.DrawString("Coverage:", fontBold, XBrushes.Black, new XRect(30, yPoint + 70, 100, 20), XStringFormats.TopLeft);
                    gfx.DrawString(request.YearlyCoverage.ToString(), fontRegular, XBrushes.Black, new XRect(130, yPoint + 70, 100, 20), XStringFormats.TopLeft);

                    gfx.DrawString("Quotation:", fontBold, XBrushes.Black, new XRect(30, yPoint + 90, 100, 20), XStringFormats.TopLeft);
                    gfx.DrawString(request.Quotation.ToString(), fontRegular, XBrushes.Black, new XRect(130, yPoint + 90, 100, 20), XStringFormats.TopLeft);

                    if (categoryName == "Home Insurance")
                    {
                        var homeinsurancerequest = await _unitOfWork.Repository<HomePlanRequest>().GetByIdAsync(request.Id);
                        gfx.DrawString("Natural Hazard:", fontBold, XBrushes.Black, new XRect(30, yPoint + 110, 100, 20), XStringFormats.TopLeft);
                        gfx.DrawString(homeinsurancerequest.NaturalHazard.ToString(), fontRegular, XBrushes.Black, new XRect(180, yPoint + 110, 100, 20), XStringFormats.TopLeft);

                        gfx.DrawString("Water Damage:", fontBold, XBrushes.Black, new XRect(30, yPoint + 130, 100, 20), XStringFormats.TopLeft);
                        gfx.DrawString(homeinsurancerequest.WaterDamage.ToString(), fontRegular, XBrushes.Black, new XRect(180, yPoint + 130, 100, 20), XStringFormats.TopLeft);

                        gfx.DrawString("Attempted Theft:", fontBold, XBrushes.Black, new XRect(30, yPoint + 150, 100, 20), XStringFormats.TopLeft);
                        gfx.DrawString(homeinsurancerequest.AttemptedTheft.ToString(), fontRegular, XBrushes.Black, new XRect(180, yPoint + 150, 100, 20), XStringFormats.TopLeft);

                        gfx.DrawString("Fires And Explosion:", fontBold, XBrushes.Black, new XRect(30, yPoint + 170, 100, 20), XStringFormats.TopLeft);
                        gfx.DrawString(homeinsurancerequest.FiresAndExplosion.ToString(), fontRegular, XBrushes.Black, new XRect(180, yPoint + 170, 100, 20), XStringFormats.TopLeft);
                    }
                    else if (categoryName == "Health Insurance")
                    {
                        var healthinsurancerequest = await _unitOfWork.Repository<HealthPlanRequest>().GetByIdAsync(request.Id);

                        gfx.DrawString("Clinics Coverage:", fontBold, XBrushes.Black, new XRect(30, yPoint + 110, 100, 20), XStringFormats.TopLeft);
                        gfx.DrawString(healthinsurancerequest.ClinicsCoverage.ToString(), fontRegular, XBrushes.Black, new XRect(180, yPoint + 110, 100, 20), XStringFormats.TopLeft);

                        gfx.DrawString("Dental Coverage:", fontBold, XBrushes.Black, new XRect(30, yPoint + 130, 100, 20), XStringFormats.TopLeft);
                        gfx.DrawString(healthinsurancerequest.DentalCoverage.ToString(), fontRegular, XBrushes.Black, new XRect(180, yPoint + 130, 100, 20), XStringFormats.TopLeft);

                        gfx.DrawString("Hospitalization And Surgery:", fontBold, XBrushes.Black, new XRect(30, yPoint + 150, 100, 20), XStringFormats.TopLeft);
                        gfx.DrawString(healthinsurancerequest.HospitalizationAndSurgery.ToString(), fontRegular, XBrushes.Black, new XRect(240, yPoint + 150, 100, 20), XStringFormats.TopLeft);

                        gfx.DrawString("Medical Network:", fontBold, XBrushes.Black, new XRect(30, yPoint + 170, 100, 20), XStringFormats.TopLeft);
                        gfx.DrawString(healthinsurancerequest.MedicalNetwork.ToString(), fontRegular, XBrushes.Black, new XRect(180, yPoint + 170, 100, 20), XStringFormats.TopLeft);

                        gfx.DrawString("Optical Coverage:", fontBold, XBrushes.Black, new XRect(30, yPoint + 190, 100, 20), XStringFormats.TopLeft);
                        gfx.DrawString(healthinsurancerequest.OpticalCoverage.ToString(), fontRegular, XBrushes.Black, new XRect(180, yPoint + 190, 100, 20), XStringFormats.TopLeft);
                    }
                    else
                    {
                        var motorinsurancerequest = await _unitOfWork.Repository<MotorPlanRequest>().GetByIdAsync(request.Id);

                        gfx.DrawString("Legal Expenses:", fontBold, XBrushes.Black, new XRect(30, yPoint + 110, 100, 20), XStringFormats.TopLeft);
                        gfx.DrawString(motorinsurancerequest.LegalExpenses.ToString(), fontRegular, XBrushes.Black, new XRect(180, yPoint + 110, 100, 20), XStringFormats.TopLeft);

                        gfx.DrawString("Own Damage:", fontBold, XBrushes.Black, new XRect(30, yPoint + 130, 100, 20), XStringFormats.TopLeft);
                        gfx.DrawString(motorinsurancerequest.OwnDamage.ToString(), fontRegular, XBrushes.Black, new XRect(180, yPoint + 130, 100, 20), XStringFormats.TopLeft);

                        gfx.DrawString("Personal Accident:", fontBold, XBrushes.Black, new XRect(30, yPoint + 150, 100, 20), XStringFormats.TopLeft);
                        gfx.DrawString(motorinsurancerequest.PersonalAccident.ToString(), fontRegular, XBrushes.Black, new XRect(180, yPoint + 150, 100, 20), XStringFormats.TopLeft);

                        gfx.DrawString("Theft:", fontBold, XBrushes.Black, new XRect(30, yPoint + 170, 100, 20), XStringFormats.TopLeft);
                        gfx.DrawString(motorinsurancerequest.Theft.ToString(), fontRegular, XBrushes.Black, new XRect(180, yPoint + 170, 100, 20), XStringFormats.TopLeft);

                        gfx.DrawString("Third Party Liability:", fontBold, XBrushes.Black, new XRect(30, yPoint + 190, 100, 20), XStringFormats.TopLeft);
                        gfx.DrawString(motorinsurancerequest.ThirdPartyLiability.ToString(), fontRegular, XBrushes.Black, new XRect(180, yPoint + 190, 100, 20), XStringFormats.TopLeft);
                    }
                    yPoint += 240; 
                }
                document.Save(stream, false);

                return File(stream.ToArray(), "application/pdf", "user_data.pdf");
            }
        }

        #endregion
        private string GetCategoryName(int categoryId)
        {
            switch (categoryId)
            {
                case 1:
                    return "Health Insurance";
                case 2:
                    return "Home Insurance";
                case 3:
                    return "Motor Insurance";
                default:
                    return "Unknown Category";
            }
        }

        private XBrush GetPlanNameBrush(int categoryId)
        {
            switch (categoryId)
            {
                case 1:
                    return XBrushes.DarkGreen;
                case 2:
                    return XBrushes.DarkSeaGreen;
                case 3:
                    return XBrushes.DarkGoldenrod;
                default:
                    return XBrushes.Black;
            }
        }

        private XBrush GetLevelBrush(int level)
        {
            if (level >= 80)
            {
                return XBrushes.Green;
            }
            else if (level >= 50)
            {
                return XBrushes.Orange;
            }
            else
            {
                return XBrushes.Red;
            }
        }
    }
}
