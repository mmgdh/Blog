using ArticleService.Domain.Entities;

namespace ArticleService.Domain
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
     
        /// <summary>
        /// 获取所有标签
        /// </summary>
        /// <returns></returns>
        public Task<ArticleTag[]> GetAllArticleTagsAsync();
        /// <summary>
        /// 根据标签获取对应文章
        /// </summary>
        /// <param name="ArticleTagId"></param>
        /// <returns></returns>
        public Task<Article[]> GetArticlesByArticleTagIdAsync(Guid ArticleTagId);
        /// <summary>
        /// 根据ID获取文章标签
        /// </summary>
        /// <param name="ArticleTagId"></param>
        /// <returns></returns>
        public Task<ArticleTag?> GetArticleTagByIdAsync(Guid ArticleTagId);
        /// <summary>
        /// 判断标签是否存在
        /// </summary>
        /// <param name="TagName"></param>
        /// <returns></returns>
        public Task<bool> TagNameIsExistAsync(string TagName);

    }
}
