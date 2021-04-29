using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSTForum.Models
{
    public class PostViewModel
    {
        public Post post { get; set; }
        public IEnumerable<PostReply> postReply { get; set; }
    }
}
