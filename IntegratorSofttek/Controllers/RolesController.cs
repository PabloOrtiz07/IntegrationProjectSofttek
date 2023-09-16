using AlkemyUmsa.Infrastructure;
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

    public class RolesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RolesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a list of roles.
        /// </summary>
        /// <param name="parameter">
        ///  **The parameter used to filter roles.**
        /// - Use parameter 0 to return filtered non-deleted roles.
        /// 
        /// - Use parameter 1 to retrieve all roles without filtering.
        /// </param>
        /// <returns>Returns a list of roles with a HTTP 200 response .</returns>

        [HttpGet]
        [Authorize(Policy = "AdministratorAndConsultant")]

        public async Task<IActionResult> GetAllRoles(int parameter = 0)
        {


            var roles = await _unitOfWork.RoleRepository.GetAllRoles(parameter);
            var rolesDTO = _mapper.Map<List<RoleDTO>>(roles);
            return ResponseFactory.CreateSuccessResponse(200, rolesDTO);

        }

        /// <summary>
        /// Get a role.
        /// </summary>
        /// <param name="id">
        /// **The ID used to find a role with this identication.**
        /// </param>
        /// 
        /// <param name="parameter">
        /// **The parameter used to filter a non-deleted role or deleted role**
        /// - Use parameter 0 to return filtered non-deleted roles.
        /// 
        /// - Use parameter 1 to retrieve all roles without filtering. 
        /// </param>
        /// <returns>
        /// Returns a HTTP 200 response with the role object matching the given ID 
        /// if found, or a HTTP 404 response with an error message if the role is not found.
        /// </returns>

        [HttpGet("{id}")]
        [Authorize(Policy = "AdministratorAndConsultant")]

        public async Task<IActionResult> GetRoleById([FromRoute] int id, int parameter = 0)
        {
            var role = await _unitOfWork.RoleRepository.GetRoleById(id, parameter);

            if (role != null)
            {
                var roleDTO = _mapper.Map<RoleDTO>(role);
                return ResponseFactory.CreateSuccessResponse(200, roleDTO);
            }
            else
            {
                return ResponseFactory.CreateErrorResponse(404, "The role couldn't be found");
            }
        }

        /// <summary>
        /// Register a role in the database.
        /// </summary>
        /// <param name="roleDTO">
        /// **A model used to fill in role information.**
        /// </param>
        /// <returns>Returns an HTTP 201 response if the registration operation was successful 
        /// or an Error HTTP 400 response.</returns>

        [HttpPost]
        [Route("RegisterRole")]
        [Authorize(Policy = "Administrator")]

        public async Task<IActionResult> RegisterRole(RoleDTO roleDTO)
        {
            var role = _mapper.Map<Role>(roleDTO);
            var result = await _unitOfWork.RoleRepository.Insert(role);
            if (result != false)
            {
                await _unitOfWork.Complete();
                return ResponseFactory.CreateSuccessResponse(201, "The register operation was successful");


            }
            return ResponseFactory.CreateErrorResponse(400, "The operation was canceled");

        }
        /// <summary>
        /// Update a role.
        /// </summary>
        /// <param name="id">**The ID used to find a role that matches this identification.**</param>
        /// 
        /// <param name="roleDTO">**A model which will replace the older role data.**</param>
        /// <returns>Returns an HTTP 200 response if the updating operation was successful 
        /// or an Error HTTP 400 response.</returns>

        [HttpPut]
        [Route("UpdateRole/{id}")]
        [Authorize(Policy = "Administrator")]

        public async Task<IActionResult> UpdateRole([FromRoute] int id, RoleDTO roleDTO)
        {
            var role = _mapper.Map<Role>(roleDTO);
            var result = await _unitOfWork.RoleRepository.Update(role, id);

            if (result != null)
            {
                await _unitOfWork.Complete();
                return ResponseFactory.CreateSuccessResponse(200, "The updating operation was successful");

            }
            return ResponseFactory.CreateErrorResponse(400, "The operation was canceled");
        }
        /// <summary>
        /// Delete a role softly (soft deletion) or permanently (hard deletion).
        /// </summary>
        /// <param name="id">**The ID used to find a role with this identification.**</param>
        /// 
        /// <param name="parameter">
        /// **The parameter used to select the type of deletion.**
        /// - Use parameter 0 to soft delete.
        /// 
        /// - Use parameter 1 to hard delete.</param>
        /// 
        /// <returns>Returns an HTTP 204 response if the deletion operation was successful 
        /// or an Error HTTP 400 response.</returns>

        [HttpPut]
        [Route("DeleteRole/{id}")]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> DeleteRole([FromRoute] int id, int parameter = 0)
        {

            var roleReturn = await _unitOfWork.RoleRepository.DeleteRoleById(id, parameter);

            if (roleReturn != false)
            {
                await _unitOfWork.Complete();
                return ResponseFactory.CreateSuccessResponse(204, "The deletion operation was successful");

            }

            return ResponseFactory.CreateErrorResponse(400, "The operation was canceled");
        }

    }
}
