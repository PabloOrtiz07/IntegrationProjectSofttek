using IntegratorSofttek.Entities;

namespace IntegratorSofttek.DataAccess.Repositories.Interfaces
{
    public interface IServiceRepository : IRepository<Service> // Update interface name and entity type
    {
        public Task<List<Service>> GetAllServices(int parameter); // Update method name
        public Task<Service> GetServiceById(int id, int parameter); // Update method name
        public Task<bool> DeleteServiceById(int id, int parameter); // Update method name
        public Task<bool> UpdateService(Service service, int id); // Update method name
    }
}
