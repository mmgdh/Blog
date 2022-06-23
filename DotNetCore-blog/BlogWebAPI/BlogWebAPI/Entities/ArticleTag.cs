using DomainCommon;

namespace Ariticle.Domain
{
    public class ArticleTag:BaseEntity
    {
        public string TagName { get; set; }

        public List<Article> Articles { get; set; }
    }
}
