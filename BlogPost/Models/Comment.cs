using BlogPost.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPost.Models
{
    public class Comment : IEntityBase
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public long CreationTime { get; set; }
        public User Owner { get; set; }
    }
}
