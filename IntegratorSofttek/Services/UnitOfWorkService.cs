using AutoMapper;
using IntegratorSofttek.DataAccess;
using IntegratorSofttek.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IntegratorSofttek.Services
{
    public class UnitOfWorkService : IUnitOfWork
    {

        private readonly ContextDB _contextDB;

        private readonly IMapper _mapper;


        public UserRepository UserRepository { get; set; }

        public ServiceRepository ServiceRepository { get; set; }

        public WorkRepository WorkRepository { get; set; }

        public ProjectRepository ProjectRepository { get; set; }

        public RoleRepository RoleRepository { get; set; }


        
        public UnitOfWorkService(ContextDB contextDB, IMapper mapper)
        {
            _mapper = mapper;
            _contextDB = contextDB;
            UserRepository = new UserRepository(_contextDB, _mapper);
            ServiceRepository = new ServiceRepository(_contextDB,_mapper);
            WorkRepository = new WorkRepository(_contextDB, _mapper);
            ProjectRepository = new ProjectRepository(_contextDB, _mapper);
            RoleRepository = new RoleRepository(_contextDB, _mapper) ;

        }

        public Task<int> Complete()
        {
            return _contextDB.SaveChangesAsync();
        }
    }
}
