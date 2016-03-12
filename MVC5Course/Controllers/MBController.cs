using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class MBController : Controller
    {
        // GET: MB
        public ActionResult Index()
        {
            return View();
        }
        //以下二者擇一
       //(強型別)
        [HttpPost]
        public ActionResult Index(string Name, DateTime Birthday) {
            //傳入的參數名稱要跟view的一樣
            return Content(Name + " " + Birthday);
        }
        //弱型別
        //[HttpPost]
        //public ActionResult Index(FormCollection form) {
        //    return Content(form["Name"] + " " + form["Birthday"]);
        //}



    }
}