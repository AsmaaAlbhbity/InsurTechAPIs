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
            //if (userRequests == null || !userRequests.Any()) return NotFound(new ApiResponse(404, "No Requests yet"));

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

                gfx.DrawString("Insure Tech", fontBold, XBrushes.DarkGreen,
                    new XRect(0, 20, page.Width, 20),
                    XStringFormats.TopCenter);

                gfx.DrawString("User Details:", fontBold, XBrushes.Black, new XRect(20, 140, page.Width, 20), XStringFormats.TopLeft);
                gfx.DrawString($"Name: {user.Name}", fontRegular, XBrushes.Black, new XRect(20, 160, page.Width, 20), XStringFormats.TopLeft);
                gfx.DrawString($"Email: {user.Email}", fontRegular, XBrushes.Black, new XRect(20, 180, page.Width, 20), XStringFormats.TopLeft);
                gfx.DrawString($"Phone: {user.PhoneNumber}", fontRegular, XBrushes.Black, new XRect(20, 200, page.Width, 20), XStringFormats.TopLeft);
                gfx.DrawString("Insurance Plans:", fontBold, XBrushes.Black, new XRect(20, 240, page.Width, 20), XStringFormats.TopLeft);
                gfx.DrawLine(XPens.Black, 20, 260, page.Width - 20, 260);

                gfx.DrawString("Request ID", fontBold, XBrushes.Black, new XRect(20, 280, 100, 20), XStringFormats.TopLeft);
                gfx.DrawString("Plan Name", fontBold, XBrushes.Black, new XRect(120, 280, 200, 20), XStringFormats.TopLeft);
                gfx.DrawString("Plan Level", fontBold, XBrushes.Black, new XRect(220, 280, 200, 20), XStringFormats.TopLeft);
                gfx.DrawString("Coverage", fontBold, XBrushes.Black, new XRect(320, 280, 100, 20), XStringFormats.TopLeft);
                gfx.DrawString("Quotation", fontBold, XBrushes.Black, new XRect(420, 280, 100, 20), XStringFormats.TopLeft);

                gfx.DrawLine(XPens.Black, 20, 300, page.Width - 20, 300);

                int yPoint = 320;
                foreach (var request in userRequests)
                {
                    gfx.DrawString(request.Id.ToString(), fontRegular, XBrushes.Black, new XRect(20, yPoint, 100, 20), XStringFormats.TopLeft);
                    gfx.DrawString(request.InsurancePlan.Category.Name, fontRegular, XBrushes.Black, new XRect(120, yPoint, 100, 20), XStringFormats.TopLeft);
                    gfx.DrawString(request.InsurancePlan.Level.ToString(), fontRegular, XBrushes.Black, new XRect(220, yPoint, 100, 20), XStringFormats.TopLeft);
                    gfx.DrawString(request.InsurancePlan.YearlyCoverage.ToString(), fontRegular, XBrushes.Black, new XRect(320, yPoint, 200, 20), XStringFormats.TopLeft);
                    gfx.DrawString(request.InsurancePlan.Quotation.ToString(), fontRegular, XBrushes.Black, new XRect(420, yPoint, 100, 20), XStringFormats.TopLeft);
                    yPoint += 20;
                }
                document.Save(stream, false);

                return File(stream.ToArray(), "application/pdf", "user_data.pdf");
            }
        }
        #endregion

    }
}
