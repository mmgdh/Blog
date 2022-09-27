using CommonHelpers;
using Commons;
using DomainCommon;

namespace ArticleService.Domain.Entities
{
    public class Article : AggregateRootEntity
    {
        private string _Title = "";
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
                PinYin = PinYinHelper.GetFrist(value);
            }
        }
        public ArticleClassify Classify { get; set; } = new ArticleClassify();

        public Guid ImageId { get; set; }

        public string? Description { get; set; }

        public string PinYin { get; private set; } = "";
        /// <summary>
        /// 文章内容
        /// </summary>
        public ArticleContent articleContent { get; set; } = new ArticleContent();
        /// <summary>
        /// 标签
        /// </summary>
        public List<ArticleTag> Tags { get; set; } = new List<ArticleTag>();



        public static Article Create(string Title, string Content)
        {
            var article = new Article();
            article.Title = Title;
            article.articleContent = ArticleContent.Create(article, Content);
            article.SetContent(Content);
            return article;
        }

        public void SetContent(string Content)
        {
            this.Description = RegexHelper.GetContent(Content, 200);
        }
    }
}
