using InsurTech.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsurTech.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IUploadService _uploadService;

        public UploadController(IUploadService uploadService)
        {
            _uploadService = uploadService;
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
    }
}
