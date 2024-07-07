using InsurTech.APIs.CustomeValidation;
using InsurTech.Core.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace InsurTech.APIs.DTOs.Customer
{
    public class CustomerDto
    {
        public string Id { get; set; }

        public string? Name { get; set; }
        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? NationalId { get; set; }


        public string? BirthDate { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Image { get; set; }
    }
}
