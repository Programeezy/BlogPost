using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPost.Models
{
    public class Like
    {
        public string ArticleId { get; set; }
        public Article Article { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
