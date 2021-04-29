using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSTForum.Models;

namespace VSTForum.Areas.Admin.Models
{
    public class AdminViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<PostReply> PostReplies { get; set; }
        public User UserDelete { get; set; }
        public string id { get; set; }
    }
}
