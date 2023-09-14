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

        [HttpGet]
        [Authorize(Policy = "AdministratorAndConsultant")]

        public async Task<ActionResult<IEnumerable<ServiceDTO>>> GetAllServices([FromQuery] int parameter = 0)
        {
            var services = await _unitOfWork.ServiceRepository.GetAllServices(parameter); 
            var servicesDTO = _mapper.Map<List<ServiceDTO>>(services);
            return Ok(servicesDTO);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "AdministratorAndConsultant")]
        public async Task<IActionResult> GetServiceById([FromRoute] int id, [FromQuery] int parameter = 0)
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

        [HttpPut]
        [Route("DeleteService/{id}")]
        [Authorize(Policy = "Administrator")]

        public async Task<IActionResult> DeleteService([FromRoute] int id, [FromQuery] int parameter)
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
