using ArticleService.Domain;
using ArticleService.Domain.Entities;
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

        public  async Task<Article[]> GetArticlesByArticleTagIdAsync(Guid ArticleTagId)
        {
            var Tag= await dbCtx.Tags.FindAsync(ArticleTagId);
            if (Tag == null)
                throw new Exception("未找到对应的Tag");
            return await dbCtx.Articles.Where(x => x.Tags.Contains(Tag)).ToArrayAsync();
        }

        public async Task<ArticleTag?> GetArticleTagByIdAsync(Guid ArticleTagId)
        {
            return await dbCtx.Tags.FindAsync(ArticleTagId);
        }

        public async Task<ArticleTag[]> GetAllArticleTagsAsync()
        {
            return await dbCtx.Tags.ToArrayAsync();
        }

        public async Task<bool> TagNameIsExistAsync(string TagName)
        {
            return await dbCtx.Tags.AnyAsync(x => x.TagName == TagName);
        }

        public async Task<Article[]> GetArticleByPageAsync(int page, int pageSize)
        {
            return await dbCtx.Articles.Include(x => x.Tags).OrderByDescending(x => x.CreationTime).Skip(page * pageSize).Take(pageSize).ToArrayAsync();
        }
    }
}
