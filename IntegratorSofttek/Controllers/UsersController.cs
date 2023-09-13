using AutoMapper;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
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
        private readonly IMapper _mapper;

        public UsersController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a list of users based on a parameter.
        /// Requires The Administrador and the Consultant policy for access 
        /// </summary>
        /// <param name="parameter">The parameter is used to filter users.
        /// Use parameter 1 filter for non-deleted users
        /// and parameter 2 to return all of them without filters</param>
        /// <returns>Returns a list of users</returns>

        [HttpGet]
        [Authorize(Policy = "AdministratorAndConsultant")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers([FromQuery] int parameter)
        {
            var users = await _unitOfWork.UserRepository.GetAllUsers(parameter);
            var usersDTO = _mapper.Map<List<UserDTO>>(users);
            return Ok(usersDTO);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>

        [HttpGet("{id}")]
        [Authorize(Policy = "AdministratorAndConsultant")]

        public async Task<IActionResult> GetUserById([FromRoute] int id, [FromQuery] int parameter)
        {
            var user = await _unitOfWork.UserRepository.GetUserById(id,parameter);

            if (user != null)
            {
                var userDTO = _mapper.Map<UserDTO>(user);
                return Ok(userDTO);
            }
            else
            {
                return NotFound("The user couldn't be found");
            }
        }

        [HttpPost]
        [Route("RegisterUser")]
        [Authorize(Policy = "Administrator")]

        public async Task<IActionResult> RegisterUser(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            var result = await _unitOfWork.UserRepository.Insert(user);
            if (result != false)
            {
                await _unitOfWork.Complete();
                return Ok("The register operation was successful");

            }
            return BadRequest("The operation was canceled");
        }

        [HttpPut]
        [Route("UpdateUser/{id}")]
        [Authorize(Policy = "Administrator")]

        public async Task<IActionResult> UpdateUser([FromRoute] int id, UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);

            var result = await _unitOfWork.UserRepository.Update(user,id);
            if (result != null)
            {
                await _unitOfWork.Complete();
                return Ok("The update operation was successful");

            }
            return BadRequest("The operation was canceled");
        }


        [HttpPut]
        [Route("DeleteUser/{id}")]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id, [FromQuery] int parameter)
        {

            var userReturn = await _unitOfWork.UserRepository.DeleteUserById(id,parameter);
            if (userReturn != false)
            {
                await _unitOfWork.Complete();
                return Ok("This user has been dropped down");

            }
            return NotFound("The user couldn't be found");
        }

    }
}
