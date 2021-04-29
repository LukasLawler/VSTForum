using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VSTForum.Models
{
    public class PostReply
    {
        [Key]
        public string ReplyId { get; set; }
        [Required(ErrorMessage ="A post body is required")]
        public string Body { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime DateCreated { get; set; }
        [ForeignKey("Post")]
        public string PostId { get; set; }
        public Post Post { get; set; }
    }
}
