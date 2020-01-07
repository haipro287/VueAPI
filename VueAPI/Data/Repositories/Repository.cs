using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        internal SchoolContext _context;
        internal DbSet<T> _dbSet;

        public Repository()
        {
        }

        public Repository(SchoolContext context)
        {
            this._context = context;
            this._dbSet = context.Set<T>();
        }

        public virtual DbSet<T> GetAll()
        {
            return _dbSet;
        }

        public virtual async Task<T> GetByID(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task Insert(T t)
        {
            await _dbSet.AddAsync(t);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Delete(object id)
        {
            T entityToDelete = await _dbSet.FindAsync(id);
            await Delete(entityToDelete);
        }

        public virtual async Task Delete(T t)
        {
            if (_context.Entry(t).State == EntityState.Detached)
            {
                _dbSet.Attach(t);
            }
            _dbSet.Remove(t);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Update(T t)
        {
            _dbSet.Attach(t);
            _context.Entry(t).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

}
