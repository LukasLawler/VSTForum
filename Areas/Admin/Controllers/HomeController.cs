using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using VSTForum.Models;
using VSTForum.Areas.Admin.Models;

namespace VSTForum.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private ForumContext context;
        public HomeController(ForumContext ctx, UserManager<User> userMngr,
            SignInManager<User> signInMngr)
        {
            context = ctx;
            userManager = userMngr;
            signInManager = signInMngr;
        }
        public IActionResult Index()
        {
            var admin = new AdminViewModel
            {
                Categories = context.Categories.ToList(),
                Posts = context.Posts.ToList(),
                Users = context.Users.ToList()
            };
            ViewData["Users"] = admin.Users;
            ViewData["Posts"] = admin.Posts;
            ViewData["Categories"] = admin.Categories;
            return View(admin);
        }

        [HttpGet]
        public IActionResult DeleteUser(string ID)
        {
            var userId = new AdminViewModel
            {
                id = ID
            };
            return View(userId);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(AdminViewModel model)
        {
            var replies = context.PostReplies.Where(r => r.UserId == model.id);
            var posts = context.Posts.Where(p => p.UserId == model.id);
            var user = await userManager.FindByIdAsync(model.id);
            foreach(var reply in replies)
            {
                context.PostReplies.Remove(reply);
            }
            foreach(var post in posts)
            {
                context.Posts.Remove(post);
            }
            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult DeletePost(string ID)
        {
            var postId = new AdminViewModel
            {
                id = ID
            };
            return View(postId);
        }

        [HttpPost]
        public IActionResult DeletePost(AdminViewModel model)
        {
            var replies = context.PostReplies.Where(r => r.PostId == model.id);
            var post = context.Posts.First(p => p.PostId == model.id);
            foreach (var reply in replies)
            {
                context.PostReplies.Remove(reply);
            }
            context.Posts.Remove(post);
            context.SaveChanges();
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new Category());
        }

        [HttpPost]
        public IActionResult Add(Category cat)
        {
            Category lastcategory = context.Categories.OrderByDescending(c => c.CategoryId).First();
            int lastid = Int32.Parse(lastcategory.CategoryId) + 1;
            cat.CategoryId = lastid.ToString();
            context.Add(cat);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult DeleteCategory(string ID)
        {
            var catId = new AdminViewModel
            {
                id = ID
            };
            return View(catId);
        }

        [HttpPost]
        public IActionResult DeleteCategory(AdminViewModel model)
        {
            var posts = context.Posts.Where(p => p.CategoryId == model.id);
            var replies = context.PostReplies;
            var category = context.Categories.First(c => c.CategoryId == model.id);
            foreach (var reply in replies)
            {
                foreach (var post in posts)
                {
                    var replytodelete = replies.FirstOrDefault(r => r.PostId == post.PostId);
                    context.PostReplies.Remove(replytodelete);
                }
            }
            foreach (var post in posts)
            {
                context.Posts.Remove(post);
            }
            
            context.Categories.Remove(category);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}