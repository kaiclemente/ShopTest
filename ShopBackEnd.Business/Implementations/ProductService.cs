using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopBackEnd.Model;
using ShopBackEnd.Repository;
using System.Transactions;

namespace ShopBackEnd.Business.Implementations
{
    public class ProductService:IProductService
    {
        private GenericRepository<Product> productRepository;
        private UnitOfWork _unitOfWork;

        public ProductService()
        {
            _unitOfWork = new UnitOfWork();
            productRepository = new GenericRepository<Product>(_unitOfWork.Context);
        }

        public Model.DTO.Product Get(int id)
        {
            var item = productRepository.FindById(id);
            return AutoMapper.Mapper.Map<Product, Model.DTO.Product>(item);
        }




        public Model.DTO.Product BestProduct()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Model.DTO.Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public Model.DTO.Product Add(Model.DTO.Product item)
        {
            var entity = AutoMapper.Mapper.Map<Model.DTO.Product, Product>(item);
            using (TransactionScope ctx = new TransactionScope())
            {
                try
                {
                    
                   productRepository.Insert(entity);
                   _unitOfWork.Save();
                   ctx.Complete();
                }
                catch (Exception)
                {
                    ctx.Dispose();
                    throw;
                }
            }
            return AutoMapper.Mapper.Map<Product, Model.DTO.Product>(entity);
        }

        public Model.DTO.Product Update(Model.DTO.Product item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
