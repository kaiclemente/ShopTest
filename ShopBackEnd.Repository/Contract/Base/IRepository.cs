using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopBackEnd.Repository
{
    public interface IRepository<T> : IDisposable where T : IEntity
    {
        void Add (T entity);
        void Update(T entity);
        void Delete (T entity);
        void Delete(Expression<Func<T, Boolean>> where);
        T GetById(object id);
        IList<T> Get();
        int Count(Expression<Func<T, Boolean>> where);
    }
}
