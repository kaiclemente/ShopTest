using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopBackEnd.Model;
using ShopBackEnd.Repository;
using System.Transactions;

namespace ShopBackEnd.Business
{
    public class ProductService:IProductService
    {
        private GenericRepository<Product> productRepository;
        private IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            productRepository = new GenericRepository<Product>(_unitOfWork.Context);
        }

        public Model.DTO.Product Get(int id)
        {
            var item = productRepository.FindById(id);
            return AutoMapper.Mapper.Map<Product, Model.DTO.Product>(item);
        }

        public Model.DTO.Product Add(Model.DTO.Product item)
        {
            var entity = AutoMapper.Mapper.Map<Model.DTO.Product, Product>(item);
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    entity.State = ObjectState.Added;
                    productRepository.Insert(entity);
                    _unitOfWork.Save();
                    scope.Complete();
                }
                catch (Exception)
                {
                    scope.Dispose();
                    throw;
                }
            }
            return AutoMapper.Mapper.Map<Product, Model.DTO.Product>(entity);
        }

        public Model.DTO.Product BestProduct()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Model.DTO.Product> GetAll()
        {
            var list = productRepository.GetAll();
            return AutoMapper.Mapper.Map<IEnumerable<Product>, IEnumerable<Model.DTO.Product>>(list);
        }

        public Model.DTO.Product Update(Model.DTO.Product item)
        {
            var existEntity = productRepository.FindById(item.ID);
            var entity = AutoMapper.Mapper.Map(item, existEntity);
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    entity.State = ObjectState.Modified;
                    productRepository.Update(entity);
                    _unitOfWork.Save();
                    scope.Complete();
                }
                catch (Exception)
                {
                    scope.Dispose();
                    throw;
                }
            }

            return AutoMapper.Mapper.Map<Product, Model.DTO.Product>(entity);
        }

        public bool Remove(int id)
        {
            var entity = productRepository.FindById(id);
            if (entity == null)
                return false;

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    productRepository.Delete(entity);
                    entity.State = ObjectState.Deleted;
                    _unitOfWork.Save();
                    scope.Complete();
                }
                catch (Exception)
                {
                    scope.Dispose();
                    throw;
                }
            }

            return true;
        }
    }
}
