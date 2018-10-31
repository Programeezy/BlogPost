using AutoMapper;
using BlogPost.Models;

namespace BlogPost.ViewModels
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Article, ArticleDetailViewModel>()
                .ForMember(s => s.OwnerUsername, map => map.MapFrom(s => s.Owner.Username));
            CreateMap<Article, DraftViewModel>();
            CreateMap<Article, ArticleViewModel>();
        }
    }
}
