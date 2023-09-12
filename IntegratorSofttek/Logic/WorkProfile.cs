using AutoMapper;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;

public class WorkProfile : Profile
{
    public WorkProfile()
    {
        CreateMap<WorkDTO, Work>()
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.DeletedTimeUtc, opt => opt.MapFrom(src => (DateTime?)null));

        CreateMap<Work, WorkDTO>();
    }
}
