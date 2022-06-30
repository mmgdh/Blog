using Commons;
using DomainCommon;

namespace ArticleService.Domain
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

        public string PinYin { get; private set; }
        /// <summary>
        /// 文章内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public List<ArticleTag> Tags { get; set; }



        public static Article Create(string Title,string Content,List<ArticleTag> tags)
        {
            try
            {
                var r = new Article();
                r.Title = Title;
                r.Content = Content;
                tags.ForEach(x => x.Articles.Add(r));
                r.Tags = tags;
                return r;
            }
            catch(Exception ex)
            {

            }


            return null;
        }
    }
}
