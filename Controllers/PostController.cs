using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using VSTForum.Models;
using WebMatrix.WebData;
using System.Security.Claims;

namespace VSTForum.Controllers
{
    public class PostController : Controller
    {
        private ForumContext context;
        public PostController(ForumContext ctx)
        {
            context = ctx;
        }

        [HttpGet]
        public IActionResult Add()
        {
            if (User.Identity.IsAuthenticated) {
                ViewBag.Categories = context.Categories.OrderBy(g => g.CategoryName).ToList();
                return View(new Post());
            }
            else
            {
                return RedirectToAction("Login", "Account", null);
            }
        }

        [HttpPost]
        public IActionResult Add(Post post)
        {
            if (ModelState.IsValid)
            {
                post.DateCreated = DateTime.Now;
                IQueryable<Post> postquery = context.Posts;
                IQueryable<User> query = context.Users;
                User user = query.FirstOrDefault(u => u.UserName.ToLower() == User.Identity.Name.ToLower());
                post.UserId = user.Id;
                Post lastpost = postquery.OrderByDescending(p => p.PostId).First();
                int lastid = Int32.Parse(lastpost.PostId) + 1;
                post.PostId = lastid.ToString();
                context.Posts.Add(post);
                context.SaveChanges();
                return RedirectToAction("ViewPost", "Home", new { id = post.PostId });
            }
            else
            {
                ViewBag.Categories = context.Categories.OrderBy(g => g.CategoryName).ToList();
                return View(post);
            }
        }
        [HttpGet]
        public IActionResult Reply(string id)
        {
            TempData["PostID"] = id;
            if (User.Identity.IsAuthenticated)
            {
                return View("Reply", new PostReply());
            }
            else
            {
                return RedirectToAction("Login", "Account", null);
            }
        }
        [HttpPost]
        public IActionResult Reply(PostReply postreply)
        {
            if (ModelState.IsValid)
            {
                string postid = TempData["PostID"].ToString();
                postreply.DateCreated = DateTime.Now;
                IQueryable<PostReply> replyquery = context.PostReplies;
                IQueryable<User> query = context.Users;
                User user = query.FirstOrDefault(u => u.UserName.ToLower() == User.Identity.Name.ToLower());
                postreply.UserId = user.Id;
                PostReply lastpostreply = replyquery.OrderByDescending(r => r.ReplyId).First();
                int lastid = Int32.Parse(lastpostreply.ReplyId) + 1;
                postreply.ReplyId = lastid.ToString();
                postreply.PostId = postid;
                context.PostReplies.Add(postreply);
                context.SaveChanges();
                return RedirectToAction("ViewPost", "Home", new { id = postreply.PostId });
            }
            else
            {
                ViewBag.Id = postreply.ReplyId;
                return View(postreply);
            }
        }
    }
}