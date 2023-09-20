﻿using AlkemyUmsa.Infrastructure;
using AutoMapper;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
using IntegratorSofttek.Helper;
using IntegratorSofttek.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace IntegratorSofttek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController>_logger;

        public UsersController(IUnitOfWork unitOfWork,IMapper mapper, ILogger<UsersController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Gets a list of users.
        /// </summary>
        /// <param name="parameter">
        ///  **The parameter is used to filter users.**
        /// - Use parameter 0 to return filtered non-deleted users.
        /// 
        /// - Use parameter 1 to retrieve all users without filtering.
        /// </param>
        /// <param name="pageSize">
        /// **The pageSize is used to indicate how many elements you want per page.**
        /// </param>
        /// <param name="pageToShow">
        /// **The pageToShow is used to indicate on which page you will be.**
        /// </param>
        /// <returns>Returns a list of users with an HTTP 200 response.</returns>
        /// 

        [HttpGet]
        [Authorize(Policy = "AdministratorAndConsultant")]
        public async Task<IActionResult> GetAll(int parameter=0, int pageSize=10, int pageToShow=1)
        {

            try
            {
                var users = await _unitOfWork.UserRepository.GetAllUsers(parameter);
                var usersDTO = _mapper.Map<List<UserDTO>>(users);
                if (Request.Query.ContainsKey("page")) int.TryParse(Request.Query["page"], out pageToShow);
                var url = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}").ToString();
                var paginateUsers = PaginateHelper.Paginate(usersDTO, pageToShow, url, pageSize);
                return ResponseFactory.CreateSuccessResponse(200, paginateUsers);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "A surprise error happened");
                return ResponseFactory.CreateErrorResponse(500, "A surprise error happened");

            }

        }

        /// <summary>
        /// Get an user.
        /// </summary>
        /// <param name="id">
        /// **The ID is used to find an user with this identication.**
        /// </param>
        /// 
        /// <param name="parameter">
        /// **The parameter is used to filter a non-deleted user or deleted user**
        /// - Use parameter 0 to return filtered non-deleted users.
        /// 
        /// - Use parameter 1 to retrieve all users without filtering. 
        /// </param>
        /// <returns>
        /// Returns a HTTP 200 response with the user object matching the given ID 
        /// if found, or a HTTP 404 response with an error message if the user is not found.
        /// </returns>
        /// 

        [HttpGet("{id}")]
        [Authorize(Policy = "AdministratorAndConsultant")]
        public async Task<IActionResult> GetById([FromRoute] int id, int parameter=0)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetUserById(id, parameter);

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
            catch(Exception ex)
            {
                _logger.LogError(ex, "A surprise error happened");
                return ResponseFactory.CreateErrorResponse(500, "A surprise error happened");
            }
 
        }

        /// <summary>
        /// Register an user in the database.
        /// </summary>
        /// <param name="userDTO">
        /// **A model is used to fill in user information.**
        /// </param>
        /// <returns>Returns an HTTP 201 response if the registration operation was successful 
        /// or an Error HTTP 400 response.</returns>
        /// 

        [HttpPost]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            try
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
            catch(Exception ex)
            {
                _logger.LogError(ex, "A surprise error happened");
                return ResponseFactory.CreateErrorResponse(500, "A surprise error happened");
            }
        }

        /// <summary>
        /// Update an user.
        /// </summary>
        /// <param name="id">**The ID is used to find an user that matches this identification.**</param>
        /// 
        /// <param name="userDTO">**A model which will replace the older user data.**</param>
        /// <returns>Returns an HTTP 200 response if the updating operation was successful 
        /// or an Error HTTP 400 response.</returns>
        /// 

        [HttpPut("{id}")]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Update([FromRoute] int id, UserDTO userDTO)
        {
            try
            {
                var user = _mapper.Map<User>(userDTO);
                var result = await _unitOfWork.UserRepository.Update(user, id);

                if (result != null)
                {
                    await _unitOfWork.Complete();
                    return ResponseFactory.CreateSuccessResponse(200, "The updating operation was successful");

                }
                return ResponseFactory.CreateErrorResponse(400, "The operation was canceled");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "A surprise error happened");
                return ResponseFactory.CreateErrorResponse(500, "A surprise error happened");
            }

        }

        /// <summary>
        /// Delete an user softly (soft deletion) or permanently (hard deletion).
        /// </summary>
        /// <param name="id">**The ID used to find an user with this identification.**</param>
        /// 
        /// <param name="parameter">
        /// **The parameter is used to select the type of deletion.**
        /// - Use parameter 0 to soft delete.
        /// 
        /// - Use parameter 1 to hard delete.</param>
        /// <returns>Returns an HTTP 204 response if the deletion operation was successful 
        /// or an Error HTTP 400 response.</returns>
        /// 

        [HttpPut]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Delete([FromRoute] int id,  int parameter = 0)
        {
            try
            {
                var userReturn = await _unitOfWork.UserRepository.DeleteUserById(id, parameter);

                if (userReturn != false)
                {
                    await _unitOfWork.Complete();
                    return ResponseFactory.CreateSuccessResponse(204, "The deletion operation was successful");

                }

                return ResponseFactory.CreateErrorResponse(400, "The operation was canceled");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "A surprise error happened");
                return ResponseFactory.CreateErrorResponse(500, "A surprise error happened");
            }
        }

    }
}
