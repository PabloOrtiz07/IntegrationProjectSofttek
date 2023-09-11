using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
using IntegratorSofttek.Logic;
using IntegratorSofttek.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace IntegratorSofttek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserMapper _userMapper;

        public UsersController(IUnitOfWork unitOfWork, UserMapper userMapper)
        {
            _unitOfWork = unitOfWork;
            _userMapper = userMapper;
        }

        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _unitOfWork.UserRepository.GetAll();

            return users;
        }

        [HttpGet("{id}")]
        //[Authorize]

        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _unitOfWork.UserRepository.GetById(id);

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound("The user couldn't be found");
            }
        }
        [HttpPost]
        [Route("RegisterUser")]
        //[Authorize]

        public async Task<IActionResult> RegisterUser(UserDTO userDTO)
        {
            
            var user = _userMapper.MapUserDTOToUser(userDTO);
            var userReturn = await _unitOfWork.UserRepository.Insert(user);
            if (userReturn != false)
            {
                await _unitOfWork.Complete();
                return Ok("The register operation was successful");

            }
            return BadRequest("The operation was canceled");
        }

        [HttpPut("update/{id}")]
        //[Authorize]

        public async Task<IActionResult> UpdateUser(User user)
        {
         
            var userReturn = await _unitOfWork.UserRepository.Update(user);
            if (userReturn != false)
            {
                await _unitOfWork.Complete();
                return Ok("The update operation was successful");

            }
            return BadRequest("The operation was canceled");
        }


        [HttpPut("delete/{id}")]
        //[Authorize]

        public async Task<IActionResult> DeleteSoftUser(int id)
        {

            var userReturn = await _unitOfWork.UserRepository.DeleteSoftById(id);
            if (userReturn != false)
            {
                await _unitOfWork.Complete();
                return Ok("This user has been dropped down");

            }
            return NotFound("The user couldn't be found");
        }

        [HttpDelete("{id}")]
       // [Authorize]

        public async Task<IActionResult> DeleteHardUser(int id)
        {
            var user = await _unitOfWork.UserRepository.DeleteHardById(id);

            if (user != null)
            {
                await _unitOfWork.Complete();
                return Ok("This user has been elimited from DataBase");
            }
            else
            {
                return NotFound("The user couldn't be found");
            }
        }
    }
}
