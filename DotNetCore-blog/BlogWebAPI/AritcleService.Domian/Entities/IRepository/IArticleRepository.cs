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
        public Task<Article?> GetArticleByIdAsync(Guid ArticleId,bool NeedDetail,bool NeedHtml);

        public Task<Article[]?> GetArticlesByIdAsync(Guid[] Ids, bool NeedDetail, bool NeedHtml);

    }
}
