using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using ShopBackEnd.Business;
using ShopBackEnd.Repository;
using AutoMapper;

namespace ShopBackEnd.MvcApplication.Tests.UnitTest
{
    [TestFixture]
    public abstract class BaseServiceTest
    {
        
        protected Mock<IUnitOfWork> MockUnitOfWork { get; set; }
        protected ICompositionService CompositionService { get; set; }


        protected BaseServiceTest()
        {
            // Mock unit of work
            MockUnitOfWork = new Mock<IUnitOfWork>();

            CompositionService = new CompositionService(MockUnitOfWork.Object, Mapper.Engine);

        }

        [SetUp]
        public void BaseSetUp()
        {

        }

        [TearDown]
        public void BaseTearDown()
        {
        }
    }
}
