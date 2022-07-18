using ArticleService.Domain.Entities;

namespace ArticleService.WebAPI.Controllers.ViewModels.ResponseModel
{
    public class ArticleResp
    {
        public string Title { get; set; }
        public ArticleClassify Classify { get; set; }

        public Guid ImageId { get; set; }

        public string PinYin { get; private set; }
        /// <summary>
        /// 文章内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public List<ArticleTag> Tags { get; set; } = new List<ArticleTag>();



        public static ArticleResp Create(Article article)
        {
            ArticleResp resp = new ArticleResp();

            article.Classify?.Articles?.Clear();
            article.Tags.ForEach(x => x.Articles.Clear());
            resp.Title = article.Title;       
            resp.Classify = article.Classify;
            resp.Content = article.Content;
            resp.Tags = article.Tags;
            resp.PinYin = article.PinYin;
            resp.ImageId = article.ImageId;
            return resp;

        }
    }
}
