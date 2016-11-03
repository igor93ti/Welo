using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Mvc;
using IExceptionFilter = System.Web.Mvc.IExceptionFilter;

namespace Welo.Bot.Filters
{
    public class RollbarExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            //if (filterContext.ExceptionHandled)
            //    return;

            //(new RollbarClient()).SendException(filterContext.Exception);
        }
    }
    public class RollbarWebApiExceptionFilter : IExceptionFilter, IFilter
    {
        //    RollbarClient client = new RollbarClient();

        public bool AllowMultiple
        {
            get { return false; }
        }

        public async Task ExecuteExceptionFilterAsync(HttpActionExecutedContext context, CancellationToken cancelToken)
        {
            //await Task.Factory.StartNew(() => client.SendException(context.Exception), cancelToken);
        }

        public void OnException(ExceptionContext filterContext)
        {
            //throw new System.NotImplementedException();
        }
    }
}