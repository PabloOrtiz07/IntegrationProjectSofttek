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

        [ForeignKey("Id")]
        public  int Project { get; set; }

        [ForeignKey("Id")]
        public int Service { get; set; }

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
