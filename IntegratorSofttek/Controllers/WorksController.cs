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
    public class WorksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly WorkMapper _workMapper;

        public WorksController(IUnitOfWork unitOfWork, WorkMapper workMapper)
        {
            _unitOfWork = unitOfWork;
            _workMapper = workMapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Work>>> GetAllWorks()
        {
            var works = await _unitOfWork.WorkRepository.GetAll();

            return works;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkById(int id)
        {
            var work = await _unitOfWork.WorkRepository.GetById(id);

            if (work != null)
            {
                return Ok(work);
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
            var work = _workMapper.MapWorkDTOToWork(workDTO);
            var workReturn = await _unitOfWork.WorkRepository.Insert(work);

            if (workReturn != false)
            {
                await _unitOfWork.Complete();
                return Ok("The register operation was successful");
            }

            return BadRequest("The operation was canceled");
        }

        [HttpPut]
        [Route("UpdateWork/{id}")]
        public async Task<IActionResult> UpdateWork(Work work)
        {
            var workReturn = await _unitOfWork.WorkRepository.Update(work);

            if (workReturn != false)
            {
                await _unitOfWork.Complete();
                return Ok("The update operation was successful");
            }

            return BadRequest("The operation was canceled");
        }

        [HttpPut]
        [Route("DeleteSoftWork/{id}")]
        public async Task<IActionResult> DeleteSoftWork(int id)
        {
            var workReturn = await _unitOfWork.WorkRepository.DeleteSoftById(id);

            if (workReturn != false)
            {
                await _unitOfWork.Complete();
                return Ok("This work has been dropped down");
            }

            return NotFound("The work couldn't be found");
        }

        [HttpDelete]
        [Route("DeleteHardWork/{id}")]
        public async Task<IActionResult> DeleteHardWork(int id)
        {
            var work = await _unitOfWork.WorkRepository.DeleteHardById(id);

            if (work != null)
            {
                await _unitOfWork.Complete();
                return Ok("This work has been eliminated from Database");
            }
            else
            {
                return NotFound("The work couldn't be found");
            }
        }
    }
}
