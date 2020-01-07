using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data.Repositories
{
    interface IRepository<T> where T: class
    {
        DbSet<T> GetAll();
        Task<T> GetByID(object id);
        Task Insert(T t);
        Task Delete(object id);
        Task Update(T t);
    }
}
