using System;
using System.Collections.Generic;

namespace BlogPost.ViewModels
{
    public class ArticleViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<String> Tags { get; set; } = new List<String>();
        public long PublishTime { get; set; }
    }
}
