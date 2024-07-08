using InsurTech.Core.Entities.Identity;
using InsurTech.Core.Entities;

namespace InsurTech.APIs.DTOs.RequestDTO
{
	public class UserRequestDTO
	{
		public string CustomerID { get; set; }
		public string InsurancePlanLevel { get; set; }
		public decimal YearlyCoverage { get; set; }
		public decimal Quotation { get; set; }
		public string Status { get; set; }
		public bool Paid { get; set; }

		public int catId { get; set; }
		public string Category { get; set; }
		public int planId { get; set; }
		public string companyName { get; set; }

	}
}
