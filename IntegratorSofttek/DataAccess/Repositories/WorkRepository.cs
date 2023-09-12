using IntegratorSofttek.DataAccess.Repositories.Interfaces;
using IntegratorSofttek.Entities;

namespace IntegratorSofttek.DataAccess.Repositories
{
    public class WorkRepository : Repository<Work>, IWorkRepository
    {
        public WorkRepository(ContextDB contextDB) : base(contextDB)
        {
        }
        public override async Task<bool> Update(Work work, int id)
        {
            try
            {
                var workFinding = await GetById(id);
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

        public override async Task<bool> DeleteSoftById(int id)
        {
            Work work  = await GetById(id);
            if (work != null)
            {
                work.IsDeleted = true;
                work.DeletedTimeUtc = DateTime.UtcNow;

                return true;
            }

            return false;
        }

    }
}
