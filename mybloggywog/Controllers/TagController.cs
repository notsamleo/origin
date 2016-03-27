using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mybloggywog.Models;

namespace mybloggywog.Controllers
{
    public class TagController : Controller
    {
        //
        // GET: /Tag/

        public ActionResult Tag(int id)
        {
            var dbContext = new blogEntities();

            var TagsToReturn = (from p in dbContext.Tags
                                where p.Id == id
                                select p);

            ViewBag.Tags = TagsToReturn;

            return View();
        }

        public ActionResult TagSearch(string TagSearchString)
        {
            using (var dbContext = new blogEntities())
            {
                var TagToQuery = (from t in dbContext.Tags
                                  where t.Name == TagSearchString
                                  select t).FirstOrDefault();

                if (TagToQuery != null)
                {
                    return RedirectToAction("Tag", new { id = TagToQuery.Id });
                }
                else
                {
                    //TODO: Don't just return home
                    return RedirectToAction("Index", "Home");
                }
            }
        }

    }
}
