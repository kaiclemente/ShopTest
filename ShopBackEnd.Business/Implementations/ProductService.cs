using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopBackEnd.Model;
using ShopBackEnd.Repository;

namespace ShopBackEnd.Business.Implementations
{
    public class ProductService
    {
        private GenericRepository<Product> productRepository; 

        public ProductService()
        {
            var unitOfWork = new UnitOfWork();
            productRepository = new GenericRepository<Product>(unitOfWork.Context);
        }

        public Model.DTO.Product Get(int id)
        {
            var item = productRepository.FindById(id);
            return AutoMapper.Mapper.Map<Product, Model.DTO.Product>(item);
        }
    }
}
