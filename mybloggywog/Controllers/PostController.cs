using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mybloggywog.Models;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace mybloggywog.Controllers
{
    public class PostController : Controller
    {
        //
        // GET: /Post/

        public ActionResult Post(int id)
        {
            var dbContext = new blogEntities();

            var postToDisplay = (from p in dbContext.Posts
                                 where p.Id == id
                                 select p);

            ViewBag.SinglePost = postToDisplay;

            return View();
        }

        public ActionResult CreatePost()
        {
            return View();
        }


        //Takes values and parses to database
        //Returns to index upon completion
        [HttpPost]
        public ActionResult CreatePost(Post postFromForm, string tags)
        {
            postFromForm.CreatedOn = DateTime.Now;

            postFromForm.Tags = new List<Tag>();

            string[] tagList = tags.Split(' ');
            using (var dbContext = new blogEntities())
            {
                foreach (var tag in tagList)
                {
                    var TagToCompare = (from t in dbContext.Tags
                                        where t.Name == tag
                                        select t).FirstOrDefault();

                    if (TagToCompare != null)
                    {
                        postFromForm.Tags.Add(TagToCompare);
                    }
                    else
                    {
                        Tag NewTag = new Tag();
                        NewTag.Name = tag;
                        postFromForm.Tags.Add(NewTag);
                    }
                }

                dbContext.Posts.Add(postFromForm);
                dbContext.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Delete(int id)
        {
            using (var dbContext = new blogEntities())
            {
                //Identify single post by id
                var PostToDelete = (from p in dbContext.Posts
                                    where p.Id == id
                                    select p).FirstOrDefault();

                //And casts it into the fires of Mt. Doom
                dbContext.Posts.Remove(PostToDelete);

                dbContext.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }


        //Fetches row from database by id and passes values back to form
        //in the view
        public ActionResult Edit(int id)
        {
            var dbContext = new blogEntities();

            var postToEdit = (from p in dbContext.Posts
                              where p.Id == id
                              select p).FirstOrDefault();

            return View(postToEdit);
        }



        //Updates row by id with form input values and saves
        //changes to database. Returns to Index
        [HttpPost]
        public ActionResult Edit(int id, string title, string body)
        {
            using (var dbContext = new blogEntities())
            {
                var postToEdit = (from p in dbContext.Posts
                                  where p.Id == id
                                  select p).FirstOrDefault();

                postToEdit.Title = title;
                postToEdit.Body = body;
                postToEdit.EditedOn = DateTime.Now;

                dbContext.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

   }
}
