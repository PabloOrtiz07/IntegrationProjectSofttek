using IntegratorSofttek.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntegratorSofttek.DTOs
{
    public class WorkDTO
    {
        public DateTime Date { get; set; }

        public int HoursQuantity { get; set; }

        public double HourlyRate { get; set; }

        public double Cost { get; set; }
    }
}
