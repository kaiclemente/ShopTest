using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShopBackEnd.Business.Implementations;
using ShopBackEnd.Model;

namespace ShopBackEnd.MvcApplication.Controllers
{
    public class ProductsController : ApiController
    {
        private ProductService _productService;
        public ProductsController()
        {
            _productService = new ProductService();
        }

        [HttpGet]
        public Model.DTO.Product Get(int id)
        {
            return _productService.Get(id);
        }

        [HttpPost]
        public Model.DTO.Product Insert(Model.DTO.Product product)
        {
            return _productService.Add(product);
        }
    }
}
