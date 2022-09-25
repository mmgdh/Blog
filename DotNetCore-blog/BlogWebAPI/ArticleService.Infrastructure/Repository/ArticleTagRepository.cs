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

        public async Task<bool> TagIsUsedByArticle(Guid id)
        {
            var Tag = await dbCtx.Tags.Include(x => x.Articles).FirstOrDefaultAsync(x => x.Id == id);
            return Tag != null && Tag.Articles.Any();
        }

        public async Task<bool> TagDeletAsync(Guid id)
        {
            var tag = await dbCtx.Tags.FirstAsync(x => x.Id == id);
            if (await TagIsUsedByArticle(id))
            {
                throw new Exception("该标签已被使用，无法删除");
            }
            dbCtx.Remove(tag);
            return true;
        }

        public async Task<Dictionary<ArticleTag, int>> GetArticleTagWithArticleCount()
        {
            Dictionary<ArticleTag, int> result = new Dictionary<ArticleTag, int>();
            var TagCounts = await dbCtx.Tags.Select(x => new
            {
                ArticleTag = x,
                ArticleCount = x.Articles.Count()
            }).ToListAsync();
            foreach(var TagCount in TagCounts)
            {
                result.Add(TagCount.ArticleTag, TagCount.ArticleCount);
            }
            return result;
        }
    }
}
