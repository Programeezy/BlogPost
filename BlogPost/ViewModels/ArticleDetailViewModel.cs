using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPost.ViewModels
{
    public class ArticleDetailViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Tags { get; set; }
        public long PublishTime { get; set; }

        public string OwnerId { get; set; }
        public string OwnerUsername { get; set; }
    }
}
