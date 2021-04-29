using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace VSTForum.Models
{
    public class User : IdentityUser
    {
        
        public ICollection<Post> Posts { get; set; }
        public ICollection<PostReply> PostReplies { get; set; }
        
        [NotMapped]
        public IList<string> RoleNames { get; set; }

    }
}
