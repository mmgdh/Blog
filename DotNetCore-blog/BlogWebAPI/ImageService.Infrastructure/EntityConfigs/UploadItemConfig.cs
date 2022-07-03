using FileService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Infrastructure.EntityConfigs
{
    public class UploadItemConfig : IEntityTypeConfiguration<UploadItem>
    {
        public void Configure(EntityTypeBuilder<UploadItem> builder)
        {
            builder.ToTable("T_UploadItem");
            builder.HasKey(e => e.Id).IsClustered(false);//对于Guid主键，不要建聚集索引，否则插入性能很差
            builder.HasMany(x => x.Uris).WithOne(t => t.UploadItem);
        }
    }
}
