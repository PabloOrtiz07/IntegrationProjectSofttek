using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;

namespace IntegratorSofttek.Logic
{
    public class ServiceMapper
    {
        public Service MapServiceDTOToService(ServiceDTO serviceDTO)
        {
            return new Service
            {
                Description = serviceDTO.Description,
                IsActive = serviceDTO.IsActive,
                HourlyRate = serviceDTO.HourlyRate,


                // Map other properties as needed
            };
        }
    }
}
