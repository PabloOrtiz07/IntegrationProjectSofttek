using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
using IntegratorSofttek.Logic;
using IntegratorSofttek.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var users = await _unitOfWork.UserRepository.GetAll();

            return users;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _unitOfWork.UserRepository.GetById(id);

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insert(UserDTO userDTO)
        {
            
            var user = _userMapper.MapUserDTOToUser(userDTO);
            var userReturn = await _unitOfWork.UserRepository.Insert(user);
            if (userReturn != false)
            {
                return Ok("The insert operation was successful");

            }
            return BadRequest("The operation was canceled");
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserDTO userDTO, int id)
        {
            var user = _userMapper.MapUserDTOToUser(userDTO);
            var userReturn = await _unitOfWork.UserRepository.Update(user,id);
            if (userReturn != false)
            {
                return Ok("The update operation was successful");

            }
            return BadRequest("The operation was canceled");
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _unitOfWork.UserRepository.GetById(id);

            if (user != null)
            {
                await _unitOfWork.UserRepository.Delete(id);
                return Ok("This user has been elimited: "+user);
            }
            else
            {
                return NotFound("The user couldn't be found");
            }
        }
    }
}
