using AutoMapper;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
using IntegratorSofttek.Helper;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<RoleDTO, Role>();

        CreateMap<Role, RoleDTO>();

        CreateMap<Role, Role>()
          .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore ID property
    }
}
