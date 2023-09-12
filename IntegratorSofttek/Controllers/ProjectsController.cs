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
    public class ProjectsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetAllProjects()
        {
            var projects = await _unitOfWork.ProjectRepository.GetAll();
            var projectsDTO = _mapper.Map<List<ProjectDTO>>(projects);
            return Ok(projectsDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var project = await _unitOfWork.ProjectRepository.GetById(id);

            if (project != null)
            {
                var projectDTO = _mapper.Map<ProjectDTO>(project);
                return Ok(projectDTO);
            }
            else
            {
                return NotFound("The project couldn't be found");
            }
        }

        [HttpPost]
        [Route("RegisterProject")]
        public async Task<IActionResult> RegisterProject(ProjectDTO projectDTO)
        {
            var project = _mapper.Map<Project>(projectDTO);
            var result = await _unitOfWork.ProjectRepository.Insert(project);
            if (result != false)
            {
                await _unitOfWork.Complete();
                return Ok("The register operation was successful");
            }
            return BadRequest("The operation was canceled");
        }

        [HttpPut]
        [Route("UpdateProject/{id}")]
        public async Task<IActionResult> UpdateProject([FromRoute] int id, ProjectDTO projectDTO)
        {
            var project = _mapper.Map<Project>(projectDTO);

            var result = await _unitOfWork.ProjectRepository.Update(project, id);
            if (result != null)
            {
                await _unitOfWork.Complete();
                return Ok("The update operation was successful");
            }
            return BadRequest("The operation was canceled");
        }

        [HttpPut]
        [Route("DeleteSoftProject/{id}")]
        public async Task<IActionResult> DeleteSoftProject(int id)
        {
            var projectReturn = await _unitOfWork.ProjectRepository.DeleteSoftById(id);
            if (projectReturn != false)
            {
                await _unitOfWork.Complete();
                return Ok("This project has been dropped down");
            }
            return NotFound("The project couldn't be found");
        }

        [HttpDelete]
        [Route("DeleteHardProject/{id}")]
        public async Task<IActionResult> DeleteHardProject(int id)
        {
            var project = await _unitOfWork.ProjectRepository.DeleteHardById(id);

            if (project != null)
            {
                await _unitOfWork.Complete();
                return Ok("This project has been eliminated from the database");
            }
            else
            {
                return NotFound("The project couldn't be found");
            }
        }
    }
}
