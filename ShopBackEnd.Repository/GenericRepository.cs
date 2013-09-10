using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBackEnd.Repository
{
    
    /// <summary>
    /// Extending the IRepository<Product>
    /// </summary>
    public class GenericRepository<T> : Repository<T> where T : class
    {
        //private readonly IRepository<Product> _productRepository;
        //private readonly IUnitOfWork _unitOfWork;

        public GenericRepository(IDbContext context)
            : base(context)
        {
            //_unitOfWork = unitOfWork;
            //_productRepository = productRepository;
        }

        //public virtual T Get(int id)
        //{
        //    return _unitOfWork.Repository<T>().FindById(id);
        //}

        //public virtual void Add(T entity)
        //{
        //    _unitOfWork.Repository<T>().Insert(entity);
        //    _unitOfWork.Save();
        //}

        //public virtual void Update(T entity)
        //{
        //    _unitOfWork.Repository<T>().Update(entity);
        //    _unitOfWork.Save();
        //}

        //public virtual void Delete(T entity)
        //{
        //    _unitOfWork.Repository<T>().Delete(entity);
        //    _unitOfWork.Save();
        //}



        //public static decimal GetCustomerOrderTotalByYear(
        //    this IRepository<Product> productRepository,
        //    int customerId)
        //{
        //    return productRepository
        //        .FindById(customerId)
        //        .Orders.SelectMany(o => o.OrderDetails)
        //        .Select(o => o.Quantity * o.UnitPrice).Sum();
        //}

        ///// <summary>
        ///// TODO:
        ///// This should really live in the Services project (Business Layer), 
        ///// however, we'll leave it here for now as an example, and migrate
        ///// this in the next post.
        ///// </summary>
        //public static void AddCustomerWithAddressValidation(
        //    this IRepository<Customer> customerRepository, Customer customer)
        //{
        //    USPSManager m = new USPSManager("YOUR_USER_ID", true);
        //    Address a = new Address();
        //    a.Address1 = customer.Address;
        //    a.City = customer.City;

        //    Address validatedAddress = m.ValidateAddress(a);

        //    if (validatedAddress != null)
        //        customerRepository.InsertGraph(customer);
        //}
    }
}
