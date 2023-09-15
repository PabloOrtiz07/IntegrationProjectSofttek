using AutoMapper;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
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
        /// Gets a list of services based on a parameter.
        /// Requires the Administrator and Consultant policies for access.
        /// </summary>
        /// <param name="parameter">The parameter used to filter services.
        /// Use parameter 0 to return filtered non-deleted services,
        /// use parameter 1 to retrieve all services without filtering,
        /// and use parameter 2 to return filtered non-deleted users with active status.
        /// </param>
        /// <returns>Returns a list of services.</returns>

        [HttpGet]
        [Authorize(Policy = "AdministratorAndConsultant")]

        public async Task<ActionResult<IEnumerable<ServiceDTO>>> GetAllServices(int parameter = 0)
        {

            var services = await _unitOfWork.ServiceRepository.GetAllServices(parameter);
            if (services == null || !services.Any())
            {
                return NotFound();
            }
            var servicesDTO = _mapper.Map<List<ServiceDTO>>(services);
            return Ok(servicesDTO);
        }
        /// <summary>
        /// Get a service by their id.
        /// Requires the Administrator and Consultant policies for access.
        /// </summary>
        /// <param name="id">The ID used to find a service with this identication.</param>
        /// <param name="parameter">The parameter used to filter a non-deleted service or deleted service
        /// Use parameter 0 to return filtered non-deleted service,
        /// and use parameter 1 to retrieve all services without filtering </param>
        /// <returns>Returns a service object matching the given ID or null</returns>

        [HttpGet("{id}")]
        [Authorize(Policy = "AdministratorAndConsultant")]
        public async Task<IActionResult> GetServiceById([FromRoute] int id,int parameter = 0)
        {
            var service = await _unitOfWork.ServiceRepository.GetServiceById(id, parameter); 

            if (service != null)
            {
                var serviceDTO = _mapper.Map<ServiceDTO>(service);
                return Ok(serviceDTO);
            }
            else
            {
                return NotFound("The service couldn't be found");
            }
        }

        /// <summary>
        /// Register a service in the database.
        /// Requires the Administrator policies for access.
        /// </summary>
        /// <param name="serviceDTO">A model used to fill in service information</param>
        /// <returns>Returns "OK" if the registeration operation was succesful </returns>

        [HttpPost]
        [Route("RegisterService")]
        [Authorize(Policy = "Administrator")]

        public async Task<IActionResult> RegisterService(ServiceDTO serviceDTO)
        {
            var service = _mapper.Map<Service>(serviceDTO);
            var result = await _unitOfWork.ServiceRepository.Insert(service); // Update repository method call
            if (result != false)
            {
                await _unitOfWork.Complete();
                return Ok("The register operation was successful");
            }
            return BadRequest("The operation was canceled");
        }

        /// <summary>
        /// Update a service in the database
        /// Requires the Administrator policies for access.
        /// </summary>
        /// <param name="id">The ID used to find a service that matche  this identification</param>
        /// <param name="serviceDTO">A model which will replace the older service data</param>
        /// <returns>Returns "OK" if the updating operation was succesfull</returns>

        [HttpPut]
        [Route("UpdateService/{id}")]
        [Authorize(Policy = "Administrator")]

        public async Task<IActionResult> UpdateService([FromRoute] int id, ServiceDTO serviceDTO)
        {
            var service = _mapper.Map<Service>(serviceDTO);

            var result = await _unitOfWork.ServiceRepository.Update(service, id); // Update repository method call
            if (result != null)
            {
                await _unitOfWork.Complete();
                return Ok("The update operation was successful");
            }
            return BadRequest("The operation was canceled");
        }

        /// <summary>
        /// Delete a service softly (soft deletion) or permanently (hard deletion).
        /// Requires the Administrator policies for access.
        /// </summary>
        /// <param name="id">The ID used to find a service with this identification </param>
        /// <param name="parameter">The parameter used to select the type of deletion 
        /// Use parameter 0 to soft delete  and,
        /// use parameter 1 to hard delete </param>
        /// <returns>Returns "OK" if the delete operation was succesfull or "BadRequest" if there was and issue</returns>

        [HttpPut]
        [Route("DeleteService/{id}")]
        [Authorize(Policy = "Administrator")]

        public async Task<IActionResult> DeleteService([FromRoute] int id, int parameter = 0)
        {
            var serviceReturn = await _unitOfWork.ServiceRepository.DeleteServiceById(id, parameter); // Update repository method call
            if (serviceReturn != false)
            {
                await _unitOfWork.Complete();
                return Ok("This service has been dropped down");
            }
            return NotFound("The service couldn't be found");
        }
    }
}
