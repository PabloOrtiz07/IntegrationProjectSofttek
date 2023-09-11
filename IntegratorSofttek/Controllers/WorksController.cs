using Microsoft.AspNetCore.Mvc;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
using IntegratorSofttek.Logic;
using IntegratorSofttek.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace IntegratorSofttek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly WorkMapper _workMapper;

        public WorkController(IUnitOfWork unitOfWork, WorkMapper workMapper)
        {
            _unitOfWork = unitOfWork;
            _workMapper = workMapper;
        }

        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<Work>>> GetAllWorks()
        {
            var works = await _unitOfWork.WorkRepository.GetAll();

            return works;
        }

        [HttpGet("{id}")]
        [Authorize]

        public async Task<IActionResult> GetWorkById(int id)
        {
            var work = await _unitOfWork.WorkRepository.GetById(id);

            if (work != null)
            {
                return Ok(work);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize]

        public async Task<IActionResult> InsertWork(WorkDTO workDTO)
        {
            var work = _workMapper.MapWorkDTOToWork(workDTO);
            var workReturn = await _unitOfWork.WorkRepository.Insert(work);

            if (workReturn != false)
            {
                return Ok("The insert operation was successful");
            }

            return BadRequest("The operation was canceled");
        }

        [HttpPut]
        [Authorize]

        public async Task<IActionResult> UpdateWork(WorkDTO workDTO, int id)
        {
            var work = _workMapper.MapWorkDTOToWork(workDTO);
            var workReturn = await _unitOfWork.WorkRepository.Update(work);

            if (workReturn != false)
            {
                return Ok("The update operation was successful");
            }

            return BadRequest("The operation was canceled");
        }

        [HttpDelete]
        [Authorize]

        public async Task<IActionResult> DeleteWork(int id)
        {
            var work = await _unitOfWork.WorkRepository.DeleteHardById(id);

            if (work != null)
            {
                
                return Ok("The work has been eliminated from Database");
            }

            return NotFound("The work couldn't be found");
        }
    }
}