//AOP - POSTSHARP
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MVC5Course.Controllers {
    public class 記錄Action的執行時間Attribute : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            //記錄開始時間
            base.OnActionExecuting(filterContext);
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext) {
            //記錄結束時間

            //計算執行時間
            TimeSpan executionTime = TimeSpan.FromHours(1);
            filterContext.Controller.ViewBag.執行時間 = executionTime;

            base.OnActionExecuted(filterContext);
        }

    }
}
