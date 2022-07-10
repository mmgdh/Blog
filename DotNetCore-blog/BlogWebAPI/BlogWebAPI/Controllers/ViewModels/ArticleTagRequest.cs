﻿using ArticleService.Domain;
using ArticleService.Domain.Entities;

namespace ArticleService.WebAPI.Controllers.ViewModels
{
    public record ArticleTagRequest(string TagName, Guid id)
    {
        public ArticleTag ToArticleTag()
        {
            return new ArticleTag
            {
                TagName = TagName,
                Id = id,
                Articles=new List<Article>()
            };
        }
    }

}
