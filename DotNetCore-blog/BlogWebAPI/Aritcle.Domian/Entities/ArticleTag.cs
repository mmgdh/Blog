using Commons;
using DomainCommon;

namespace ArticleService.Domain
{
    public class ArticleTag:BaseEntity
    {
        private string _TagName;
        public string TagName
        {
            get
            {
                return _TagName;
            }
            set
            {
                _TagName =value;
                PinYin = PinYinHelper.GetFrist(value);
            }
        }

        public string PinYin { get;private set; }
        public List<Article> Articles { get; set; }

        public static ArticleTag Create(string TagName)
        {
            return new ArticleTag
            {
                TagName = TagName
            };
        }

        //public static ArticleTag Get(string id)
        //{
        //    return new ArticleTag
        //    {
        //        TagName = TagName
        //    };
        //}
    }
}
