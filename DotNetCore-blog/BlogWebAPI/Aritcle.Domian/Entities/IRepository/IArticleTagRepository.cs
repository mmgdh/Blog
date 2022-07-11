using ArticleService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleService.Domain.IRepository
{
    public interface IArticleTagRepository
    {
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
