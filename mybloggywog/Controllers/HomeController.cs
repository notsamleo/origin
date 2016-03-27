using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mybloggywog.Models;
using System.Web.Mvc;

namespace mybloggywog.Controllers
{
    public class HomeController : Controller
    {

        public void DisplayPosts()
        {
            var dbContext = new blogEntities();
            
            var displayPosts = (from p in dbContext.Posts
                                orderby p.CreatedOn descending
                                select p).ToList();

            ViewBag.DisplayPosts = displayPosts;
        }

        public ActionResult Index()
        {
            DisplayPosts();
            return View();
        }

        public ActionResult Archive()
        {
            DisplayPosts();
            return View();
        }
        
    }
}
