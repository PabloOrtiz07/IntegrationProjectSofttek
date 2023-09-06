using IntegratorSofttek.DataAccess.Repositories;

namespace IntegratorSofttek.Services
{
    public interface IUnitOfWork
    {
        public UserRepository oUserRepository { get; }
    }
}
