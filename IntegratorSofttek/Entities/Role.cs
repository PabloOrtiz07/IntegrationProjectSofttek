using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntegratorSofttek.Entities
{
    [Table("roles")]

    public class Role
    {

        [Key]
        [Column("role_id")]
        public int Id { get; set; }

        [Required]
        [Column("role_name")]
        public string Name { get; set; }

        [Required]
        [Column("role_description")]
        public string Description { get; set; }

        [Required]
        [Column("role_isDeleted")]
        public bool IsDeleted { get; set; }

        [Column("role_deletedTimeUtc")]
        public DateTime? DeletedTimeUtc { get; set; }

    }
}
