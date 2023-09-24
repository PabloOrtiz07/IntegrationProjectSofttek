using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntegratorSofttek.Entities
{
    [Table("works")]

    public class Work
    {
        [Key]
        [Required]
        [Column("work_id")]
        public int Id{ get; set; }

        [Required]
        [Column("work_date")]
        public DateTime Date { get; set; }

        [ForeignKey("ServiceId")]
        public int ServiceId { get; set; }

        public Service Service { get; set; }

        [ForeignKey("ProjectId")]
        public int ProjectId { get; set; }

        public Project Project { get; set; }

        [Required]
        [Column("work_hoursQuantity")]
        public int HoursQuantity { get; set; }
        [Required]
        [Column("work_hourlyRate")]
        public double HourlyRate { get; set; }
        [Required]
        [Column("work_cost")]
        public double Cost { get; set; }
        [Required]
        [Column("work_isDeleted")]
        public bool IsDeleted { get; set; }


        [Column("work_deletedTimeUtc")]
        public DateTime? DeletedTimeUtc { get; set; }

    }
}
