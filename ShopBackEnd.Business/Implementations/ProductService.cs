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
        private readonly IProductRepository _productRepository;
        private readonly ICompositionService _compositionService;

        public ProductService(ICompositionService compositionService, IProductRepository productRepository)
        {
            _compositionService = compositionService;
            _productRepository = productRepository;
        }

        public Model.DTO.Product Get(int id)
        {
            var item = _productRepository.FindById(id);
            return _compositionService.Mapper.Map<Product, Model.DTO.Product>(item);
        }

        public Model.DTO.Product Add(Model.DTO.Product item)
        {
            var entity = _compositionService.Mapper.Map<Model.DTO.Product, Product>(item);
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    entity.State = ObjectState.Added;
                    _productRepository.Insert(entity);
                    _compositionService.UnitOfWork.Save();
                    scope.Complete();
                }
                catch (Exception)
                {
                    scope.Dispose();
                    throw;
                }
            }
            return _compositionService.Mapper.Map<Product, Model.DTO.Product>(entity);
        }

        public Model.DTO.Product BestProduct()
        {
            var item = _productRepository.BestProduct();
            return _compositionService.Mapper.Map<Product, Model.DTO.Product>(item);
        }

        public IEnumerable<Model.DTO.Product> GetAll()
        {
            var list = _productRepository.GetAll();
            return _compositionService.Mapper.Map<IEnumerable<Product>, IEnumerable<Model.DTO.Product>>(list);
        }

        public Model.DTO.Product Update(Model.DTO.Product item)
        {
            var existEntity = _productRepository.FindById(item.ID);
            var entity = _compositionService.Mapper.Map(item, existEntity);
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    entity.State = ObjectState.Modified;
                    _productRepository.Update(entity);
                    _compositionService.UnitOfWork.Save();
                    scope.Complete();
                }
                catch (Exception)
                {
                    scope.Dispose();
                    throw;
                }
            }

            return _compositionService.Mapper.Map<Product, Model.DTO.Product>(entity);
        }

        public bool Remove(int id)
        {
            var entity = _productRepository.FindById(id);
            if (entity == null)
                return false;

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    _productRepository.Delete(entity);
                    entity.State = ObjectState.Deleted;
                    _compositionService.UnitOfWork.Save();
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
