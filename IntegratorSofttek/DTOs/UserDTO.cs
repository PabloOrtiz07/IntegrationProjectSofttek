using IntegratorSofttek.Entities;

namespace IntegratorSofttek.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Dni { get; set; }

        public string Email { get; set; }

        public int RoleId { get; set; }

        public RoleDTO? RoleDTO { get; set; }

    }


}
