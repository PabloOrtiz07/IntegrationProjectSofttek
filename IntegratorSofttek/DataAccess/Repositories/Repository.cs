using IntegratorSofttek.DataAccess.Repositories.Interfaces;
using IntegratorSofttek.Entities;
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
            var users = await _contextDB.Set<T>().ToListAsync();
            return users;
        }

        public virtual async Task<T> GetById(int id)
        {
            try
            {
                var user = await _contextDB.Set<T>().FindAsync(id);
                if (user != null)
                {
                    return user;
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
                await _contextDB.SaveChangesAsync();
                return true;
            }catch (Exception) {
                return false;
            }
          
        }

        public virtual async Task<bool> Delete(int id)
        {
            var user = await _contextDB.Set<T>().FindAsync(id);
            if(user != null)
            {
                _contextDB.Set<T>().Remove(user);
                await _contextDB.SaveChangesAsync();
                return true;
            }
       
            return false;
        }

        public async Task<bool> Update(T entity, int id)
        {
            try
            {
                var user = await _contextDB.Set<T>().FindAsync(id);
                if (user != null)
                {
                    _contextDB.Set<T>().Update(entity);
                    await _contextDB.SaveChangesAsync();
                    return true;
                }

                return false;
              
            }
            catch (Exception) {
                return false;
            }
        
        }






    }
}
