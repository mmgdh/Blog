using ArticleService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleService.Infrastructure.EntityConfigs
{
    public class ArticleHtmlConfig : IEntityTypeConfiguration<ArticleHtml>
    {
        public void Configure(EntityTypeBuilder<ArticleHtml> builder)
        {
            builder.ToTable("T_ArticleHtml");
        }
    }
}
