using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;

namespace IntegratorSofttek.DataAccess.Repositories.Interfaces
{
    public interface IServiceRepository : IRepository<Service> // Update interface name and entity type
    {
        public Task<List<ServiceDTO>> GetAllServices(int parameter); // Update method name
        public Task<ServiceDTO> GetServiceById(int id, int parameter); // Update method name
        public Task<bool> DeleteServiceById(int id, int parameter); // Update method name
        public Task<bool> UpdateService(ServiceDTO service, int id, int parameter); // Update method name
        public Task<bool> InsertService(ServiceDTO serviceDTO);

    }
}
