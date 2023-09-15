using IntegratorSofttek.DataAccess.Repositories.Interfaces;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System.Linq;

namespace IntegratorSofttek.DataAccess.Repositories
{
    public class ServiceRepository : Repository<Service>, IServiceRepository // Update class and interface names
    {
        public ServiceRepository(ContextDB contextDB) : base(contextDB)
        {

        }

        public async Task<bool> UpdateService(Service service, int id) // Update method name
        {
            try
            {
                var serviceFinding = await GetById(id); // Update variable name
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

        public virtual async Task<List<Service>> GetAllServices(int parameter) // Update method name
        {
            try
            {
                var services = await base.GetAll();
                switch (parameter)
                {
                    case 0:
                        return services.Where(service => !service.IsDeleted).ToList();
                    case 1:
                        return services;
                    case 2:
                        return services.Where(service => !service.IsDeleted && service.IsActive).ToList();
                    default:
                        return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Service> GetServiceById(int id, int parameter)
        {
            try
            {
                Service service = await base.GetById(id);
                if (service.IsDeleted != true && parameter == 0)
                {
                    return service;
                }
                if (parameter == 1)
                {
                    return service;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public async Task<bool> DeleteServiceById(int id, int parameter) // Update method name
        {

            try
            {
                Service service = await GetById(id); // Update variable name
                if (service != null && parameter == 0)
                {
                    service.IsDeleted = true;
                    service.DeletedTimeUtc = DateTime.UtcNow;
                    return true;
                }
                if (service != null && parameter == 1)
                {
                    _contextDB.Services.Remove(service); // Update entity reference
                    return true;
                }
                return false;
            }
            catch (Exception){
                return false;

            }

        }
    }
}
