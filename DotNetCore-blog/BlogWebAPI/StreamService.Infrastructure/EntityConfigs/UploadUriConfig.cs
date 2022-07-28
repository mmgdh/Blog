using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamService.Domain.Entities;

namespace StreamService.Infrastructure.EntityConfigs
{
    public class UploadUriConfig : IEntityTypeConfiguration<UploadUri>
    {
        public void Configure(EntityTypeBuilder<UploadUri> builder)
        {
            builder.ToTable("T_UploadUrl");
            builder.HasKey(e => e.Id).IsClustered(false);//对于Guid主键，不要建聚集索引，否则插入性能很差
        }
    }
}
