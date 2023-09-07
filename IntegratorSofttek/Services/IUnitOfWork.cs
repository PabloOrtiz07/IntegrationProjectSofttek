using IntegratorSofttek.DataAccess.Repositories;

namespace IntegratorSofttek.Services
{
    public interface IUnitOfWork
    {
        public UserRepository UserRepository { get; }
        public ServiceRepository ServiceRepository { get; }

        public ProjectRepository ProjectRepository { get; }

        public WorkRepository WorkRepository { get; }
    }
}
