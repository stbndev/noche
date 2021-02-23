using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using noche.Helpers;

namespace noche.Filters
{
    public class ExceptionManager : IExceptionFilter
    {
        private readonly IWebHostEnvironment _hostingEnviroment;
        private readonly IModelMetadataProvider _modelMetaProvider;

        public ExceptionManager(IWebHostEnvironment hostingEnviroment, IModelMetadataProvider modelMetaProvider)
        {
            this._hostingEnviroment = hostingEnviroment;
            this._modelMetaProvider = modelMetaProvider;
        }

        public void OnException(ExceptionContext context)
        {
            //if (context.Exception is MyException)
            //{
                context.Result = new JsonResult(String.Format("Error Service {0} Path {1}  - Details {2} {3} {4}", _hostingEnviroment.ApplicationName, _hostingEnviroment.WebRootPath, context.Exception.GetType(), context.Exception.Message, context.Exception.InnerException));
            //}
        }
    }
}
