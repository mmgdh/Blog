using Commons;
using DomainCommon;

namespace ArticleService.Domain.Entities
{
    public class Article : AggregateRootEntity
    {
        private string _Title;
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



        public static Article Create(string Title,string Content)
        {
            try
            {
                var r = new Article();
                r.Title = Title;
                r.Content = Content;
                return r;
            }
            catch(Exception ex)
            {

            }


            return null;
        }
    }
}
