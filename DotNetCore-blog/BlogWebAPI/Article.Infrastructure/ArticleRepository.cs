using ArticleService.Domain;
using ArticleService.Domian;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleService.Infrastructure
{
    public class ArticleRepository : IArticleRepository
    {
        public Task<Article> GetArticleByIdAsync(Guid ArticleId)
        {
            throw new NotImplementedException();
        }

        public Task<Article[]> GetArticlesByArticleTagId(Guid ArticleTagId)
        {
            throw new NotImplementedException();
        }

        public Task<ArticleTag> GetArticleTagByIdAsync(Guid ArticleTagId)
        {
            throw new NotImplementedException();
        }
    }
}
