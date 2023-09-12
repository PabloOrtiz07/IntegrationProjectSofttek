using IntegratorSofttek.DataAccess.Repositories.Interfaces;
using IntegratorSofttek.DataAccess.Repositories;
using IntegratorSofttek.DataAccess;
using IntegratorSofttek.Entities;

namespace IntegratorSofttek.DataAccess.Repositories
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        public ServiceRepository(ContextDB contextDB) : base(contextDB)
        {

        }
        public override async Task<bool> Update(Service service, int id)
        {
            try
            {
                var serviceFinding = await GetById(id);
                if (serviceFinding != null)
                {
                    _contextDB.Update(service);
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
            Service service = await GetById(id);
            if (service != null)
            {
                service.IsDeleted = true;
                service.DeletedTimeUtc = DateTime.UtcNow;

                return true;
            }

            return false;
        }

    }
}
