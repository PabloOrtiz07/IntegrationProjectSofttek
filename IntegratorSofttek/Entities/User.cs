using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntegratorSofttek.Entities
{
    [Table("users")]


    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Dni { get; set; }

        public int Type { get; set; }
        public string Password { get; set; }

    }

}
