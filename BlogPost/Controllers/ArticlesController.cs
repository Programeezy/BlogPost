using AutoMapper;
using BlogPost.Data.Repositories;
using BlogPost.Models;
using BlogPost.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPost.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        ArticleRepository articleRepository;
        IMapper mapper;

        public ArticlesController(ArticleRepository articleRepository, IMapper mapper)
        {
            this.articleRepository = articleRepository;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public ActionResult<ArticleDetailViewModel> GetArticleDetail(string id)
        {
            var article = articleRepository.GetSingle(s => s.Id == id, s => s.Owner);
            return mapper.Map<ArticleDetailViewModel>(article);
        }

        [HttpPost]
        public ActionResult<ArticleCreationViewModel> Post([FromBody]UpdateArticleViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var ownerId = HttpContext.User.Identity.Name;
            var creationTime = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();
            var articleId = Guid.NewGuid().ToString();
            var article = new Article
            {
                Id = articleId,
                Title = model.Title,
                Content = model.Content,
                Tags = model.Tags,
                CreationTime = creationTime,
                LastEditTime = creationTime,
                OwnerId = ownerId,
                Draft = true
            };

            articleRepository.Add(article);
            articleRepository.Commit();

            return new ArticleCreationViewModel
            {
                ArticleId = articleId
            };
        }

        [HttpPatch("{id}")]
        public ActionResult Patch(string id, [FromBody]UpdateArticleViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var ownerId = HttpContext.User.Identity.Name;
            if (!articleRepository.IsOwner(id, ownerId)) return Forbid("You are not the owner of this article");

            var newArticle = articleRepository.GetSingle(id);
            newArticle.Title = model.Title;
            newArticle.LastEditTime = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();
            newArticle.Tags = model.Tags;
            newArticle.Content = model.Content;

            articleRepository.Update(newArticle);
            articleRepository.Commit();

            return NoContent();
        }

        [HttpPost("{id}/publish")]
        public ActionResult Post(string id)
        {
            var ownerId = HttpContext.User.Identity.Name;
            if (!articleRepository.IsOwner(id, ownerId)) return Forbid("You are not the owner of this article");

            var newArticle = articleRepository.GetSingle(id);
            newArticle.Draft = false;
            newArticle.PublishTime = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();

            articleRepository.Update(newArticle);
            articleRepository.Commit();

            return NoContent();
        }

        [HttpGet("drafts")]
        public ActionResult<DraftsViewModel> Get()
        {
            var ownerId = HttpContext.User.Identity.Name;

            var drafts = articleRepository.FindBy(article => article.OwnerId == ownerId && article.Draft);
            return new DraftsViewModel
            {
                Articles = drafts.Select(mapper.Map<DraftViewModel>).ToList()
            };
        }

        [HttpGet("user/{id}")]
        public ActionResult<ArticlesViewModel> Get(string id)
        {
            var articles = articleRepository.FindBy(article => article.OwnerId == id && !article.Draft);
            return new ArticlesViewModel
            {
                Articles = articles.Select(mapper.Map<ArticleViewModel>).ToList()
            };
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var ownerId = HttpContext.User.Identity.Name;
            if (!articleRepository.IsOwner(id, ownerId)) return Forbid("You are not the owner of this article");

            articleRepository.DeleteWhere(article => article.Id == id);
            articleRepository.Commit();

            return NoContent();
        }
    }
}
