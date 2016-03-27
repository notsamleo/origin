using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mybloggywog.Models;

namespace mybloggywog.Controllers
{
    public class CommentController : Controller
    {
        //
        // GET: /Comment/

        public ActionResult AddComment(string name, string comment, int postId)
        {
            using (var dbContext = new blogEntities())
            {
                var ParentPost = (from p in dbContext.Posts
                                  where p.Id == postId
                                  select p).FirstOrDefault();

                ParentPost.Comments = new List<Comment>();

                Comment NewComment = new Comment();
                NewComment.Name = name;
                NewComment.Body = comment;
                NewComment.PostId = postId;
                NewComment.CreatedOn = DateTime.Now;

                ParentPost.Comments.Add(NewComment);
                dbContext.SaveChanges();
            }

            return RedirectToAction("Post", "Post", new { id = postId });
        }

    }
}
