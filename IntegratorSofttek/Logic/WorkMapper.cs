using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
using Microsoft.Extensions.Hosting;

namespace IntegratorSofttek.Logic
{
    public class WorkMapper
    {
        public Work MapWorkDTOToWork(WorkDTO workDTO)
        {
            return new Work
            {
                Date = workDTO.Date,
                HoursQuantity = workDTO.HoursQuantity,
                HourlyRate = workDTO.HourlyRate,
                Cost = workDTO.Cost


                // Map other properties as needed
            };
        }
    }
}
