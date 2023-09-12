using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
using IntegratorSofttek.Logic;
using IntegratorSofttek.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegratorSofttek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ServicesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ServiceMapper _serviceMapper;

        public ServicesController(IUnitOfWork unitOfWork, ServiceMapper serviceMapper)
        {
            _unitOfWork = unitOfWork;
            _serviceMapper = serviceMapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetAllServices()
        {
            var services = await _unitOfWork.ServiceRepository.GetAll();

            return services;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            var service = await _unitOfWork.ServiceRepository.GetById(id);

            if (service != null)
            {
                return Ok(service);
            }
            else
            {
                return NotFound("The service couldn't be found");
            }
        }

        [HttpPost]
        [Route("RegisterService")]
        public async Task<IActionResult> RegisterService(ServiceDTO serviceDTO)
        {
            var service = _serviceMapper.MapServiceDTOToService(serviceDTO);
            var serviceReturn = await _unitOfWork.ServiceRepository.Insert(service);

            if (serviceReturn != false)
            {
                await _unitOfWork.Complete();
                return Ok("The register operation was successful");
            }

            return BadRequest("The operation was canceled");
        }

        [HttpPut]
        [Route("UpdateService/{id}")]
        public async Task<IActionResult> UpdateService(Service service)
        {
            var serviceReturn = await _unitOfWork.ServiceRepository.Update(service);

            if (serviceReturn != false)
            {
                await _unitOfWork.Complete();
                return Ok("The update operation was successful");
            }

            return BadRequest("The operation was canceled");
        }

        [HttpPut]
        [Route("DeleteSoftService/{id}")]
        public async Task<IActionResult> DeleteSoftService(int id)
        {
            var serviceReturn = await _unitOfWork.ServiceRepository.DeleteSoftById(id);

            if (serviceReturn != false)
            {
                await _unitOfWork.Complete();
                return Ok("This service has been dropped down");
            }

            return NotFound("The service couldn't be found");
        }

        [HttpDelete]
        [Route("DeleteHardService/{id}")]
        public async Task<IActionResult> DeleteHardService(int id)
        {
            var service = await _unitOfWork.ServiceRepository.DeleteHardById(id);

            if (service != null)
            {
                await _unitOfWork.Complete();
                return Ok("This service has been eliminated from Database");
            }
            else
            {
                return NotFound("The service couldn't be found");
            }
        }
    }
}
