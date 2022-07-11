using ArticleService.Domain.Entities;
using ArticleService.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleService.Infrastructure.Repository
{
    public class ArticleTagRepository : IArticleTagRepository
    {
        private readonly ArticleDbContext dbCtx;

        public ArticleTagRepository(ArticleDbContext dbCtx)
        {
            this.dbCtx = dbCtx;
        }

        public async Task<Article[]> GetArticlesByArticleTagIdAsync(Guid ArticleTagId)
        {
            var Tag = await dbCtx.Tags.FindAsync(ArticleTagId);
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
    }
}
