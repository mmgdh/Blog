using BlogInfoService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogInfoService.Infrastructure.EntityConfigs
{
    internal class BlogParameterConfig : IEntityTypeConfiguration<BlogParameter>
    {
        public void Configure(EntityTypeBuilder<BlogParameter> builder)
        {
            builder.ToTable("T_BlogParameter");
            
        }
    }
}
