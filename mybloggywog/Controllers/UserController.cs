using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.SqlClient;
using mybloggywog.Models;

namespace mybloggywog.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var dbContext = new blogEntities();

            var UserLoggingIn = (from u in dbContext.Users
                                 where u.Name == username
                                 && u.Password == password
                                 select u).FirstOrDefault();

            if (UserLoggingIn != null)
            {
                Session["username"] = UserLoggingIn.Name.ToString();
                Session["IsAdmin"] = (bool)UserLoggingIn.IsAdmin;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        public ActionResult Logout()
        {
            Session["username"] = null;
            Session["IsAdmin"] = null;
            return RedirectToAction("Index", "Home");
        }

    }
}
