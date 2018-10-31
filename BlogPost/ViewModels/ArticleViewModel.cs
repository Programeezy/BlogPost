using System.Collections.Generic;

namespace BlogPost.ViewModels
{
    public class ArticleViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public long PublishTime { get; set; }
    }
}
