using Commons;
using DomainCommon;

namespace ArticleService.Domain
{
    public class ArticleTag:BaseEntity
    {
        public string TagName { get; set; }

        public string PinYin { get; set; }
        public List<Article> Articles { get; set; }

        public ArticleTag Create(string TagName)
        {
            return new ArticleTag
            {
                TagName = TagName,
                PinYin = PinYinHelper.GetFrist(TagName)
            };
        }
    }
}
