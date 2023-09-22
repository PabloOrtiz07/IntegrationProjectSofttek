using IntegratorSofttek.DataAccess.Repositories.Interfaces;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System.Linq;
using AutoMapper;

namespace IntegratorSofttek.DataAccess.Repositories
{
    public class WorkRepository : Repository<Work>, IWorkRepository // Update class and interface names
    {
        private readonly IMapper _mapper;

        public WorkRepository(ContextDB contextDB, IMapper mapper) : base(contextDB)
        {
            _mapper = mapper;
        }

        public async Task<bool> UpdateWork(WorkDTO workDTO, int id, int parameter) // Update method name
        {
            try
            {

                var workFinding = await GetById(id); // Update variable name
                if (workFinding != null && parameter == 0)
                {
                    var work = _mapper.Map<Work>(workDTO);
                    _mapper.Map(work, workFinding);
                    _contextDB.Update(workFinding);
                    return true;
                }
                if (workFinding != null && workFinding.IsDeleted !=false && parameter == 1)
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

        public virtual async Task<List<WorkDTO>> GetAllWorks(int parameter) // Update method name
        {
            try
            {

                var works = await base.GetAll(); // Update variable name
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

        public async Task<WorkDTO> GetWorkById(int id, int parameter) // Update method name
        {
            try
            {
                Work work = await base.GetById(id); // Update variable name
                if (work.IsDeleted != true && parameter == 0)
                {
                   return _mapper.Map<WorkDTO>(work);
                }
                if (parameter == 1)
                {
                    return _mapper.Map<WorkDTO>(work);
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteWorkById(int id, int parameter) // Update method name
        {
            try
            {
                Work work = await GetById(id); // Update variable name
                if (work != null && parameter == 0)
                {
                    work.IsDeleted = true;
                    work.DeletedTimeUtc = DateTime.UtcNow;
                    return true;
                }
                if (work != null && parameter == 1)
                {
                    _contextDB.Works.Remove(work); // Update entity reference
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
  
        }

        public virtual async Task<bool> InsertWork(WorkDTO workDTO)
        {
            try
            {
                var work = _mapper.Map<Work>(workDTO);
                var response = await base.Insert(work);
                return response;
            }
            catch (Exception ex)
            {
                return false;
            }
    
        }
    }
}
