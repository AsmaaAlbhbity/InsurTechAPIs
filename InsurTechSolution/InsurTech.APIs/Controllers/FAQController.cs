using AutoMapper;
using InsurTech.APIs.DTOs.FAQDTOs;
using InsurTech.APIs.Helpers;
using InsurTech.Core;
using InsurTech.Core.Entities;
using InsurTech.Core.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InsurTech.APIs.Controllers
{
    [Route("api/FAQs")]
    [ApiController]
    public class FAQController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHubContext<NotificationHub> _hubContext;

        public FAQController(IUnitOfWork unitOfWork, IMapper mapper,UserManager<AppUser> userManager,IHubContext<NotificationHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetFAQs()
        {
            var faqs = await _unitOfWork.Repository<FAQ>().GetAllAsync();

            var faqDtos = _mapper.Map<List<FAQDTO>>(faqs);

            return Ok(faqDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFAQ(int id)
        {
            var faq = await _unitOfWork.Repository<FAQ>().GetByIdAsync(id);
            if (faq == null)
            {
                return NotFound();
            }

            var faqDto = _mapper.Map<FAQDTO>(faq);

            return Ok(faqDto);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> CreateFAQ([FromBody] CreateFAQInput createFAQInput)
        {
            var faq = _mapper.Map<FAQ>(createFAQInput);

            faq.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _unitOfWork.Repository<FAQ>().AddAsync(faq);

            await _unitOfWork.CompleteAsync();

            var faqDto = _mapper.Map<FAQDTO>(faq);

            // Create and send notifications

            // Fetch all approved customers
            var approvedUsers = await _userManager.Users
         .Where(u => (u.UserType == UserType.Customer || u.UserType == UserType.Company) && u.IsApprove == IsApprove.approved)
         .ToListAsync();

            foreach (var user in approvedUsers)
            {
                var notification = new Notification
                {
                    Body = $"A new FAQ titled '{faq.Body}' has been added. Check it out!",
                    UserId = user.Id,
                    IsRead = false
                };

                await _unitOfWork.Repository<Notification>().AddAsync(notification);

                if (user.UserType == UserType.Customer)
                {
                    await _hubContext.Clients.Group("customer").SendAsync("ReceiveNotification", notification.Body);
                }
                else if (user.UserType == UserType.Company)
                {
                    await _hubContext.Clients.Group("company").SendAsync("ReceiveNotification", notification.Body);
                }
            }

                await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetFAQ), new { id = faq.Id }, faqDto);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Admin)]

        public async Task<IActionResult> UpdateFAQ([FromRoute] int id, [FromBody] CreateFAQInput createFAQInput)
        {
            var faq = await _unitOfWork.Repository<FAQ>().GetByIdAsync(id);

            if (faq == null)
            { return NotFound(); }

            _mapper.Map<CreateFAQInput, FAQ>(createFAQInput, faq);

            await _unitOfWork.CompleteAsync();

            var faqDto = _mapper.Map<FAQDTO>(faq);

            return Ok(faqDto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]

        public async Task<IActionResult> DeleteFAQ(int id)
        {
            var faq = await _unitOfWork.Repository<FAQ>().GetByIdAsync(id);

            if (faq == null)
            { return NotFound(); }


            await _unitOfWork.Repository<FAQ>().Delete(faq);

            await _unitOfWork.CompleteAsync();


            return Ok("FAQ deleted successfully.");

        }

    }
}
