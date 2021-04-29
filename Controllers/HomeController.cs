using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSTForum.Models;

namespace VSTForum.Controllers
{
    public class HomeController : Controller
    {
        private ForumContext context;
        public HomeController(ForumContext ctx)
        {
            context = ctx;
        }

        public IActionResult Index()
        {
            var categories = context.Categories
                .ToList();
            return View(categories);
        }
        public IActionResult Forum()
        {
            var posts = context.Posts
                .ToList();
            return View(posts);
        }

        public IActionResult Category(string id)
        {
            ViewBag.CategoryName = context.Categories.First(c => c.CategoryId == id).CategoryName;
            IQueryable<Post> query = context.Posts;
            query = query.Where(
                p => p.Category.CategoryId.ToLower() == id.ToLower());
            var posts = query.ToList();
            return View(posts);
        }

        public IActionResult ViewPost(string id)
        {
            var model = new PostViewModel
            {
                post = context.Posts.First(p => p.PostId == id),
                postReply = context.PostReplies
                            .Where(r => r.PostId == id)
                            .ToList()
            };
            ViewData["Post"] = model.post;
            ViewData["PostReplies"] = model.postReply;
            return View(model);
        }
    }
}
