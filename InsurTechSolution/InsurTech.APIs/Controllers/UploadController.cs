using AutoMapper;
using InsurTech.APIs.DTOs.Customer;
using InsurTech.Core.Entities.Identity;
using InsurTech.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InsurTech.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IUploadService _uploadService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UploadController(IUploadService uploadService, UserManager<AppUser> userManager, IMapper mapper)
        {
            _uploadService = uploadService;
            _userManager = userManager;
            _mapper = mapper;
        }
   

        [HttpPost("image")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var url = "";

            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            using (var stream = file.OpenReadStream())
            {
                url = await _uploadService.UploadImageAsync(stream, file.FileName);
            }

            return Ok(new
            {
                message = "File uploaded successfully",
                url = url
            });
        }
        [HttpGet("/api/customers/profile")]
        [Authorize]

        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var customer = _mapper.Map<CustomerDto>(user);
            return Ok(customer);
        }




        [HttpPost("/api/profile/image")]
        [Authorize]
        public async Task<IActionResult> UploadProfileImage(IFormFile file)
        {
            var url = "";

            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            using (var stream = file.OpenReadStream())
            {
                url = await _uploadService.UploadImageAsync(stream, file.FileName);
            }

            user.Image = url;

            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                message = "File uploaded successfully",
                url = url
            });

        }
    }
}
