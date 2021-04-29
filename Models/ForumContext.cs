using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace VSTForum.Models
{
    public class ForumContext : IdentityDbContext<User>
    {
        DateTime date = new DateTime(2021, 1, 1, 8, 30, 00);
        public ForumContext(DbContextOptions<ForumContext> options) : base(options)
        {}
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostReply> PostReplies { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            /*
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = "0", CategoryName = "Plugin Deals", Description = "Looking for a good deal on a plugin? Look in here!"},
                new Category { CategoryId = "1", CategoryName = "News", Description = "New version of Pro Tools? New hardware release? Post here!"},
                new Category { CategoryId = "2", CategoryName = "Production Tips", Description = "Got any interesting tips to share? Post them here!"},
                new Category { CategoryId = "3", CategoryName = "Stem Share", Description = "Got any raw recordings you would like to share? Post them here! Copyright laws apply, MAKE SURE YOU EITHER OWN THE RIGHTS TO THE RECORDINGS OR HAVE EXPLICIT PERMISSION TO SHARE!!!!"},
                new Category { CategoryId = "4", CategoryName = "Rate My Mix", Description = "Want someone to critique your mix? Share your mix here!"}
            );
            modelBuilder.Entity<Post>().HasData(
                new Post { PostId = "0", Title = "Test Post", Body = "This is a test post.", CategoryId = "0", DateCreated = date, UserId = "18c99a9e-47c0-4352-aa59-a81b8a0c9c29"}
            );*/
            modelBuilder.Entity<PostReply>().HasData(
                new PostReply { ReplyId = "0", Body = "This is a test reply", DateCreated = date, PostId = "0", UserId = "18c99a9e-47c0-4352-aa59-a81b8a0c9c29" }
            );
                
            
        }
        
        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            UserManager<User> userManager =
                serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string username = "admin";
            string password = "Sesam3";
            string roleName = "Admin";

            // if role doesn't exist, create it
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // if username doesn't exist, create it and add to role
            if (await userManager.FindByNameAsync(username) == null)
            {
                User user = new User { UserName = username };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }
    }
}
