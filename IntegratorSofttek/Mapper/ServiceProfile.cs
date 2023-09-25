using AutoMapper;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<ServiceDTO, Service>()
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.DeletedTimeUtc, opt => opt.MapFrom(src => (DateTime?)null));

        CreateMap<Service, ServiceDTO>();

        CreateMap<Service, Service>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore ID property

    }
}
