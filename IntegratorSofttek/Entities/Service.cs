using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntegratorSofttek.Entities
{
    [Table("services")]

    public class Service
    {
        [Key]

        [Required]
        [Column("service_id")]
        public int Id {  get; set; }

        [Required]
        [Column("service_description", TypeName = "VARCHAR(100)")]
        public string Description { get; set; }

        [Required]
        [Column("service_isActive")]
        public bool IsActive { get; set; }

        [Required]
        [Column("service_hourlyRate")]
        public double HourlyRate { get; set; }

        [Required]
        [Column("service_isDeleted")]
        public bool IsDeleted { get; set; }


        [Column("service_deletedTimeUtc")]
        public DateTime? DeletedTimeUtc { get; set; }
    }
}
