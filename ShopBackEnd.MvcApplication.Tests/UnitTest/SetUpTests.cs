using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ShopBackEnd.MvcApplication.DependencyContainer;

namespace ShopBackEnd.MvcApplication.Tests.UnitTest
{
    [SetUpFixture]
    public class SetUpTests
    {
        [SetUp]
        public void RunBeforeAnyTests()
        {
            ShopBackEnd.MvcApplication.DependencyContainer.DependencyResolverfactory.InitializeMapperProfiles();
        }

        [TearDown]
        public void RunAfterAnyTests()
        {

        }
    }
}
