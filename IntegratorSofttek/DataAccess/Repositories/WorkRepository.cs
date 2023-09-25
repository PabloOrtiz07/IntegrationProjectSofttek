using IntegratorSofttek.DataAccess.Repositories.Interfaces;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;

namespace IntegratorSofttek.DataAccess.Repositories
{
    public class WorkRepository : Repository<Work>, IWorkRepository // Update class and interface names
    {
        private readonly IMapper _mapper;

        public WorkRepository(ContextDB contextDB, IMapper mapper) : base(contextDB)
        {
            _mapper = mapper;
        }

        public async Task<bool> UpdateWork(WorkRegisterDTO workRegisterDTO, int id, int parameter)
        {
            try
            {

                var workFinding = await GetById(id);
                if (workFinding == null)
                {
                    return false;
                }
                if (parameter == 0)
                {
                    var work = _mapper.Map<Work>(workRegisterDTO);
                    _mapper.Map(work, workFinding);
                    _contextDB.Update(workFinding);
                    return true;
                }
                if (workFinding.IsDeleted !=false && parameter == 1)
                {
                    workFinding.IsDeleted = false;
                    workFinding.DeletedTimeUtc = null;
                    _contextDB.Update(workFinding);
                    return true;

                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual async Task<List<WorkDTO>> GetAllWorks(int parameter)
        {
            try
            {

                List<Work> works = await _contextDB.Works
                    .Include(work => work.Service)
                    .Include(work => work.Project)
                    .ToListAsync();

                switch (parameter)
                {
                    case 0:
                        works=works.Where(work => !work.IsDeleted).ToList();
                        return _mapper.Map<List<WorkDTO>>(works);
                    case 1:
                        return _mapper.Map<List<WorkDTO>>(works); ;
                    default:
                        return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<WorkDTO> GetWorkById(int id, int parameter)
        {
            try
            {
                Work workFinding = await _contextDB.Works
                    .Include(work => work.Service)
                    .Include(work => work.Project)
                    .Where(u => u.Id == id)
                    .FirstOrDefaultAsync();

                if (workFinding == null)
                {
                    return null;
                }

                if (workFinding.IsDeleted != true && parameter == 0)
                {
                   return _mapper.Map<WorkDTO>(workFinding);
                }
                if (parameter == 1)
                {
                    return _mapper.Map<WorkDTO>(workFinding);
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteWorkById(int id, int parameter)
        {
            try
            {
                Work workFinding = await GetById(id);
                if (workFinding == null)
                {
                    return false;
                }
                if (parameter == 0)
                {
                    workFinding.IsDeleted = true;
                    workFinding.DeletedTimeUtc = DateTime.UtcNow;
                    return true;
                }
                if (parameter == 1)
                {
                    _contextDB.Works.Remove(workFinding);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
  
        }

        public virtual async Task<bool> InsertWork(WorkRegisterDTO workRegisterDTO)
        {
            try
            {
                var work = _mapper.Map<Work>(workRegisterDTO);
                var response = await base.Insert(work);
                return response;
            }
            catch (Exception)
            {
                return false;
            }
    
        }
    }
}
