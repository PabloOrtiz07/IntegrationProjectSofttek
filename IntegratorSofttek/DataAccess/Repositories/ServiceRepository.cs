using IntegratorSofttek.DataAccess.Repositories.Interfaces;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System.Linq;
using AutoMapper;

namespace IntegratorSofttek.DataAccess.Repositories
{
    public class ServiceRepository : Repository<Service>, IServiceRepository // Update class and interface names
    {

        private readonly IMapper _mapper;


        public ServiceRepository(ContextDB contextDB, IMapper mapper) : base(contextDB)
        {
            _mapper = mapper;

        }

        public async Task<bool> UpdateService(ServiceDTO serviceDTO, int id) // Update method name
        {
            try
            {
                var service= _mapper.Map<Service>(serviceDTO);

                var serviceFinding = await GetById(id);
                if (serviceFinding != null)
                {
                    _mapper.Map(service, serviceFinding); 

                    _contextDB.Update(serviceFinding);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual async Task<List<ServiceDTO>> GetAllServices(int parameter) // Update method name
        {
            try
            {
                var services = await base.GetAll();
                switch (parameter)
                {
                    case 0:
                        services=services.Where(service => service.IsDeleted!=true).ToList();
                        return _mapper.Map<List<ServiceDTO>>(services);
                    case 1:
                        return _mapper.Map<List<ServiceDTO>>(services);
                    case 2:
                        services=services.Where(service => service.IsDeleted!=true && service.IsActive).ToList();
                        return _mapper.Map<List<ServiceDTO>>(services); 
                    default:
                        return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ServiceDTO> GetServiceById(int id, int parameter)
        {
            try
            {
                Service service = await base.GetById(id);
                if (service.IsDeleted != true && parameter == 0)
                {
                    return _mapper.Map<ServiceDTO>(service);
                }
                if (parameter == 1)
                {
                    return _mapper.Map<ServiceDTO>(service);
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
            catch (Exception)
            {
                return false;

            }
        }
        public virtual async Task<bool> InsertService(ServiceDTO serviceDTO)
        {
            try
            {
                var service = _mapper.Map<Service>(serviceDTO);
                var response = await base.Insert(service);
                return response;
            }
            catch (Exception ex)
            {
                return false;
            }
      
        }
    }
}