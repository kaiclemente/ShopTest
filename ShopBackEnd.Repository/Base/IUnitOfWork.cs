using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBackEnd.Repository
{
    public interface IUnitOfWork 
    {
        void Dispose();
        void Dispose(bool disposing);
        IRepository<T> Repository<T>() where T : class, new();
        IDbContext Context { get; }
        void Save();
    }
}
