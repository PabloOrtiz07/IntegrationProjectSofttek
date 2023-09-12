using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;

namespace IntegratorSofttek.Logic
{
    public class ProjectMapper
    {
        public Project MapProjectDTOToProject(ProjectDTO projectDTO)
        {
            return new Project
            {
                Name = projectDTO.Name,
                Status = projectDTO.Status,
                Address = projectDTO.Address,


                // Map other properties as needed
            };
        }
    }
}
