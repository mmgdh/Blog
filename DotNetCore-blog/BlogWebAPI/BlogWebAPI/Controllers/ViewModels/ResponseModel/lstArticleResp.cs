using ArticleService.Domain.Entities;

namespace ArticleService.WebAPI.Controllers.ViewModels.ResponseModel
{
    public class lstArticleResp
    {
        public int PageArticleCount { get; set; }
        public lstArticles[] articles { get; set; }



        public class lstArticles
        {
            public Guid id { get; set; }
            public string Title { get; set; } = "";
            public ArticleClassify? Classify { get; set; }

            public Guid ImageId { get; set; }
            /// <summary>
            /// 拼音
            /// </summary>
            public string? PinYin { get;  set; }
            /// <summary>
            /// 文章内容描述
            /// </summary>
            public string? Description { get; set; }
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
        }

        public static lstArticles[] CreateArticles(Article[] article)
        {
            var ret = article.Select(article =>
            {
                lstArticles resp = new lstArticles();

                article.Classify?.Articles?.Clear();
                article.Tags.ForEach(x => x.Articles.Clear());
                resp.id = article.Id;
                resp.Title = article.Title;
                resp.Classify = article.Classify;
                resp.Description = article.Description;
                resp.Tags = article.Tags;
                resp.PinYin = article.PinYin;
                resp.ImageId = article.ImageId;
                resp.CreateDateTime = article.CreationTime;
                resp.UpdateDateTime = article.LastModificationTime;
                return resp;
            });
            return ret.ToArray();


        }
    }
}
