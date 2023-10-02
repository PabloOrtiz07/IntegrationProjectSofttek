namespace IntegratorSofttek.DTOs
{
    public class WorkDTO
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int HoursQuantity { get; set; }

        public double HourlyRate { get; set; }

        public double Cost { get; set; }

        public int ProjectId { get; set; }
        public ProjectDTO? ProjectDTO { get; set; }

        public int ServiceId { get; set; }
        public ServiceDTO? ServiceDTO { get; set; }
    }
}
