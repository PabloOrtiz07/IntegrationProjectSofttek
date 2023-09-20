using IntegratorSofttek.Entities;

namespace IntegratorSofttek.DTOs
{
    public class UserDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Dni { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public int RoleId { get; set; }

        public Role? Role { get; set; }

    }


}
