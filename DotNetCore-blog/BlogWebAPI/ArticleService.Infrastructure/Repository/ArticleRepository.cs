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

        public async Task<Article?> GetArticleByIdAsync(Guid ArticleId, bool NeedDetail)
        {
            if (NeedDetail)
            {
                return await dbCtx.Articles.Include(x => x.Classify).Include(x => x.Tags).Include(x=>x.articleContent).FirstOrDefaultAsync(x => x.Id == ArticleId);
            }
            else
            {
                return await dbCtx.Articles.Include(x => x.Classify).Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == ArticleId);
            }

        }

        public async Task<Article[]?> GetArticlesByIdAsync(Guid[] Ids, bool NeedDetail)
        {
            if (NeedDetail)
            {
                return await dbCtx.Articles.Include(x => x.Classify).Include(x => x.Tags).Include(x=>x.articleContent).Where(x => Ids.Contains(x.Id)).ToArrayAsync();
            }
            else
            {
                return await dbCtx.Articles.Include(x => x.Classify).Include(x => x.Tags).Where(x => Ids.Contains(x.Id)).ToArrayAsync();
            }


        }
    }
}
