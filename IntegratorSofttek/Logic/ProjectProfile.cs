using AutoMapper;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
using System;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<ProjectDTO, Project>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => MapStatusStringToEnum(src.Status)))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.DeletedTimeUtc, opt => opt.MapFrom(src => (DateTime?)null));

        CreateMap<Project, ProjectDTO>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));


    }

    private ProjectStatus MapStatusStringToEnum(string status)
    {
        switch (status.ToLower())
        {
            case "pending":
                return ProjectStatus.Pending;
            case "confirmed":
                return ProjectStatus.Confirmed;
            case "finished":
                return ProjectStatus.Finished;
            default:
                throw new ArgumentException($"Invalid status string: {status}");
        }
    }

}
