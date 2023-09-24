using AutoMapper;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;

public class WorkProfile : Profile
{
    public WorkProfile()
    {
        CreateMap<WorkRegisterDTO, Work>()
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.DeletedTimeUtc, opt => opt.MapFrom(src => (DateTime?)null));


        CreateMap<Work, WorkDTO>()
            .ForMember(dest => dest.ServiceDTO, opt => opt.MapFrom(src => src.Service))
            .ForMember(dest => dest.ProjectDTO, opt => opt.MapFrom(src => src.Project));

        CreateMap<Work, Work>()
          .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore ID property
    }
}
