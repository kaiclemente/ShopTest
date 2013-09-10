//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Data.Entity;

//namespace ShopBackEnd.Repository.BaseRepository
//{
//    public class EFBaseRepository<T> : IRepository<T> where T: class, IEntity
//    {
        
//        private readonly ShopDBEntities _context;
//        private readonly IDbSet<T> _dbset;

//        protected ShopDBEntities context { get { return _context; } }
//        protected IDbSet<T> dbset { get { return _dbset; } }
        

//        protected EFBaseRepository()
//        {
//            _context = new ShopDBEntities();
//            _dbset = _context.Set<T>();
//        }

//        public virtual void Add(T entity)
//        {
//            _dbset.Add(entity);
//        }

//        public virtual void Update(T entity)
//        {
//            var entry = _context.Entry(entity);
//            _dbset.Attach(entity);
//            entry.State = System.Data.EntityState.Modified;
//        }

//        public virtual void Delete(T entity)
//        {
//            var entry = _context.Entry(entity);
//            entry.State = System.Data.EntityState.Deleted;
//            _dbset.Remove(entity);
//        }

//        public virtual void Delete(System.Linq.Expressions.Expression<Func<T, bool>> where)
//        {
//            IEnumerable<T> objects = _dbset.Where(where);
//            foreach (T item in objects)
//            {
//                var entry = _context.Entry(item);
//                entry.State = System.Data.EntityState.Deleted;
//                _dbset.Remove(item);
//            }
//        }

//        public virtual T GetById(object id)
//        {
//            T entity = _dbset.Find(id);
//            return entity;
//        }

//        public virtual IList<T> Get()
//        {
//            return _dbset.ToList();
//        }

//        public virtual int Count(System.Linq.Expressions.Expression<Func<T, bool>> where)
//        {
//            return _dbset.Where(where).Count();
//        }

//        public void Dispose()
//        {
//            //throw new NotImplementedException();
//        }
//    }
//}
