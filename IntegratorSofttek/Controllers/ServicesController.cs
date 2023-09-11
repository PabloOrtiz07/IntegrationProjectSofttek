using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
using IntegratorSofttek.Logic;
using IntegratorSofttek.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IntegratorSofttek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ServiceMapper _serviceMapper;

        public ServiceController(IUnitOfWork unitOfWork, ServiceMapper serviceMapper)
        {
            _unitOfWork = unitOfWork;
            _serviceMapper = serviceMapper;
        }

        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<Service>>> GetAllServices()
        {
            var services = await _unitOfWork.ServiceRepository.GetAll();

            return services;
        }

        [HttpGet("{id}")]
        [Authorize]

        public async Task<IActionResult> GetServiceById(int id)
        {
            var service = await _unitOfWork.ServiceRepository.GetById(id);

            if (service != null)
            {
                return Ok(service);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize]

        public async Task<IActionResult> InsertService(ServiceDTO serviceDTO)
        {
            var service = _serviceMapper.MapServiceDTOToService(serviceDTO);
            var serviceReturn = await _unitOfWork.ServiceRepository.Insert(service);

            if (serviceReturn != false)
            {
                return Ok("The insert operation was successful");
            }

            return BadRequest("The operation was canceled");
        }

        [HttpPut]
        [Authorize]

        public async Task<IActionResult> UpdateService(ServiceDTO serviceDTO, int id)
        {
            var service = _serviceMapper.MapServiceDTOToService(serviceDTO);
            var serviceReturn = await _unitOfWork.ServiceRepository.Update(service);

            if (serviceReturn != false)
            {
                return Ok("The update operation was successful");
            }

            return BadRequest("The operation was canceled");
        }

        [HttpDelete]
        [Authorize]

        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _unitOfWork.ServiceRepository.DeleteHardById(id);

            if (service != null)
            {
                return Ok("The service has been eliminated from DataBase");
            }

            return NotFound("The service couldn't be found");
        }
    }
}
