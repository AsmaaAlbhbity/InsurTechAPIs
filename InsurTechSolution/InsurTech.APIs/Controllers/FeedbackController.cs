using AutoMapper;
using InsurTech.APIs.DTOs.FeedbackDTOs;
using InsurTech.Core;
using InsurTech.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using InsurTech.Core.Entities.Identity;
using InsurTech.Repository;
using Microsoft.AspNetCore.SignalR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.AspNetCore.Identity;
using InsurTech.APIs.Helpers;

namespace InsurTech.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHubContext<NotificationHub> _hubContext;

        public FeedbackController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager,IHubContext<NotificationHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetFeedbacks()
        {
            var feedbacks = await _unitOfWork.Repository<Feedback>().GetAllAsync();
            var feedbackDtos = _mapper.Map<List<FeedbackDto>>(feedbacks);

            return Ok(feedbackDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeedback(int id)
        {
            var feedback = await _unitOfWork.Repository<Feedback>().GetByIdAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }

            var feedbackDto = _mapper.Map<FeedbackDto>(feedback);
            return Ok(feedbackDto);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateFeedback([FromBody] CreateFeedbackInput input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //var customerId = "1"; for testing


            // Check if the user is authorized to rate this insurance plan
            var userRequests = await _unitOfWork.Repository<UserRequest>().GetAllAsync();
            var userRequest = userRequests.FirstOrDefault(r => r.CustomerId == customerId
                                                             && r.InsurancePlanId == input.InsurancePlanId
                                                             && r.Status == RequestStatus.Approved);

            if (userRequest == null)
                return BadRequest("You cannot rate this insurance plan as you haven't purchased it.");

            var feedback = _mapper.Map<Feedback>(input);
            feedback.CustomerId = customerId;

            await _unitOfWork.Repository<Feedback>().AddAsync(feedback);
            await _unitOfWork.CompleteAsync();

            var customer = await _userManager.FindByIdAsync(customerId);
            var plan = await _unitOfWork.Repository<InsurancePlan>().GetByIdAsync(feedback.InsurancePlanId);
            var company = await _userManager.FindByIdAsync(plan.CompanyId);

            var notification = new Notification()
            {
                Body = $"{customer.Name} added feedback for insurance plan ID {feedback.InsurancePlanId} belonging to {company.Name}",
                UserId = "1",
                IsRead = false
            };
            await _unitOfWork.Repository<Notification>().AddAsync(notification);
            await _hubContext.Clients.Group("admin").SendAsync("ReceiveNotification", notification.Body);

            var notificationToCompany = new Notification()
            {
                Body = $"{customer.Name} added feedback for insurance plan ID {feedback.InsurancePlanId}.",
                UserId = company.Id,
                IsRead = false
            };
           

            await _unitOfWork.Repository<Notification>().AddAsync(notification);
            await _hubContext.Clients.Group("company").SendAsync("ReceiveNotification", notification.Body);

            await _unitOfWork.CompleteAsync();

            var feedbackDto = _mapper.Map<FeedbackDto>(feedback);

            return Ok(new { Message = "Feedback submitted successfully", Feedback = feedbackDto });
        }


        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateFeedback(int id, [FromBody] CreateFeedbackInput input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var feedback = await _unitOfWork.Repository<Feedback>().GetByIdAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }

            var customerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (feedback.CustomerId != customerId && !User.IsInRole("Admin"))
            {
                return Forbid("You can only update your own feedback unless you are an admin.");
            }

            _mapper.Map(input, feedback);
            await _unitOfWork.CompleteAsync();

            var feedbackDto = _mapper.Map<FeedbackDto>(feedback);

            return Ok(new { Message = "Feedback updated successfully", Feedback = feedbackDto });
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Customer")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            var feedback = await _unitOfWork.Repository<Feedback>().GetByIdAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }

            var customerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (feedback.CustomerId != customerId && !User.IsInRole("Admin"))
            {
                return Forbid("You can only delete your own feedback unless you are an admin.");
            }

            _unitOfWork.Repository<Feedback>().Delete(feedback);
            await _unitOfWork.CompleteAsync();

            return Ok(new { Message = "Feedback deleted successfully" });
        }
    }
}
