using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;

namespace IntegratorSofttek.Logic
{
    public class UserMapper
    {
        public User MapUserDTOToUser(UserDTO userDTO)
        {
            return new User
            {
                FirstName = userDTO.FirstName,
                Password = userDTO.Password,
                Dni = userDTO.Dni,
                Type = userDTO.Type,


                // Map other properties as needed
            };
        }

        //public UserDTO MapUserToUserDTO(User user)
        //{
        //    return new UserDTO
        //    {
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        // Map other properties as needed
        //    };
        //}
    }
}
