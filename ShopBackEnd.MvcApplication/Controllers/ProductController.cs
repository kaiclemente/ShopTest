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
    public class ProductController : ApiController
    {
        private ProductService _productService;
        public ProductController()
        {
            _productService = new ProductService();
        }

        [HttpGet]
        public Model.DTO.Product Get(int id)
        {
            return _productService.Get(id);
        }
    }
}
