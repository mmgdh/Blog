using ArticleService.Domain;
using ArticleService.Domain.Entities;
using ArticleService.Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ArticleService.Infrastructure
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ArticleDbContext dbCtx;
        public ArticleRepository(ArticleDbContext _dbCtx)
        {
            dbCtx = _dbCtx;
        }

        public async Task<Article?> GetArticleByIdAsync(Guid ArticleId, bool NeedContent, bool NeedHtml)
        {
            return await GetArticleQueryAble(NeedContent, NeedHtml).FirstOrDefaultAsync(x => x.Id == ArticleId);
        }

        public async Task<Article[]?> GetArticlesByIdAsync(Guid[] Ids, bool NeedContent, bool NeedHtml)
        {

            return await GetArticleQueryAble(NeedContent, NeedHtml).Where(x => Ids.Contains(x.Id)).ToArrayAsync();
        }

        public IQueryable<Article> GetArticleQueryAble(bool NeedContent, bool NeedHtml)
        {
            var linq = dbCtx.Articles.Include(x => x.Classify).Include(x => x.Tags).AsQueryable();
            if (NeedContent) linq = linq.Include(x => x.articleContent);
            if (NeedHtml) linq = linq.Include(x => x.articleHtml);
            return linq;
        }
    }
}
