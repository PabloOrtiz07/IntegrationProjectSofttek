using IntegratorSofttek.DataAccess.Repositories.Interfaces;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
using Microsoft.EntityFrameworkCore;
using IntegratorSofttek.Helper;
using AutoMapper;
using System.Data.Common;

namespace IntegratorSofttek.DataAccess.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IMapper _mapper;

        public UserRepository(ContextDB contextDB, IMapper mapper) : base(contextDB)
        {
            _mapper=mapper;
        }

        public  async Task<bool> UpdateUser(UserRegisterDTO userRegisterDTO,int id, int parameter)
        {
            try
            {

                var userFinding = await GetById(id);

                if (userFinding == null) {

                    return false;

                }
                if (parameter == 0) {

                    var user = _mapper.Map<User>(userRegisterDTO);
                    _mapper.Map(user, userFinding);
                    _contextDB.Update(userFinding);
                    return true;

                }
                if(userFinding.IsDeleted != false && parameter == 1 )
                {
                    userFinding.IsDeleted = false;
                    userFinding.DeletedTimeUtc = null;
                    _contextDB.Update(userFinding);
                    return true;

                }

                return false;
            }
            catch (Exception)
            {
                
                return false;
            }
        }


        public virtual async Task<List<UserDTO>> GetAllUsers(int parameter)
        {
            try
            {
                if (parameter == 0)
                {
                    List<User> users = await _contextDB.Users
                        .Include(user => user.Role)
                        .Where(user => user.IsDeleted != true)
                        .ToListAsync();

                    return _mapper.Map<List<UserDTO>>(users);
                }
                else if (parameter == 1)
                {
                    List<User> users = await _contextDB.Users.Include(user => user.Role).ToListAsync();
                    return _mapper.Map<List<UserDTO>>(users);
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }

        }


        public async Task<UserDTO> GetUserById(int id, int parameter)
        {
            try
            {
                User user = await _contextDB.Users
                            .Include(u => u.Role)
                            .Where(u => u.Id == id)
                            .FirstOrDefaultAsync();

                if(user == null)
                {
                    return null;
                }

                if (user.IsDeleted != true && parameter == 0)
                {
                    return _mapper.Map<UserDTO>(user);
                }
                if (parameter == 1)
                {
                    return _mapper.Map<UserDTO>(user);
                }
                return null;

            }
            catch (Exception)
            {
                return null;
            }
        }
        public  async Task<bool> DeleteUserById(int id,int parameter)
        {

            try
            {
                User userFinding = await base.GetById(id);

                if (userFinding == null)
                {
                    return false;
                }

                if (parameter == 0)
                {
                    userFinding.IsDeleted = true;
                    userFinding.DeletedTimeUtc = DateTime.UtcNow;
                    _contextDB.Update(userFinding);

                    return true;
                }
                if (parameter == 1)
                {
                    _contextDB.Users.Remove(userFinding);
                    return true;
                }

                return false;
            }
            catch(Exception) {
                return false;
            }

        }

        public virtual async Task<bool> InsertUser(UserRegisterDTO userRegisterDTO)
        {
            try
            {
                var user = _mapper.Map<User>(userRegisterDTO);
                var response = await base.Insert(user);
                return response;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<User?> AuthenticateCredentials(AuthenticateDto dto)
        {

            try
            {
                return await _contextDB.Users.Include(user => user.Role).SingleOrDefaultAsync
                              (user => user.Email == dto.Email && user.Password == PasswordEncryptHelper.EncryptPassword(dto.Password, dto.Email));
            }
            catch (Exception) {
                return null;
            }
           
        }
  

    }
}