using Autofac.Integration.WebApi;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using ShopBackEnd.Common;

namespace ShopBackEnd.MvcApplication.Filters
{
    public class UnhandledExceptionFilter : IAutofacExceptionFilter
    {
        readonly ILogService _logService;

        public UnhandledExceptionFilter(ILogService logService)
        {
            _logService = logService;
        }

        public void OnException(System.Web.Http.Filters.HttpActionExecutedContext actionExecutedContext)
        {
            string actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
            _logService.Logger().ErrorFormat("Action: {0} failed. Exception: {1}", actionName, actionExecutedContext.Exception);

            bool includeErrorDetail = false;

            #if DEBUG
            includeErrorDetail = true;
            #endif

            HttpError error = new HttpError(actionExecutedContext.Exception, includeErrorDetail);

            // TODO: check for exception types and return correct HttpStatusCode
            actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, error);
        }
    }
}