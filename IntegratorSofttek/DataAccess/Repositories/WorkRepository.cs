using IntegratorSofttek.DataAccess.Repositories.Interfaces;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System.Linq;

namespace IntegratorSofttek.DataAccess.Repositories
{
    public class WorkRepository : Repository<Work>, IWorkRepository // Update class and interface names
    {
        public WorkRepository(ContextDB contextDB) : base(contextDB)
        {

        }

        public async Task<bool> UpdateWork(Work work, int id) // Update method name
        {
            try
            {
                var workFinding = await GetById(id); // Update variable name
                if (workFinding != null)
                {
                    _contextDB.Update(work);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual async Task<List<Work>> GetAllWorks(int parameter) // Update method name
        {
            try
            {
                var works = await base.GetAll(); // Update variable name
                if (parameter == 1)
                {
                    works = works.Where(work => work.IsDeleted == false).ToList();
                    return works;
                }
                return works;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Work> GetWorkById(int id, int parameter) // Update method name
        {
            try
            {
                Work work = await base.GetById(id); // Update variable name
                if (work.IsDeleted != true && parameter == 1)
                {
                    return work;
                }
                if (parameter == 2)
                {
                    return work;
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
            Work work = await GetById(id); // Update variable name
            if (work != null && parameter == 1)
            {
                work.IsDeleted = true;
                work.DeletedTimeUtc = DateTime.UtcNow;
                return true;
            }
            if (work != null && parameter == 2)
            {
                _contextDB.Works.Remove(work); // Update entity reference
                return true;
            }
            return false;
        }
    }
}
