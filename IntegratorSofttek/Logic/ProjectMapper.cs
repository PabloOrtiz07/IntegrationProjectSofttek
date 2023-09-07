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
                Adress = projectDTO.Adress,


                // Map other properties as needed
            };
        }
    }
}
