using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSTForum.Models
{
    public class Category
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
