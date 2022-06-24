using ArticleService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ArticleService.Domian
{
    public interface IArticleRepository
    {
        public Task<Article> GetArticleByIdAsync(Guid ArticleId);

        public Task<Article[]> GetArticlesByArticleTagId(Guid ArticleTagId);

        public Task<ArticleTag> GetArticleTagByIdAsync(Guid ArticleTagId);


    }
}
