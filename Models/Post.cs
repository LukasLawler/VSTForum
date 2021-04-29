using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace VSTForum.Models
{
    public class Post
    {
        public string PostId { get; set; }
        [Required(ErrorMessage = "A title is required to post")]
        public string Title { get; set; }
        [Required(ErrorMessage = "A post body is required")]
        [MaxLength(2000, ErrorMessage = "Post cannot be longer than 2000 characters")]
        public string Body { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime DateCreated { get; set; }
        [ForeignKey("Category")]
        public string CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<PostReply> PostReplies { get; set; }
    }
}
