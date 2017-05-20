using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {

        private libEntities db = new libEntities();
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(Models.member u)
        {


            var count = db.members.Where(x => x.firstname == u.firstname && x.password == u.password).Count();

            if (count ==0 )
            {

                ViewBag.Msg = "Invalid user";

                return View();
            }
            else
            {

                FormsAuthentication.SetAuthCookie(u.firstname, false);
                return RedirectToAction("Index", "Home");

            }

          
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("index", "categories");
        }


    }
}