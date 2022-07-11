using ArticleService.Domain.Entities;

namespace ArticleService.Domain.IRepository
{
    public interface IArticleRepository
    {
        /// <summary>
        /// 根据ID获取对应文章
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <returns></returns>
        public Task<Article?> GetArticleByIdAsync(Guid ArticleId);
        /// <summary>
        /// 分页获取文章
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<Article[]> GetArticleByPageAsync(int page,int pageSize);
     


    }
}
