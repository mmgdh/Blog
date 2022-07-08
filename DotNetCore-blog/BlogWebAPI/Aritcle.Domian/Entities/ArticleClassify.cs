using DomainCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleService.Domain.Entities
{
    /// <summary>
    /// 文章分类
    /// </summary>
    internal class ArticleClassify : BaseEntity
    {
        public string ClassifyText { get; set; }

        public List<Article> Articles { get; private set; }

        public Guid DefaultImgId { get; set; }
    }
}
