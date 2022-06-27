using ArticleService.Domain;

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

        public  async Task<Article[]> GetArticlesByArticleTagId(Guid ArticleTagId)
        {
            var Tag= await dbCtx.Tags.FindAsync(ArticleTagId);
            if (Tag == null)
                throw new Exception("未找到对应的Tag");
            return dbCtx.Articles.Where(x => x.Tags.Contains(Tag)).ToArray();
        }

        public async Task<ArticleTag?> GetArticleTagByIdAsync(Guid ArticleTagId)
        {
            return await dbCtx.Tags.FindAsync(ArticleTagId);
        }

        public bool TagNameIsExist(string TagName)
        {
            return dbCtx.Tags.Any(x => x.TagName == TagName);
        }
    }
}
