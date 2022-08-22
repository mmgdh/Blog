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

        public async Task<Article?> GetArticleByIdAsync(Guid ArticleId)
        {
            return await dbCtx.Articles.Include(x => x.Classify).Include(x => x.Tags).Include(x=>x.articleContent).FirstOrDefaultAsync(x => x.Id == ArticleId);
        }
    }
}
