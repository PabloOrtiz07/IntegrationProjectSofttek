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

        public async Task<bool> UpdateService(ServiceDTO serviceDTO, int id, int parameter) // Update method name
        {
            try
            {

                var serviceFinding = await GetById(id);
                if (serviceFinding == null)
                {
                    return false;
                }
                if (parameter==0)
                {
                    var service = _mapper.Map<Service>(serviceDTO);
                    _mapper.Map(service, serviceFinding); 
                    _contextDB.Update(serviceFinding);
                    return true;
                }
                if(serviceFinding.IsDeleted !=false && parameter ==1) {
                    serviceFinding.IsDeleted = false;
                    serviceFinding.DeletedTimeUtc = null;
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
                Service serviceFinding = await base.GetById(id);
                if (serviceFinding == null)
                {
                    return null;
                }
                if (serviceFinding.IsDeleted != true && parameter == 0)
                {
                    return _mapper.Map<ServiceDTO>(serviceFinding);
                }
                if (parameter == 1)
                {
                    return _mapper.Map<ServiceDTO>(serviceFinding);
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public async Task<bool> DeleteServiceById(int id, int parameter)
        {

            try
            {
                Service serviceFinding = await GetById(id);
                if (serviceFinding == null)
                {
                    return false;
                }
                if (parameter == 0)
                {
                    serviceFinding.IsDeleted = true;
                    serviceFinding.DeletedTimeUtc = DateTime.UtcNow;
                    return true;
                }
                if (parameter == 1)
                {
                    var relatedWork = _contextDB.Works.Where(work => work.ServiceId == id).ToList();
                    _contextDB.Works.RemoveRange(relatedWork);
                    _contextDB.Services.Remove(serviceFinding);
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
            catch (Exception)
            {
                return false;
            }
      
        }
    }
}