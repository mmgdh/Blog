using ArticleService.Domain.Entities;

namespace ArticleService.WebAPI.Controllers.ViewModels.ResponseModel
{
    public class ArticleResp
    {
        public Guid id { get; set; }
        public string Title { get; set; } = "";
        public ArticleClassify? Classify { get; set; }

        public Guid ImageId { get; set; }
        /// <summary>
        /// 拼音
        /// </summary>
        public string? PinYin { get; set; }
        /// <summary>
        /// 文章内容描述
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// 文章内容
        /// </summary>
        public string? Content { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDateTime { get; set; }
        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime? UpdateDateTime { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public List<ArticleTag> Tags { get; set; } = new List<ArticleTag>();

        public static ArticleResp Create(Article article,bool needDetail)
        {
            ArticleResp resp = new ArticleResp();

            article.Classify?.Articles?.Clear();
            article.Tags.ForEach(x => x.Articles.Clear());
            resp.id = article.Id;
            resp.Title = article.Title;
            resp.Classify = article.Classify;
            resp.Tags = article.Tags;
            resp.PinYin = article.PinYin;
            resp.ImageId = article.ImageId;
            resp.CreateDateTime = article.CreationTime;
            resp.UpdateDateTime = article.LastModificationTime;
            resp.Description = article.Description;
            if (needDetail)
            {
                resp.Content = article.articleContent.Content;
            }
            return resp;

        }
    }
}
