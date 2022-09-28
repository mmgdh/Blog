using DomainCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleService.Domain.Entities
{
    public class ArticleHtml : BaseEntity
    {
        public Article article { get; set; }

        public Guid ArticleId { get; set; }

        public string Html { get; set; }

        public static ArticleHtml Create(Article article, string Html)
        {
            ArticleHtml articleContent = new ArticleHtml();
            articleContent.Html = Html;
            articleContent.article = article;
            return articleContent;
        }
    }
}
