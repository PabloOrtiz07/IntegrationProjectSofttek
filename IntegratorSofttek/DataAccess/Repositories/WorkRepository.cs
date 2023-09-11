using IntegratorSofttek.DataAccess.Repositories.Interfaces;
using IntegratorSofttek.Entities;

namespace IntegratorSofttek.DataAccess.Repositories
{
    public class WorkRepository : Repository<Work>, IWorkRepository
    {
        public WorkRepository(ContextDB contextDB) : base(contextDB)
        {
        }
        public virtual async Task<bool> Update(Work updatedWork)
        {
            try
            {
                var workFinding = await GetById(updatedWork.Id);

                if (workFinding != null)
                {
                    _contextDB.Update(updatedWork); 
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
              
                return false;
            }
        }

    }
}
