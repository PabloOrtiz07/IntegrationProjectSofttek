using AlkemyUmsa.Infrastructure;
using AutoMapper;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
using IntegratorSofttek.Helper;
using IntegratorSofttek.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegratorSofttek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase // Update controller class name
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServicesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a list of services.
        /// </summary>
        /// <param name="parameter">
        /// **The parameter is used to filter services.**
        /// - Use parameter 0 to return filtered non-deleted services.
        /// 
        /// - Use parameter 1 to retrieve all services without filtering.
        /// 
        /// - Use parameter 2 to return filtered non-deleted users with active status.
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

        public async Task<IActionResult> GetAll(int parameter = 0, int pageSize = 10, int pageToShow = 1)
        {

            var services = await _unitOfWork.ServiceRepository.GetAllServices(parameter);
            var servicesDTO = _mapper.Map<List<ServiceDTO>>(services);
            if (Request.Query.ContainsKey("page")) int.TryParse(Request.Query["page"], out pageToShow);
            var url = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}").ToString();
            var paginateServices = PaginateHelper.Paginate(servicesDTO, pageToShow, url, pageSize);
            return ResponseFactory.CreateSuccessResponse(200, paginateServices);
        }

        /// <summary>
        /// Get a service.
        /// </summary>
        /// <param name="id">
        /// **The ID is used to find a service with this identication**.</param>
        /// <param name="parameter">
        /// **The parameter is used to filter a non-deleted service or deleted service**
        /// - Use parameter 0 to return filtered non-deleted service.
        /// 
        /// - Use parameter 1 to retrieve all services without filtering. </param>
        /// <returns>
        /// Returns a HTTP 200 response with the service object matching the given ID 
        /// if found, or a HTTP 404 response with an error message if the service is not found.
        /// </returns>
        /// 

        [HttpGet("{id}")]
        [Authorize(Policy = "AdministratorAndConsultant")]
        public async Task<IActionResult> GetById([FromRoute] int id,int parameter = 0)
        {
            var service = await _unitOfWork.ServiceRepository.GetServiceById(id, parameter); 

            if (service != null)
            {
                var serviceDTO = _mapper.Map<ServiceDTO>(service);
                return ResponseFactory.CreateSuccessResponse(200, serviceDTO);

            }
            else
            {
                return ResponseFactory.CreateErrorResponse(404, "The service couldn't be found");
            }
        }

        /// <summary>
        /// Register a service in the database.
        /// </summary>
        /// 
        /// <param name="serviceDTO">
        /// **A model is used to fill in service information**</param>
        /// <returns>Returns an HTTP 201 response if the registration operation was successful 
        /// or an Error HTTP 400 response.</returns>

        [HttpPost]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Register(ServiceDTO serviceDTO)
        {
            var service = _mapper.Map<Service>(serviceDTO);
            var result = await _unitOfWork.ServiceRepository.Insert(service); // Update repository method call
            if (result != false)
            {
                await _unitOfWork.Complete();
                return ResponseFactory.CreateSuccessResponse(201, "The register operation was successful");
            }
            return ResponseFactory.CreateErrorResponse(400, "The operation was canceled");
        }

        /// <summary>
        /// Update a service in the database.
        /// </summary>
        /// <param name="id">
        /// **The ID is used to find a service that matches this identification.**</param>
        /// <param name="serviceDTO">
        /// **A model is used to replace the older service data.**</param>
        /// <returns>Returns an HTTP 200 response if the updating operation was successful 
        /// or an Error HTTP 400 response.</returns>
        [HttpPut]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Update([FromRoute] int id, ServiceDTO serviceDTO)
        {
            var service = _mapper.Map<Service>(serviceDTO);

            var result = await _unitOfWork.ServiceRepository.Update(service, id); // Update repository method call
            if (result != null)
            {
                await _unitOfWork.Complete();
                return ResponseFactory.CreateSuccessResponse(200, "The updating operation was successful");
            }
            return ResponseFactory.CreateErrorResponse(400, "The operation was canceled");
        }

        /// <summary>
        /// Delete a service softly (soft deletion) or permanently (hard deletion).
        /// </summary>
        /// <param name="id">
        /// **The ID is used to find a service with this identification.**</param>
        /// <param name="parameter">
        /// **The parameter is used to select the type of deletion.** 
        /// - Use parameter 0 to soft delete.
        /// 
        /// - Use parameter 1 to hard delete. </param>
        /// <returns>Returns an HTTP 204 response if the deletion operation was successful 
        /// or an Error HTTP 400 response.</returns>
        /// 

        [HttpPut("{id}")]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Delete([FromRoute] int id, int parameter = 0)
        {
            var serviceReturn = await _unitOfWork.ServiceRepository.DeleteServiceById(id, parameter); // Update repository method call
            if (serviceReturn != false)
            {
                await _unitOfWork.Complete();
                return ResponseFactory.CreateSuccessResponse(204, "The deletion operation was successful");
            }
            return ResponseFactory.CreateErrorResponse(400, "The operation was canceled");
        }
    }
}
