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
            return await dbCtx.FindAsync<Article>(ArticleId);
        }

        public async Task<Article[]> GetArticleByPageAsync(int page, int pageSize)
        {
            return await dbCtx.Articles.Include(x => x.Tags).OrderByDescending(x => x.CreationTime).Skip(page * pageSize).Take(pageSize).ToArrayAsync();
        }
    }
}
