using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntegratorSofttek.Entities
{
    [Table("users")]


    public class User
    {
        [Key]
        [Column("user_id")]
        public int Id { get; set; }
        [Required]
        [Column("user_firstName", TypeName = "VARCHAR(100)")]

        public string FirstName { get; set; }
        [Required]
        [Column("user_lastName", TypeName = "VARCHAR(100)")]

        public string LastName { get; set; }
        [Required]
        [Column("user_dni", TypeName = "VARCHAR(100)")]
        public int Dni { get; set; }

        [Required]
        [Column("user_type")]
        public UserType Type { get; set; }
        [Required]
        [Column("user_password", TypeName = "VARCHAR(100)")]

        public string Password { get; set; }
        [Required]
        [Column("user_email", TypeName = "VARCHAR(100)")]
        public string Email { get; set; }

        [Required]
        [Column("user_isDeleted")]
        public bool IsDeleted { get; set; }


        [Column("user_deletedTimeUtc")]
        public DateTime? DeletedTimeUtc { get; set; }


    }

    public enum UserType
    {
        Administrator,
        Consultant
    }

}
