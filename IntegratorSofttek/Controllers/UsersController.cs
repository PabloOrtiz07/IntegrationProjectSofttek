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
        /// Requires the Administrator and Consultant policies for access.
        /// </summary>
        /// <param name="parameter">The parameter used to filter users.
        /// Use parameter 0 to return filtered non-deleted users,
        /// and use parameter 1 to retrieve all users without filtering</param>
        /// <returns>Returns a list of users.</returns>

        [HttpGet]
        [Authorize(Policy = "AdministratorAndConsultant")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers(int parameter=0)
        {


            var users = await _unitOfWork.UserRepository.GetAllUsers(parameter);
            if (users == null || !users.Any())
            {
                return NotFound();
            }
            var usersDTO = _mapper.Map<List<UserDTO>>(users);
            return Ok(usersDTO);

        }

        /// <summary>
        /// Get an user by their id.
        /// </summary>
        /// <param name="id">The ID used to find an user with this identication.</param>
        /// <param name="parameter">The parameter used to filter a non-deleted user or deleted user
        /// Use parameter 0 to return filtered non-deleted users,
        /// and use parameter 1 to retrieve all users without filtering </param>
        /// <returns>Returns an user object matching the given ID or null</returns>

        [HttpGet("{id}")]
        [Authorize(Policy = "AdministratorAndConsultant")]

        public async Task<IActionResult> GetUserById([FromRoute] int id, int parameter=0)
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
     /// <summary>
     /// Register an user in the database.
     /// </summary>
     /// <param name="userDTO">A model used to fill in user information</param>
     /// <returns>Returns "OK" if the registeration operation was succesful </returns>


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
     /// <summary>
     /// Update an user in the database
     /// </summary>
     /// <param name="id">The ID used to find an user that matche  this identification</param>
     /// <param name="userDTO">A model which will replace the older user data</param>
     /// <returns>Returns "OK" if the updating operation was succesfull</returns>
     
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
     /// <summary>
     /// Delete an user softly (soft deletion) or permanently (hard deletion).
     /// </summary>
     /// <param name="id">The ID used to find an user with this identification </param>
     /// <param name="parameter">The parameter used to select the type of deletion 
     /// Use parameter 0 to soft delete  and,
     /// use parameter 1 to hard delete </param>
     /// <returns>Returns "OK" if the delete operation was succesfull or "BadRequest" if there was and issue</returns>

        [HttpPut]
        [Route("DeleteUser/{id}")]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id,  int parameter = 0)
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
