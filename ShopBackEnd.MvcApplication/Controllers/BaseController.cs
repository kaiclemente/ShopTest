using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ShopBackEnd.Business;
using ShopBackEnd.Common;
using ShopBackEnd.Model.DTO;

namespace ShopBackEnd.MvcApplication.Controllers
{
    public abstract class BaseController<T> : ApiController where T : Model.DTO.BaseDTO
    {
        private readonly IService<T> _service;
        protected ILogService _loggerService;

        protected BaseController(ILogService loggerService, IService<T> service)
        {
            _loggerService = loggerService;
            _service = service;
        }

        /// <summary>
        /// Get all items
        /// </summary>
        /// <returns>List of items</returns>
        public virtual IEnumerable<T> GetAll()
        {
            return _service.GetAll();
        }

        /// <summary>
        /// Get an item
        /// </summary>
        /// <param name="id">The id of item</param>
        /// <returns></returns>
        public virtual T Get(int id)
        {
            T item = _service.Get(id);
            if (item == null)
            {
                _loggerService.Logger().WarnFormat("Item with id {0} does not exist", id);
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        /// <summary>
        /// Create an new item
        /// </summary>
        /// <param name="item">The item to be created</param>
        /// <returns>The location of the created item</returns>
        public virtual HttpResponseMessage Post(T item)
        {
            item = _service.Add(item);

            var response = Request.CreateResponse<T>(HttpStatusCode.Created, item);

            string uri = Url.Link("ControllerAndId", new {id = item.ID});
            response.Headers.Location = new Uri(uri);

            _loggerService.Logger().InfoFormat("Item created with id {0}", item.ID);

            return response;
        }

        /// <summary>
        /// Updated an item
        /// </summary>
        /// <param name="id">Id of the item to update</param>
        /// <param name="item">The updated item</param>
        public virtual HttpResponseMessage Put(int id, T item)
        {
            item.ID = id;

            item = _service.Update(item);

            //if (!_service.Update(item))
            //{
            //    _loggerService.Logger().Error("Item not updated");
            //    throw new HttpResponseException(HttpStatusCode.NotFound);
            //}
            var response = Request.CreateResponse<T>(HttpStatusCode.OK, item);

            return response;
        }

        /// <summary>
        /// Deletes an item
        /// </summary>
        /// <param name="id">The id of the item to delete</param>
        /// <returns></returns>
        public virtual HttpResponseMessage Delete(int id)
        {
            _service.Remove(id);
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}