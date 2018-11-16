using AutoMapper;
using BlogPost.Data.Repositories;
using BlogPost.Models;
using BlogPost.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPost.Controllers
{
    [Route("[controller]")]
    public class PlainArticlesController : Controller
    {
        ArticleRepository articleRepository;
        IMapper mapper;

        public PlainArticlesController(ArticleRepository articleRepository, IMapper mapper)
        {
            this.articleRepository = articleRepository;
            this.mapper = mapper;
        }

        [HttpGet()]
        public IActionResult Articles()
        {
            var articles = articleRepository.AllIncluding(s => s.Owner);
            var viewModel = new ArticlesViewModel
            {
                Articles = articles.Select(mapper.Map<ArticleViewModel>).ToList()
            };
            return View(viewModel);
        }

        [HttpGet("{id}")]
        public ActionResult<ArticleDetailViewModel> ArticleDetail(string id)
        {
            var article = articleRepository.GetSingle(s => s.Id == id, s => s.Owner, s => s.Likes);
            var userId = HttpContext.User.Identity.Name;
            var liked = article.Likes.Exists(l => l.UserId == userId);

            var viewModel = mapper.Map<Article, ArticleDetailViewModel>(
                article,
                opt => opt.AfterMap((src, dest) => dest.Liked = liked)
            );

            return View(viewModel);
        }
    }
}