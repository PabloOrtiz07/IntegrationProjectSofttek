using AutoMapper;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
using IntegratorSofttek.Helper;
using IntegratorSofttek.Logic;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserDTO, User>()
      .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
      .ForMember(dest => dest.DeletedTimeUtc, opt => opt.MapFrom(src => (DateTime?)null))
      .BeforeMap((src, dest) => dest.Password = PasswordEncryptHelper.EncryptPassword(src.Password, src.Email));



        CreateMap<User, UserDTO>()
            .ForMember(dest => dest.RoleDTO, opt => opt.MapFrom(src => src.Role));


        CreateMap<UserRegisterDTO, User>()
        .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
        .ForMember(dest => dest.DeletedTimeUtc, opt => opt.MapFrom(src => (DateTime?)null))
        .BeforeMap((src, dest) => dest.Password = PasswordEncryptHelper.EncryptPassword(src.Password, src.Email))
        .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
        .ForMember(dest => dest.Role, opt => opt.MapFrom<RoleResolver>());

        CreateMap<User, User>()
                  .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore ID property
    }

}

