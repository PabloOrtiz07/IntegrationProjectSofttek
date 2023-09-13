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
    [Authorize]

    public class WorksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WorksController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkDTO>>> GetAllWorks()
        {
            var works = await _unitOfWork.WorkRepository.GetAll();
            var worksDTO = _mapper.Map<List<WorkDTO>>(works);
            return Ok(worksDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkById([FromRoute] int id, int parameter)
        {
            var work = await _unitOfWork.WorkRepository.GetWorkById(id, parameter);

            if (work != null)
            {
                var workDTO = _mapper.Map<WorkDTO>(work);
                return Ok(workDTO);
            }
            else
            {
                return NotFound("The work couldn't be found");
            }
        }

        [HttpPost]
        [Route("RegisterWork")]
        public async Task<IActionResult> RegisterWork(WorkDTO workDTO)
        {
            var work = _mapper.Map<Work>(workDTO);
            var result = await _unitOfWork.WorkRepository.Insert(work);
            if (result != false)
            {
                await _unitOfWork.Complete();
                return Ok("The register operation was successful");
            }
            return BadRequest("The operation was canceled");
        }

        [HttpPut]
        [Route("UpdateWork/{id}")]
        public async Task<IActionResult> UpdateWork([FromRoute] int id, WorkDTO workDTO)
        {
            var work = _mapper.Map<Work>(workDTO);

            var result = await _unitOfWork.WorkRepository.Update(work, id);
            if (result != null)
            {
                await _unitOfWork.Complete();
                return Ok("The update operation was successful");
            }
            return BadRequest("The operation was canceled");
        }

        [HttpPut]
        [Route("DeleteWork/{id}")]
        public async Task<IActionResult> DeleteWork([FromRoute] int id, int parameter)
        {
            var workReturn = await _unitOfWork.WorkRepository.DeleteWorkById(id, parameter);
            if (workReturn != false)
            {
                await _unitOfWork.Complete();
                return Ok("This work has been dropped down");
            }
            return NotFound("The work couldn't be found");
        }
    }
}
