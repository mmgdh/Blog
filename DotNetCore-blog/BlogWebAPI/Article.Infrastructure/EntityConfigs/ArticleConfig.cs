using ArticleService.Domain;
using ArticleService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArticleService.Infrastructure.EntityConfigs
{
    public class ArticleConfig : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("T_Articles");
            builder.HasKey(e => e.Id).IsClustered(false);//对于Guid主键，不要建聚集索引，否则插入性能很差
            builder.HasMany(s => s.Tags).WithMany(t => t.Articles).UsingEntity(j => j.ToTable("T_Articles_Tags"));

            builder.HasOne(s => s.Classify).WithMany(t => t.Articles);
        }
    }
}
