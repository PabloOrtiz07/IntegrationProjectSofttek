using IntegratorSofttek.DataAccess.Repositories.Interfaces;
using IntegratorSofttek.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntegratorSofttek.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {

        protected readonly ContextDB _contextDB;

        public Repository(ContextDB contextDB)
        {
            _contextDB = contextDB;
        }

        public virtual async Task<List<T>> GetAll()
        {
            var entity = await _contextDB.Set<T>().ToListAsync();
            return entity;
        }



        public virtual async Task<T> GetById(int id)
        {
            try
            {
                var entity = await _contextDB.Set<T>().FindAsync(id);
                if (entity != null)
                {
                    return entity;
                }

                return null;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual async Task<bool> Insert(T entity)
        {
            try
            {
                _contextDB.Set<T>().Add(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }



        public virtual async Task<bool> DeleteHardById(int id)
        {
            var entity = await GetById(id);
            if (entity != null)
            {
                _contextDB.Set<T>().Remove(entity);
                return true;
            }

            return false;
        }

        public virtual async Task<bool> DeleteSoftById(int id)
        {
            throw new NotImplementedException();

        }

        public virtual async Task<bool> Update(T entity)
        {
            throw new NotImplementedException();


        }

    }
}