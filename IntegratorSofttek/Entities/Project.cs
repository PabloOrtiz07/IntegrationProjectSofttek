using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntegratorSofttek.Entities
{
    [Table("projects")]
    public class Project
    {
        [Key]
        [Required]
        [Column("project_id")]
        public int Id { get; set; }


        [Required]
        [Column("project_name", TypeName = "VARCHAR(100)")]
        public string Name { get; set; }


        [Required]
        [Column("project_address", TypeName = "VARCHAR(100)")]
        public string Address { get; set; }

        [Required]
        [Column("project_status")]
        public ProjectStatus Status { get; set; }

        [Required]
        [Column("project_isDeleted")]
        public bool IsDeleted { get; set; }


        [Column("project_deletedTimeUtc")]
        public DateTime? DeletedTimeUtc { get; set; }

    }

    public enum ProjectStatus
    {
        Pending,
        Confirmed,
        Finished
    }
}
