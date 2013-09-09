using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBackEnd.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        ShopDBEntities _context { get; }
        //IDbSet<T> _dbset { get; }

        void commit();
        void RollBack();
    }
}
