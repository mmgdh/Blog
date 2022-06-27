﻿namespace ArticleService.Domain
{
    public interface IArticleRepository
    {
        public Task<Article?> GetArticleByIdAsync(Guid ArticleId);

        public Task<Article[]> GetArticlesByArticleTagId(Guid ArticleTagId);

        public Task<ArticleTag?> GetArticleTagByIdAsync(Guid ArticleTagId);

        public bool TagNameIsExist(string TagName);


    }
}
