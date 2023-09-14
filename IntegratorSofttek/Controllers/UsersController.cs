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
        /// Use parameter 1 to retrieve all users without filtering,
        /// and use the default parameter to return filtered non-deleted users.</param>
        /// <returns>Returns a list of users.</returns>

        [HttpGet]
        [Authorize(Policy = "AdministratorAndConsultant")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers(int parameter=0, int pageNumber=1, int pageSize=10)
        {


            var users = await _unitOfWork.UserRepository.GetAllUsers(parameter);
            if (users == null || !users.Any())
            {
                return NotFound();
            }
            var usersDTO = _mapper.Map<List<UserDTO>>(users);
            PagedResponse pagedResponse = new PagedResponse(pageNumber, pageSize);
            var (totalCount, totalPages, usersDTOPerPage) = pagedResponse.CalculatePagination(usersDTO, pagedResponse);

            return Ok(new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                Users = usersDTOPerPage
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>

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
