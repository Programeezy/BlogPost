﻿using BlogPost.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPost.Models
{
    public class Article : IEntityBase
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public long CreationTime { get; set; }
        public long LastEditTime { get; set; }
        public long PublishTime { get; set; }
        public bool Draft { get; set; }

        public User Owner { get; set; }
        public string OwnerId { get; set; }
    }
}
