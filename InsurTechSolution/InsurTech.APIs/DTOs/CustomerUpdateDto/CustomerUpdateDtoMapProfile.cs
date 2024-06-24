using AutoMapper;
using InsurTech.APIs.DTOs.Customer;
using InsurTech.Core.Entities.Identity;

namespace InsurTech.APIs.DTOs.CustomerUpdateDto
{
	public class CustomerUpdateDtoMapProfile :Profile
	{
		public CustomerUpdateDtoMapProfile()
		{
			CreateMap<UpdateCustomerDTO, InsurTech.Core.Entities.Identity.Customer>()
				.ForMember(dest => dest.NationalID, opt => opt.MapFrom(src => src.NationalId))
				.ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
				.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
				.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => DateOnly.Parse(src.BirthDate)));
		}
	}
}
