﻿using AutoMapper;
using Castle.Core.Resource;
using InsurTech.APIs.DTOs;
using InsurTech.APIs.DTOs.Customer;
using InsurTech.APIs.DTOs.Question;
using InsurTech.APIs.DTOs.RequestDTO;
using InsurTech.APIs.DTOs.RequestQuestions;
using InsurTech.APIs.Errors;
using InsurTech.Core;
using InsurTech.Core.Entities;
using InsurTech.Core.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InsurTech.APIs.Controllers
{
    [Route("api/Customers")]
    [ApiController]
    //[Authorize(Roles=Roles.Customer)]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public CustomerController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        #region GetQusetionsByCategory
        [HttpGet("GetQusetionsByCategory/{id}")]
        //[Authorize]
        //[Authorize(Roles = Roles.Customer)]
        //[Authorize(Roles = Roles.Admin)]

        public async Task<ActionResult> GetQusetionsByCategory([FromRoute] int id)
        {
            //var username = User.FindFirst(ClaimTypes.GivenName)?.Value;
            //Console.WriteLine($"username: \n{username}");
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //Console.WriteLine($"userId: \n{userId}");

            //var email = User.FindFirstValue(ClaimTypes.Email);
            //Console.WriteLine($"email: \n{email}");

            //var userType = User.FindFirstValue(ClaimTypes.UserData);
            //Console.WriteLine($"userType: \n{userType}");

            var questions = await _unitOfWork.Repository<Question>().GetAllAsync();
            var CategoryQuestions = questions.Where(q => q.CategoryId == id).ToList();
            var questionsDto = _mapper.Map<List<QuestionsDTO>>(CategoryQuestions);
            return Ok(questionsDto);
        }
        #endregion

        #region Request Insurance Plan
        [HttpPost("requestInsurancePlan")]
        //[Authorize]
        public async Task<ActionResult> RequestAnswersWithInsurancePlan(ApplyForPlanInput applyForInsurancePlanInput)
        {
            try
            {
                //var customerId = applyForInsurancePlanInput.CustomerId; //we could get current user id from token and remove this line + remove from ApplyForInsurancePlanInput + add [Authorize] attribute to the method

                var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var insurancePlanId = applyForInsurancePlanInput.InsurancePlanId;
                var CheckInsuranceCategoury = await _unitOfWork.Repository<InsurancePlan>().GetByIdAsync(insurancePlanId);

                InsurancePlan insurance;

                switch (CheckInsuranceCategoury.Category.Name)
                {
                    case "HealthInsurance":
                        insurance = await _unitOfWork.Repository<HealthInsurancePlan>().GetByIdAsync(insurancePlanId);
                        break;
                    case "HomeInsurance":
                        insurance = await _unitOfWork.Repository<HomeInsurancePlan>().GetByIdAsync(insurancePlanId);
                        break;
                    case "MotorInsurance":
                        insurance = await _unitOfWork.Repository<MotorInsurancePlan>().GetByIdAsync(insurancePlanId);
                        break;
                    default:
                        throw new ArgumentException("Invalid Plan Type");
                }

                if (insurance == null)
                {
                    return NotFound(new ApiResponse(404, "Insurance Plan not found"));
                }

                var userRequests = await _unitOfWork.Repository<UserRequest>().GetAllAsync();

                var existingRequest = userRequests.FirstOrDefault(r => r.CustomerId == customerId && r.InsurancePlanId == insurancePlanId);//we could add a method to the generic repository to get by predicate 

                if (existingRequest != null)
                {
                    return BadRequest(new ApiResponse(400, "Request Already Exists"));
                }

                UserRequest newRequest = CheckInsuranceCategoury.Category.Name switch
                {
                    "HealthInsurance" => new HealthPlanRequest
                    {
                        CustomerId = customerId,
                        InsurancePlanId = insurancePlanId,
                        YearlyCoverage = ((HealthInsurancePlan)insurance).YearlyCoverage,
                        Quotation = insurance.Quotation,
                        CategoryId = insurance.CategoryId,
                        ClinicsCoverage = ((HealthInsurancePlan)insurance).ClinicsCoverage,
                        DentalCoverage = ((HealthInsurancePlan)insurance).DentalCoverage,
                        HospitalizationAndSurgery = ((HealthInsurancePlan)insurance).HospitalizationAndSurgery,
                        OpticalCoverage = ((HealthInsurancePlan)insurance).OpticalCoverage,
                        MedicalNetwork = ((HealthInsurancePlan)insurance).MedicalNetwork,
                        Level = ((HealthInsurancePlan)insurance).Level
                    },
                    "HomeInsurance" => new HomePlanRequest
                    {
                        CustomerId = customerId,
                        InsurancePlanId = insurancePlanId,
                        YearlyCoverage = ((HomeInsurancePlan)insurance).YearlyCoverage,
                        Quotation = insurance.Quotation,
                        CategoryId = insurance.CategoryId,
                        WaterDamage = ((HomeInsurancePlan)insurance).WaterDamage,
                        AttemptedTheft = ((HomeInsurancePlan)insurance).AttemptedTheft,
                        FiresAndExplosion = ((HomeInsurancePlan)insurance).FiresAndExplosion,
                        GlassBreakage = ((HomeInsurancePlan)insurance).GlassBreakage,
                        NaturalHazard = ((HomeInsurancePlan)insurance).NaturalHazard,
                        Level = ((HomeInsurancePlan)insurance).Level
                    },
                    "MotorInsurance" => new MotorPlanRequest
                    {
                        CustomerId = customerId,
                        InsurancePlanId = insurancePlanId,
                        YearlyCoverage = ((MotorInsurancePlan)insurance).YearlyCoverage,
                        Quotation = insurance.Quotation,
                        CategoryId = insurance.CategoryId,
                        OwnDamage = ((MotorInsurancePlan)insurance).OwnDamage,
                        LegalExpenses = ((MotorInsurancePlan)insurance).LegalExpenses,
                        PersonalAccident = ((MotorInsurancePlan)insurance).PersonalAccident,
                        ThirdPartyLiability = ((MotorInsurancePlan)insurance).ThirdPartyLiability,
                        Theft = ((MotorInsurancePlan)insurance).Theft,
                        Level = ((MotorInsurancePlan)insurance).Level
                    },
                    _ => throw new ArgumentException("Invalid Plan Type")
                };


                await _unitOfWork.Repository<UserRequest>().AddAsync(newRequest);
                await _unitOfWork.CompleteAsync();

                var allRequests = await _unitOfWork.Repository<UserRequest>().GetAllAsync();

                //get the last request
                var userRequest = allRequests.LastOrDefault();

                if (userRequest == null)
                {
                    throw new Exception("Request Not Created");
                }

                var UserRequestAnswers = applyForInsurancePlanInput.Answers.Select(a => new RequestQuestion
                {
                    QuestionId = a.QuestionId,
                    Answer = a.Answer,
                    UserRequestId = userRequest.Id
                }).ToList();

                await _unitOfWork.Repository<RequestQuestion>().AddListAsync(UserRequestAnswers);

                await _unitOfWork.CompleteAsync();

                return Ok(new ApiResponse(200, "Request Created Successfully"));
            }
            catch (Exception ex)
            {
                //await _unitOfWork.Repository<UserRequest>().Delete(new UserRequest { CustomerId = CustomerId, InsurancePlanId = applyForInsurancePlanInput.InsurancePlanId });

                //await _unitOfWork.CompleteAsync();

                // Send notification to admin and company

                var plan = await _unitOfWork.Repository<InsurancePlan>().GetByIdAsync(applyForInsurancePlanInput.InsurancePlanId);
                var adminNotification = new Notification
                {
                    Body = $"A new insurance request has been created .",
                    UserId = "1"
                };

                var companyNotification = new Notification
                {
                    Body = $"A new insurance request has been created .",
                    UserId = plan.CompanyId,
                    IsRead = false
                };

                await _unitOfWork.Repository<Notification>().AddAsync(adminNotification);
                await _unitOfWork.Repository<Notification>().AddAsync(companyNotification);
                await _unitOfWork.CompleteAsync();

                return BadRequest(new ApiResponse(400, ex.Message));
            }
        }

        #endregion

      

		#region GetCustomerRequests
		[HttpGet("GetCustomerRequests/{customerId}")]
        public async Task<ActionResult> GetCustomerRequests([FromRoute] string customerId)
        {
            var userRequests = await _unitOfWork.Repository<UserRequest>().GetAllAsync();

            var customerRequests = userRequests.Where(r => r.CustomerId == customerId).ToList();
            List<UserRequest> requests = [];

            foreach (UserRequest req in customerRequests)
            {
                req.InsurancePlan = await _unitOfWork.Repository<InsurancePlan>().GetByIdAsync(req.InsurancePlanId);
                requests.Add(req);
            }
            List<UserRequestDTO> customerRequestsDto = [];
            foreach (UserRequest req in requests)
            {
                customerRequestsDto.Add(
                    new UserRequestDTO
                    {
						CustomerID = req.CustomerId,
                        InsurancePlanLevel = req.InsurancePlan.Level.ToString(),
                        YearlyCoverage = req.InsurancePlan.YearlyCoverage,
                        Quotation = req.InsurancePlan.Quotation,
                        Status = req.Status.ToString(),
                        planId = req.Id
                    }
                    );

            }

			return Ok(customerRequestsDto);
		}
		#endregion

		#region checkCustomerHasInsurancePlans 
		[HttpGet("checkCustomerHasInsurancePlans/{catId}")]
		public async Task<ActionResult> checkCustomerHasInsurancePlans([FromRoute] int catId)
		{
			var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			//var customerId = "b7f9468b-6f42-4d61-b100-6461ffeba8dd";
			//return array of true or false for all insurance plan based on user requested it before or not
			var userRequests = await _unitOfWork.Repository<UserRequest>().GetAllAsync();
			var insurancePlans = await _unitOfWork.Repository<InsurancePlan>().GetAllAsync();
			var userRequestsIds = userRequests.Where(r => r.CustomerId == customerId).Select(r => r.InsurancePlanId).ToList();
			var insurancePlansIds = insurancePlans.Where(i => i.CategoryId == catId).Select(i => i.Id).ToList();
			var result = insurancePlansIds.Select(i => userRequestsIds.Contains(i)).ToList();
			return Ok(result);
		}



		#endregion

		#region GetRequestQuestions
		[HttpGet("GetRequestQuestions/{requestId}")]
        public async Task<ActionResult> GetRequestQuestionsAndAnswers([FromRoute] int requestId)
        {
            var requestQuestions = await _unitOfWork.Repository<RequestQuestion>().GetAllAsync();

            var questions = requestQuestions.Where(r => r.UserRequestId == requestId).ToList();


            foreach (RequestQuestion req in questions)
            {
                req.Question = await _unitOfWork.Repository<Question>().GetByIdAsync(req.QuestionId);

            }
            List<RequestQuestionDTO> requestQuestionsDto = [];
            foreach (RequestQuestion req in questions)
            {
                requestQuestionsDto.Add(
                                        new RequestQuestionDTO
                                        {
                                            Id = req.Id,
                                            QuestionId = req.QuestionId.ToString(),
                                            body = req.Question.Body,
                                            Answer = req.Answer
                                        });
            }

            return Ok(requestQuestionsDto);
        }
        #endregion

        #region GetRequestStatus
        [HttpGet("GetRequestStatus/{requestId}")]
        public async Task<ActionResult> GetRequestStatus([FromRoute] int requestId)
        {
            var userRequests = await _unitOfWork.Repository<UserRequest>().GetAllAsync();

            var userRequest = userRequests.FirstOrDefault(r => r.Id == requestId);

            if (userRequest == null)
            {
                return BadRequest(new ApiResponse(400, "Request Not Found"));
            }

            return Ok(userRequest.Status.ToString());
        }
        #endregion

        #region GetCustomersWithPagination
        [HttpGet("GetCustomersWithPagination")]
        public async Task<ActionResult> GetCustomersWithPagination([FromQuery] PaginationDTO pagination)
        {
            var customers = await _userManager.Users.Where(u => u.UserType == UserType.Customer && u.IsDeleted == false).Skip((pagination.Page - 1) * pagination.ItemsPerPage).Take(pagination.ItemsPerPage).ToListAsync();
            if (customers.Count == 0) return NotFound(new ApiResponse(404, "No Customers Found"));
            var customersDto = _mapper.Map<List<GetCustomerDTO>>(customers);
            return Ok(customersDto);
        }
        #endregion

        #region GetCustomerByEmail
        [HttpGet("GetCustomerByEmail/{email}")]
        public async Task<ActionResult> GetCustomerByEmail([FromRoute] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null) return NotFound(new ApiResponse(404, "User not found"));
            if (user.IsDeleted == true) return BadRequest(new ApiResponse(400, "User is deleted"));
            if (user.UserType != UserType.Customer) return BadRequest(new ApiResponse(400, "User is not a Customer"));
            var customer = _mapper.Map<GetCustomerDTO>(user);
            return Ok(customer);
        }
        #endregion

        #region GetCustomerByUserName
        [HttpGet("GetCustomerByUserName/{userName}")]
        public async Task<ActionResult> GetCustomerByUserName([FromRoute] string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null) return NotFound(new ApiResponse(404, "User not found"));
            if (user.UserType != UserType.Customer) return BadRequest(new ApiResponse(400, "User is not a Customer"));
            if (user.IsDeleted == true) return BadRequest(new ApiResponse(400, "User is deleted"));
            var customer = _mapper.Map<GetCustomerDTO>(user);
            return Ok(customer);
        }
        #endregion

        #region GetCustomerById
        [HttpGet("GetCustomerById/{customerId}")]
        public async Task<ActionResult> GetCustomerById([FromRoute] string customerId)
        {
            var user = await _userManager.FindByIdAsync(customerId);

            if (user is null) return NotFound(new ApiResponse(404, "User not found"));

            if (user.IsDeleted == true) return BadRequest(new ApiResponse(400, "User is deleted"));

            if (user.UserType != UserType.Customer) return BadRequest(new ApiResponse(400, "User is not a Customer"));

            if (user == null) return NotFound(new ApiResponse(404, "Customer not found"));
            var customer = _mapper.Map<GetCustomerDTO>(user);


            return Ok(customer);
        }

        #endregion

        #region CreateCustomer
        [HttpPost("CreateCustomer")]
        public async Task<ActionResult> CreateCustomer(RegisterCustomerInput model)
        {
            if (await _userManager.FindByEmailAsync(model.EmailAddress) != null) return BadRequest(new ApiResponse(400, "Email is already taken"));
            if (await _userManager.FindByNameAsync(model.UserName) != null) return BadRequest(new ApiResponse(400, "UserName is already taken"));

            var User = new Customer
            {
                Email = model.EmailAddress,
                UserName = model.UserName,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                IsApprove = IsApprove.approved,
                NationalID = model.NationalId,
                BirthDate = DateOnly.Parse(model.BirthDate),
                UserType = UserType.Customer
            };

            var Result = await _userManager.CreateAsync(User, model.Password);

            if (!Result.Succeeded) return BadRequest(new ApiResponse(400, "Error in creating user"));

            await _userManager.AddToRoleAsync(User, Roles.Customer);


            return Ok(new ApiResponse(200, "Customer Registered Successfully"));
        }
        #endregion

        #region ChangeRequestPaidStatus
        [HttpPut("ChangeRequestPaidStatus")]
		public async Task<ActionResult> ChangeRequestPaidStatus(int insurancePlanId)
        { 
            var userRequests = await _unitOfWork.Repository<UserRequest>().GetAllAsync();
            var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUser == null) return BadRequest(
                                              new ApiResponse(400, "User Not Found"));
            var userRequest = userRequests.FirstOrDefault(r => r.CustomerId == currentUser && r.InsurancePlanId == insurancePlanId);
            if (userRequest == null)
            {
                return BadRequest(new ApiResponse(400, "Request Not Found"));
            }
            userRequest.Paid = true;
            await _unitOfWork.CompleteAsync();
            return Ok(new ApiResponse(200, "Request Paid Successfully"));
        }
        #endregion
        #region CancelPaid 
        [HttpPut("CancelPaid")]
        public async Task<ActionResult> CancelPaid(int insurancePlanId)
        {
            var userRequests = await _unitOfWork.Repository<UserRequest>().GetAllAsync();
            var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUser == null) return BadRequest(
                                                             new ApiResponse(400, "User Not Found"));
            var userRequest = userRequests.FirstOrDefault(r => r.CustomerId == currentUser && r.InsurancePlanId == insurancePlanId);
            if (userRequest == null)
            {
                return BadRequest(new ApiResponse(400, "Request Not Found"));
            }
            userRequest.Paid = false;
            await _unitOfWork.CompleteAsync();
            return Ok(new ApiResponse(200, "Request Paid Successfully"));
        }
        #endregion



        #region getUserRequestswithPendingStatus
        [HttpGet("getUserRequestswithPendingStatus")]
        public async Task<ActionResult> getUserRequestswithPendingStatus()
        {
            var userRequests = await _unitOfWork.Repository<UserRequest>().GetAllAsync();
            var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUser == null) return BadRequest(
                               new ApiResponse(400, "User Not Found"));
            var userRequest = userRequests.Where(r => r.CustomerId == currentUser && r.Status == RequestStatus.Pending).ToList();
            if (userRequest == null)
            {
                return BadRequest(new ApiResponse(400, "Request Not Found"));
            }
            List<UserRequest> requests = [];

            foreach (UserRequest req in userRequest)
            {
                req.InsurancePlan = await _unitOfWork.Repository<InsurancePlan>().GetByIdAsync(req.InsurancePlanId);
                requests.Add(req);
            }
            List<UserRequestDTO> customerRequestsDto = [];
            foreach (UserRequest req in requests)
            {
                customerRequestsDto.Add(
                    new UserRequestDTO
                    {
                        CustomerID = req.CustomerId,
                        InsurancePlanLevel = req.InsurancePlan.Level.ToString(),
                        YearlyCoverage = req.InsurancePlan.YearlyCoverage,
                        Quotation = req.InsurancePlan.Quotation,
                        Status = req.Status.ToString(),
                        catId = req.CategoryId,
                        Category = "hi",
                        planId = req.InsurancePlanId,
                        companyName = req.InsurancePlan.Company.Name,
                        Paid = req.Paid
                    }
                    );

            }
            return Ok( customerRequestsDto );
        }
        #endregion
        #region getRequestsWtihPaidTrue
        [HttpGet("getRequestsWtihPaidTrue")]
        public async Task<ActionResult> getRequestsWtihPaidTrue()
        {
            var userRequests = await _unitOfWork.Repository<UserRequest>().GetAllAsync();
            var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUser == null) return BadRequest(
                                              new ApiResponse(400, "User Not Found"));
            var userRequest = userRequests.Where(r => r.CustomerId == currentUser && r.Paid == true).ToList();
            if (userRequest == null)
            {
                return BadRequest(new ApiResponse(400, "Request Not Found"));
            }
            List<UserRequest> requests = [];

            foreach (UserRequest req in userRequest)
            {
                req.InsurancePlan = await _unitOfWork.Repository<InsurancePlan>().GetByIdAsync(req.InsurancePlanId);
                requests.Add(req);
            }
            List<UserRequestDTO> customerRequestsDto = [];
            foreach (UserRequest req in requests)
            {
                customerRequestsDto.Add(
                    new UserRequestDTO
                    {
                        CustomerID = req.CustomerId,
                        InsurancePlanLevel = req.InsurancePlan.Level.ToString(),
                        YearlyCoverage = req.InsurancePlan.YearlyCoverage,
                        Quotation = req.InsurancePlan.Quotation,
                        Status = req.Status.ToString(),
                        catId = req.CategoryId,
                        planId = req.InsurancePlanId,
                        companyName = req.InsurancePlan.Company.Name,
                        Paid = req.Paid


                    }
                    );

            }
            return Ok(customerRequestsDto);
        }
        #endregion
        #region getRequestsWtihApprovedStatusbutNotPaid
        [HttpGet("getRequestsWtihApprovedStatusbutNotPaid")]
        public async Task<ActionResult> getRequestsWtihApprovedStatusbutNotPaid()
        {
            var userRequests = await _unitOfWork.Repository<UserRequest>().GetAllAsync();
            var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUser == null) return BadRequest(
                                                             new ApiResponse(400, "User Not Found"));
            var userRequest = userRequests.Where(r => r.CustomerId == currentUser && r.Status == RequestStatus.Approved && r.Paid == false).ToList();
            if (userRequest == null)
            {
                return BadRequest(new ApiResponse(400, "Request Not Found"));
            }
            List<UserRequest> requests = [];

            foreach (UserRequest req in userRequest)
            {
                req.InsurancePlan = await _unitOfWork.Repository<InsurancePlan>().GetByIdAsync(req.InsurancePlanId);
                requests.Add(req);
            }
            List<UserRequestDTO> customerRequestsDto = [];
            foreach (UserRequest req in requests)
            {
                customerRequestsDto.Add(
                    new UserRequestDTO
                    {
                        CustomerID = req.CustomerId,
                        InsurancePlanLevel = req.InsurancePlan.Level.ToString(),
                        YearlyCoverage = req.InsurancePlan.YearlyCoverage,
                        Quotation = req.InsurancePlan.Quotation,
                        Status = req.Status.ToString(),
                        catId = req.CategoryId,
                        planId = req.InsurancePlanId,
                        companyName = req.InsurancePlan.Company.Name,
                        Paid = req.Paid


                    }
                    );

            }
            return Ok(customerRequestsDto);
        }
        #endregion
        #region getRequestsWtihRejectedStatus
        [HttpGet("getRequestsWtihRejectedStatus")]
        public async Task<ActionResult> getRequestsWtihRejectedStatus()
        {
            var userRequests = await _unitOfWork.Repository<UserRequest>().GetAllAsync();
            var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUser == null) return BadRequest(
                                                                            new ApiResponse(400, "User Not Found"));
            var userRequest = userRequests.Where(r => r.CustomerId == currentUser && r.Status == RequestStatus.Rejected).ToList();
            if (userRequest == null)
            {
                return BadRequest(new ApiResponse(400, "Request Not Found"));
            }
            List<UserRequest> requests = [];

            foreach (UserRequest req in userRequest)
            {
                req.InsurancePlan = await _unitOfWork.Repository<InsurancePlan>().GetByIdAsync(req.InsurancePlanId);
                requests.Add(req);
            }
            List<UserRequestDTO> customerRequestsDto = [];
            foreach (UserRequest req in requests)
            {
                customerRequestsDto.Add(
                                       new UserRequestDTO
                                       {
                                           CustomerID = req.CustomerId,
                                           InsurancePlanLevel = req.InsurancePlan.Level.ToString(),
                                           YearlyCoverage = req.InsurancePlan.YearlyCoverage,
                                           Quotation = req.InsurancePlan.Quotation,
                                           Status = req.Status.ToString(),
                                           catId = req.CategoryId,
                                           planId = req.InsurancePlanId,
                                           companyName = req.InsurancePlan.Company.Name,
                                           Paid = req.Paid

                                       });

            }
            return Ok(customerRequestsDto);
        }
        #endregion

        #region GetCustomer
  //      [HttpGet("GetCustomer")]
  //      public async Task<ActionResult> GetCustomer()
  //      {
		//	var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
		//	var user = await _userManager.FindByIdAsync(customerId);
		//	if (user is null) return NotFound(new ApiResponse(404, "User not found"));
		//	if (user.UserType != UserType.Customer) return BadRequest(new ApiResponse(400, "User is not a Customer"));
		//	if (user.IsDeleted == true) return BadRequest(new ApiResponse(400, "User is deleted"));
		//	var customer = _mapper.Map<GetCustomerDTO>(user);
		//	return Ok(customer);
		//}
        #endregion





        #region UpdateCustomer
        [HttpPut("UpdateCustomer")]
        public async Task<ActionResult> UpdateCustomer(UpdateCustomerDTO model)
        {
            dynamic existingCustomer = await _userManager.FindByIdAsync(model.Id);
            if (existingCustomer == null)
            {
                return BadRequest(new ApiResponse(400, "Customer Not Found"));
            }
            if (await _userManager.FindByEmailAsync(model.Email) != null) return BadRequest(new ApiResponse(400, "Email is already taken"));

            if (await _userManager.FindByNameAsync(model.UserName) != null) return BadRequest(new ApiResponse(400, "UserName is already taken"));

            existingCustomer.Email = model.Email;
            existingCustomer.UserName = model.UserName;
            existingCustomer.Name = model.Name;
            existingCustomer.PhoneNumber = model.PhoneNumber;
            existingCustomer.NationalID = model.NationalId;
            existingCustomer.BirthDate = DateOnly.Parse(model.BirthDate);


            await _userManager.UpdateAsync(existingCustomer);
            return Ok(new ApiResponse(200, "Customer Updated Successfully"));
        }
        #endregion


        #region DeleteCustomer
        [HttpDelete("DeleteCustomer/{customerId}")]
        public async Task<ActionResult> DeleteCustomer([FromRoute] string customerId)
        {
            var user = await _userManager.FindByIdAsync(customerId);
            if (user is null) return NotFound(new ApiResponse(404, "User not found"));
            if (user.UserType != UserType.Customer) return BadRequest(new ApiResponse(400, "User is not a Customer"));
            user.IsDeleted = true;
            await _userManager.UpdateAsync(user);
            return Ok();
        }
        #endregion
    }
}
