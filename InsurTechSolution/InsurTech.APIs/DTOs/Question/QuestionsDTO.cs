using InsurTech.Core.Entities;

namespace InsurTech.APIs.DTOs.Question
{
    public class CreateQuestionInput
    {
        public string Body { get; set; }
        public int CategoryId { get; set; }
        public QuestionType Type { get; set; }
        public string Options { get; set; }
        public string Placeholder { get; set; }
    }
    public class QuestionsDTO
	{
        public int Id { get; set; }
        public string Body { get; set; }
        public int CategoryId { get; set; }
        public QuestionType Type { get; set; }
        public string Options { get; set; }
        public string Placeholder { get; set; }
    }
    public enum QuestionType
    {
        Text,
        List,
        Checkbox,
        RadioButton
    }
}
