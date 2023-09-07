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
    }
}
