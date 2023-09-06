using IntegratorSofttek.DataAccess;
using IntegratorSofttek.DataAccess.Repositories;

namespace IntegratorSofttek.Services
{
    public class UnitOfWorkService : IUnitOfWork
    {

        private readonly ContextDB _contextDB;

        public UserRepository oUserRepository { get; set; }
        
        public UnitOfWorkService(ContextDB contextDB)
        {
            _contextDB = contextDB;
            oUserRepository = new UserRepository(_contextDB);
        }
    }
}
