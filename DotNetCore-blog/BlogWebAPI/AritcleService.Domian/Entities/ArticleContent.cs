using DomainCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleService.Domain.Entities
{
    public class ArticleContent :BaseEntity
    {
        public Article article { get; set; }

        public Guid ArticleId { get; set; }

        public string Content { get; set; }

        public static ArticleContent Create(Article article,string Content)
        {
            ArticleContent articleContent = new ArticleContent();
            articleContent.Content = Content;
            articleContent.article = article;
            return articleContent;
        }
    }
}
