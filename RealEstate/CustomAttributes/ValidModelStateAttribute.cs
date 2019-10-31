namespace RealEstate.CustomAttributes
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Newtonsoft.Json;

    public abstract class ModelStateTransfer : ActionFilterAttribute
    {
        protected const string Key = nameof(ModelStateTransfer);

        protected static void SendModelState(ControllerContext context)
            => context.Controller.TempData[Key] = context.Controller.ViewData.ModelState;

        protected static void LoadModelState(ControllerContext context)
            => context.Controller.ViewData.ModelState.Merge(context.Controller.TempData[Key] as ModelStateDictionary);

        protected static void RemoveModelState(ControllerContext context)
            => context.Controller.TempData[Key] = null;
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class PostModelStateAttribute : ModelStateTransfer
    {
        private string _getUrl;

        public PostModelStateAttribute(string getUrl = null)
        {
            _getUrl = getUrl;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.Controller.ViewData.ModelState.IsValid)
            {
                if (context.HttpContext.Request.IsAjaxRequest())
                    AjaxRequest(context);
                else
                    NormalRequest(context);
            }

            base.OnActionExecuting(context);
        }

        protected virtual void AjaxRequest(ActionExecutingContext context)
        {
            var errors = context.Controller.ViewData
                .ModelState.ToDictionary(
                    x => x.Key,
                    x => x.Value.Errors.Select(y => y.ErrorMessage));
            
            context.Result = new HttpStatusCodeResult(HttpStatusCode.BadRequest, JsonConvert.SerializeObject(errors));
        }

        protected virtual void NormalRequest(ActionExecutingContext context)
        {
            SendModelState(context);

            if (string.IsNullOrEmpty(_getUrl))
                context.Result = new RedirectToRouteResult(context.RouteData.Values);
            else
                context.Result = new RedirectResult(_getUrl);
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class GetModelStateAttribute : ModelStateTransfer
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ViewResult)
                LoadModelState(context);
            else
                RemoveModelState(context);

            base.OnActionExecuted(context);
        }
    }
}