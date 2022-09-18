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
    public class ArticleClassifyConfig : IEntityTypeConfiguration<ArticleClassify>
    {
        public void Configure(EntityTypeBuilder<ArticleClassify> builder)
        {
            builder.ToTable("T_ArticleClassify");
        }
    }
}
