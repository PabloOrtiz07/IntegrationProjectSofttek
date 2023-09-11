using IntegratorSofttek.DataAccess;
using IntegratorSofttek.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IntegratorSofttek.Services
{
    public class UnitOfWorkService : IUnitOfWork
    {

        private readonly ContextDB _contextDB;

        public UserRepository UserRepository { get; set; }

        public ServiceRepository ServiceRepository { get; set; }

        public WorkRepository WorkRepository { get; set; }

        public ProjectRepository ProjectRepository { get; set; }


        public UnitOfWorkService(ContextDB contextDB)
        {
            _contextDB = contextDB;
            UserRepository = new UserRepository(_contextDB);
            ServiceRepository = new ServiceRepository(_contextDB);
            WorkRepository = new WorkRepository(_contextDB);
            ProjectRepository = new ProjectRepository(_contextDB);

        }

        public Task<int> Complete()
        {
            return _contextDB.SaveChangesAsync();
        }
    }
}
