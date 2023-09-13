using IntegratorSofttek.Entities;

namespace IntegratorSofttek.DataAccess.Repositories.Interfaces
{
    public interface IWorkRepository : IRepository<Work> // Update interface name and entity type
    {
        public Task<List<Work>> GetAllWorks(int parameter); // Update method name
        public Task<Work> GetWorkById(int id, int parameter); // Update method name
        public Task<bool> DeleteWorkById(int id, int parameter); // Update method name
        public Task<bool> UpdateWork(Work work, int id); // Update method name
    }
}
