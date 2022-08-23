using ArticleService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleService.Domain.IRepository
{
    public interface IArticleClassifyRepository
    {
        /// <summary>
        /// 获取所有分类
        /// </summary>
        /// <returns></returns>
        public Task<ArticleClassify[]> GetAllArticleClassifyAsync();
        /// <summary>
        /// 根据分类获取对应文章
        /// </summary>
        /// <param name="ArticleTagId"></param>
        /// <returns></returns>
        public Task<Article[]> GetArticlesByArticleClassifyIdAsync(Guid ArticleTagId);
        /// <summary>
        /// 根据ID获取文章分类
        /// </summary>
        /// <param name="ArticleTagId"></param>
        /// <returns></returns>
        public Task<ArticleClassify?> GetArticleClassifyByIdAsync(Guid ArticleTagId);
        /// <summary>
        /// 判断分类是否存在
        /// </summary>
        /// <param name="TagName"></param>
        /// <returns></returns>
        public  Task<bool> ArticleClassifyNameIsExistAsync(string TagName);
        /// <summary>
        /// 判断分类是否已被使用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> ClassifyIsUsedByArticle(Guid id);
        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> ArticleClassifyDelete(Guid id);
    }
}
