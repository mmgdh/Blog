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
    public class ArticleClassifyRepository : IArticleClassifyRepository
    {
        private readonly ArticleDbContext dbCtx;

        public ArticleClassifyRepository(ArticleDbContext dbCtx)
        {
            this.dbCtx = dbCtx;
        }
        public async Task<bool> ClassifyIsUsedByArticle(Guid id)
        {
            var Tag = await dbCtx.Tags.Include(x => x.Articles).FirstOrDefaultAsync(x => x.Id == id);
            return Tag != null && Tag.Articles.Any();
        }
        public async Task<bool> ArticleClassifyDelete(Guid id)
        {
            var tag = await dbCtx.ArticleClassifies.FirstAsync(x => x.Id == id);
            if (await ClassifyIsUsedByArticle(id))
            {
                throw new Exception("该分类已被使用，无法删除");
            }
            dbCtx.Remove(tag);
            return true;
        }

        public async Task<bool> ArticleClassifyNameIsExistAsync(string ClassifyName)
        {
            return await dbCtx.ArticleClassifies.AnyAsync(x => x.ClassifyName == ClassifyName);
        }

        public async Task<ArticleClassify[]> GetAllArticleClassifyAsync()
        {
            return await dbCtx.ArticleClassifies.Include(x => x.Articles).ToArrayAsync();
        }

        public async Task<ArticleClassify?> GetArticleClassifyByIdAsync(Guid ClassifyId)
        {
            return await dbCtx.ArticleClassifies.Include(x => x.Articles).FirstOrDefaultAsync(x => x.Id == ClassifyId);
        }

        public async Task<Article[]> GetArticlesByArticleClassifyIdAsync(Guid ClassifyId)
        {
            var Classify = await dbCtx.ArticleClassifies.FindAsync(ClassifyId);
            if (Classify == null)
                throw new Exception("未找到对应的Tag");
            return await dbCtx.Articles.Where(x => x.Classify == Classify).ToArrayAsync();
        }
    }
}
