using ArticleService.Domain.Entities;
using ArticleService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleService.Infrastructure.Repository
{
    public class ArticleClassifyRepository : IArticleClassifyRepository
    {
        private readonly ArticleDbContext dbCtx;

        public ArticleClassifyRepository(ArticleDbContext dbCtx)
        {
            this.dbCtx = dbCtx;
        }

        public Task<bool> ArticleClassifyNameIsExistAsync(string TagName)
        {
            throw new NotImplementedException();
        }

        public Task<ArticleClassify[]> GetAllArticleClassifyAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ArticleClassify?> GetArticleClassifyByIdAsync(Guid ArticleTagId)
        {
            throw new NotImplementedException();
        }

        public Task<Article[]> GetArticlesByArticleClassifyIdAsync(Guid ArticleTagId)
        {
            throw new NotImplementedException();
        }
    }
}
