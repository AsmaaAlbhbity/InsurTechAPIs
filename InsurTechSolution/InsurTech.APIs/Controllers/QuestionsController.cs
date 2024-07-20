using AutoMapper;
using InsurTech.APIs.DTOs.Question;
using InsurTech.APIs.Errors;
using InsurTech.Core;
using InsurTech.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace InsurTech.APIs.Controllers
{
    [Route("api/questions")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public QuestionsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestions()
        {
            var questions = await _unitOfWork.Repository<Question>().GetAllAsync();
            var questionsDto = _mapper.Map<List<QuestionsDTO>>(questions);
            return Ok(questionsDto);
        }

        [HttpGet("{id}")]   
        public async Task<IActionResult> GetQuestion(int id)
        {
            var question = await _unitOfWork.Repository<Question>().GetByIdAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            var questionDto = _mapper.Map<QuestionsDTO>(question);
            return Ok(questionDto);
        }

        [HttpPost]
       // [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionInput questionInput)
        {
            var question = _mapper.Map<Question>(questionInput);

            await _unitOfWork.Repository<Question>().AddAsync(question);
            await _unitOfWork.CompleteAsync();

            var questionDto = _mapper.Map<QuestionsDTO>(question);
            return Ok(questionDto);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> UpdateQuestion(int id, [FromBody] CreateQuestionInput questionInput)
        {
            var question = await _unitOfWork.Repository<Question>().GetByIdAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            _mapper.Map(questionInput, question);
            await _unitOfWork.CompleteAsync();

            var questionDto = _mapper.Map<QuestionsDTO>(question);
            return Ok(questionDto);
        }

        [HttpDelete("{id}")]
       // [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var question = await _unitOfWork.Repository<Question>().GetByIdAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            _unitOfWork.Repository<Question>().Delete(question);
            await _unitOfWork.CompleteAsync();

            return Ok(new ApiResponse(200, "Question deleted successfully"));
        }
    }
}
