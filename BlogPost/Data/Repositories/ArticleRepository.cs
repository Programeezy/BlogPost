using BlogPost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPost.Data.Repositories
{
    public class ArticleRepository : ModelBaseRepository<Article>
    {
        public ArticleRepository (BlogContext context) : base (context) { }

        public bool IsOwner(string articleId, string userId)
        {
            var article = this.GetSingle(articleId);
            return article.OwnerId == userId;
        }
    }
}
