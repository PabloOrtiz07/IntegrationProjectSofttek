using System.ComponentModel.DataAnnotations;

namespace IntegratorSofttek.DTOs
{
    public class ServiceDTO
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public double HourlyRate { get; set; }
    }
}
