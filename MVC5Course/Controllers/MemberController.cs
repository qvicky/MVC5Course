/*修改 web.config 
 * <authentication mode="Forms"> 修改mode
 * <remove name="FormsAuthentication" /> 註解
 */
using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC5Course.Controllers
{
    public class MemberController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel login) {
            if (CheckLogin(login.Email, login.Password)) {
                FormsAuthentication.RedirectFromLoginPage(login.Email, false);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("Password", "您輸入的帳號或密碼是錯誤的");
            return View();
        }

        private bool CheckLogin(string username, string password) {
            //Vicky:可以讀取db來驗證人員
            return (
                username == "vicky@mail.com" && 
                password == "2222");
        }
        public ActionResult Logout() {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}