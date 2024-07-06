using AutoMapper;
using InsurTech.Core.Entities;
using InsurTech.APIs.DTOs.FeedbackDTOs;

namespace InsurTech.APIs.MappingProfiles
{
    public class FeedbackMapProfile : Profile
    {
        public FeedbackMapProfile()
        {
            // Map from Feedback entity to FeedbackDto
            CreateMap<Feedback, FeedbackDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
                .ForMember(dest => dest.InsurancePlanName, opt => opt.MapFrom(src => src.InsurancePlan.Level.ToString()))
                .ForMember(dest => dest.InsurancePlanLevel, opt => opt.MapFrom(src => src.InsurancePlan.Level.ToString()))
            .ForMember(dest => dest.catId, opt => opt.MapFrom(src => src.InsurancePlan.CategoryId))
            .ForMember(dest => dest.yearlyConverage, opt => opt.MapFrom(src => src.InsurancePlan.YearlyCoverage));

            // Map from CreateFeedbackInput to Feedback entity
            CreateMap<CreateFeedbackInput, Feedback>();
        }
    }
}
