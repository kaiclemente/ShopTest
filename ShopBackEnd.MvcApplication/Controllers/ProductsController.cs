using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShopBackEnd.Model;
using ShopBackEnd.Business;

namespace ShopBackEnd.MvcApplication.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService service)
        {
            _productService = service;
        }

        [HttpGet]
        public Model.DTO.Product Get(int id)
        {
            Model.DTO.Product item = _productService.Get(id);
            if (item == null)
            {
                //_loggerService.Logger().WarnFormat("Item with id {0} does not exist", id);
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        [HttpGet]
        public IEnumerable<Model.DTO.Product> Get()
        {
            return _productService.GetAll();
        }

        [HttpPut]
        public HttpResponseMessage Update(int id, Model.DTO.Product item)
        {
            item.ID = id;
            item = _productService.Update(item);
            var response = Request.CreateResponse<Model.DTO.Product>(HttpStatusCode.OK, item);

            return response;
        }

        [HttpPost]
        public HttpResponseMessage Insert(Model.DTO.Product item)
        {
            item = _productService.Add(item);;

            var response = Request.CreateResponse<Model.DTO.Product>(HttpStatusCode.Created, item);

            string uri = Url.Link("ControllerAndId", new { id = item.ID });
            response.Headers.Location = new Uri(uri);

            //_loggerService.Logger().InfoFormat("Item created with id {0}", item.Id);

            return response;
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            _productService.Remove(id);
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }


    }
}
