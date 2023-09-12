using AutoMapper;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
using System;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<ProjectDTO, Project>()
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.DeletedTimeUtc, opt => opt.MapFrom(src => (DateTime?)null));

        CreateMap<Project, ProjectDTO>();
    }
}
