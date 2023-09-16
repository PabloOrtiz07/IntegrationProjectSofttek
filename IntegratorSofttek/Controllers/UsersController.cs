﻿using AlkemyUmsa.Infrastructure;
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
        /// Gets a list of users.
        /// </summary>
        /// <param name="parameter">
        ///  **The parameter used to filter users.**
        /// - Use parameter 0 to return filtered non-deleted users.
        /// 
        /// - Use parameter 1 to retrieve all users without filtering.
        /// </param>
        /// <returns>Returns a list of users with a HTTP 200 response .</returns>

        [HttpGet]
        [Authorize(Policy = "AdministratorAndConsultant")]

        public async Task<IActionResult> GetAllUsers(int parameter=0)
        {


            var users = await _unitOfWork.UserRepository.GetAllUsers(parameter);
            var usersDTO = _mapper.Map<List<UserDTO>>(users);
            return ResponseFactory.CreateSuccessResponse(200, usersDTO);

        }

        /// <summary>
        /// Get an user.
        /// </summary>
        /// <param name="id">
        /// **The ID used to find an user with this identication.**
        /// </param>
        /// 
        /// <param name="parameter">
        /// **The parameter used to filter a non-deleted user or deleted user**
        /// - Use parameter 0 to return filtered non-deleted users.
        /// 
        /// - Use parameter 1 to retrieve all users without filtering. 
        /// </param>
        /// <returns>
        /// Returns a HTTP 200 response with the user object matching the given ID 
        /// if found, or a HTTP 404 response with an error message if the user is not found.
        /// </returns>

        [HttpGet("{id}")]
        [Authorize(Policy = "AdministratorAndConsultant")]

        public async Task<IActionResult> GetUserById([FromRoute] int id, int parameter=0)
        {
            var user = await _unitOfWork.UserRepository.GetUserById(id,parameter);

            if (user != null)
            {
                var userDTO = _mapper.Map<UserDTO>(user);
                return ResponseFactory.CreateSuccessResponse(200, userDTO);
            }
            else
            {
                return ResponseFactory.CreateErrorResponse(404, "The user couldn't be found");
            }
        }

        /// <summary>
        /// Register an user in the database.
        /// </summary>
        /// <param name="userDTO">
        /// **A model used to fill in user information.**
        /// </param>
        /// <returns>Returns an HTTP 201 response if the registration operation was successful 
        /// or an Error HTTP 400 response.</returns>

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
                return ResponseFactory.CreateSuccessResponse(201, "The register operation was successful");


            }
            return ResponseFactory.CreateErrorResponse(400, "The operation was canceled");

        }
        /// <summary>
        /// Update an user.
        /// </summary>
        /// <param name="id">**The ID used to find an user that matches this identification.**</param>
        /// 
        /// <param name="userDTO">**A model which will replace the older user data.**</param>
        /// <returns>Returns an HTTP 200 response if the updating operation was successful 
        /// or an Error HTTP 400 response.</returns>

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
                return ResponseFactory.CreateSuccessResponse(200, "The updating operation was successful");

            }
            return ResponseFactory.CreateErrorResponse(400, "The operation was canceled");
        }
        /// <summary>
        /// Delete an user softly (soft deletion) or permanently (hard deletion).
        /// </summary>
        /// <param name="id">**The ID used to find an user with this identification.**</param>
        /// 
        /// <param name="parameter">
        /// **The parameter used to select the type of deletion.**
        /// - Use parameter 0 to soft delete.
        /// 
        /// - Use parameter 1 to hard delete.</param>
        /// <returns>Returns an HTTP 204 response if the deletion operation was successful 
        /// or an Error HTTP 400 response.</returns>

        [HttpPut]
        [Route("DeleteUser/{id}")]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id,  int parameter = 0)
        {

            var userReturn = await _unitOfWork.UserRepository.DeleteUserById(id, parameter);

            if (userReturn != false)
            {
                await _unitOfWork.Complete();
                return ResponseFactory.CreateSuccessResponse(204, "The deletion operation was successful");

            }

            return ResponseFactory.CreateErrorResponse(400, "The operation was canceled");
        }

    }
}
