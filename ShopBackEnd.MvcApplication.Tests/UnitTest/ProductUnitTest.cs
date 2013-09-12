using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;
using ShopBackEnd.Business;
using ShopBackEnd.Repository;
using ShopBackEnd.Model;

namespace ShopBackEnd.MvcApplication.Tests.UnitTest
{
    [TestFixture]
    public class TestProduct : BaseServiceTest
    {
        private Mock<IProductRepository> _mockProductRepo;

        private ProductService _service;

        private IList<Product> _allProducts;
        private Model.DTO.Product _newItem;

        [SetUp]
        public void SetUpTests()
        {
            _newItem = GetProduct();
            _allProducts = GetProducts();

            // Mock repository
            _mockProductRepo = new Mock<IProductRepository>();

            // Get
            _mockProductRepo.Setup(c => c.GetAll()).Returns(_allProducts);

            // GetById
            _mockProductRepo
                .Setup(mr => mr.FindById(It.IsAny<int>()))
                .Returns((int i) => _allProducts.Single(x => x.ID == i));

            _service = new ProductService(
                    CompositionService,
                    _mockProductRepo.Object
                );
        }

        [Test]
        public void Has_Best_Product()
        {
            
            _mockProductRepo.Setup(mr => mr.BestProduct()).Returns(_allProducts.First());

            var dto = _service.BestProduct();

            Assert.IsNotNull(dto);
            Assert.AreEqual(_allProducts.First().ID, dto.ID);
        }

        [Test]
        public void Get_Items_Count()
        {
            var result = _service.GetAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(_allProducts.Count(), result.Count());
        }

        [Test]
        public void GetById_Item_IsNotNull()
        {
            int id = 1;
            var result = _service.Get(id);

            Assert.IsNotNull(result);
        }

        [Test]
        public void Update_Item_Updated()
        {
            _mockProductRepo.Setup(mr => mr.Update(It.IsAny<Product>())).Verifiable();

            var item = _service.Get(1);
            item.Name = "SOME NEW NAME";
            item = _service.Update(item);

            _mockProductRepo.Verify();

            Assert.IsNotNull(item);
        }

        [Test]
        public void Add_Item_Added()
        {
            _mockProductRepo.Setup(mr => mr.Insert(It.IsAny<Product>())).Verifiable();

            var item = _service.Add(_newItem);

            _mockProductRepo.Verify();

            Assert.AreEqual(3, item.ID); // Verify it has the expected id
        }

        [Test]
        public void Delete_Item_Deleted()
        {
            _mockProductRepo.Setup(mr => mr.Delete(It.IsAny<Product>())).Verifiable();

            bool deleted = _service.Remove(1);

            _mockProductRepo.Verify(mr => mr.Delete(It.IsAny<Product>()), Times.Once());

            Assert.IsTrue(deleted);
        }


        #region Test data

        public IList<Product> GetProducts()
        {
            var products = Builder<Product>
                .CreateListOfSize(2)
                .Build();

            return products;
        }

        public Model.DTO.Product GetProduct()
        {
            var productDto = Builder<Model.DTO.Product>
                .CreateNew()
                    .With(x => x.ID = 3)
                .Build();

            return productDto;
        }

        #endregion Test data
    }
}
