using IntegratorSofttek.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IntegratorSofttek.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {

        protected readonly ContextDB _contextDB;

        public Repository(ContextDB contextDB) {
            _contextDB = contextDB;
        }

        public virtual async Task<List<T>> GetAll()
        {
            var test = await _contextDB.Set<T>().ToListAsync();
            return test;
        }

    }
}
