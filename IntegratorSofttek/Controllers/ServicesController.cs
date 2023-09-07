using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
using IntegratorSofttek.Logic;
using IntegratorSofttek.Services;
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
                return NotFound();
            }
        }

        [HttpPost("insert")]
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

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateService(ServiceDTO serviceDTO, int id)
        {
            var service = _serviceMapper.MapServiceDTOToService(serviceDTO);
            var serviceReturn = await _unitOfWork.ServiceRepository.Update(service, id);

            if (serviceReturn != false)
            {
                return Ok("The update operation was successful");
            }

            return BadRequest("The operation was canceled");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _unitOfWork.ServiceRepository.GetById(id);

            if (service != null)
            {
                await _unitOfWork.ServiceRepository.Delete(id);
                return Ok("The service has been eliminated");
            }

            return NotFound("The service couldn't be found");
        }
    }
}
