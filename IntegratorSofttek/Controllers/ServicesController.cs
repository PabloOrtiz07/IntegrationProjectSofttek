using AutoMapper;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
using IntegratorSofttek.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
        private readonly IMapper _mapper;

        public ServicesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetAllServices()
        {
            var services = await _unitOfWork.ServiceRepository.GetAll();
            var servicesDTO = _mapper.Map<List<ServiceDTO>>(services);
            return Ok(servicesDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            var service = await _unitOfWork.ServiceRepository.GetById(id);

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
        public async Task<IActionResult> RegisterService(ServiceDTO serviceDTO)
        {
            var service = _mapper.Map<Service>(serviceDTO);
            var result = await _unitOfWork.ServiceRepository.Insert(service);
            if (result != false)
            {
                await _unitOfWork.Complete();
                return Ok("The register operation was successful");
            }
            return BadRequest("The operation was canceled");
        }

        [HttpPut]
        [Route("UpdateService/{id}")]
        public async Task<IActionResult> UpdateService([FromRoute] int id, ServiceDTO serviceDTO)
        {
            var service = _mapper.Map<Service>(serviceDTO);

            var result = await _unitOfWork.ServiceRepository.Update(service, id);
            if (result != null)
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
                return Ok("This service has been eliminated from the database");
            }
            else
            {
                return NotFound("The service couldn't be found");
            }
        }
    }
}
