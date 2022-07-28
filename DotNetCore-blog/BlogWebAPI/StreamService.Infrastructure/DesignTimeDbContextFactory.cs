
using CommonInitializer;
using Microsoft.EntityFrameworkCore.Design;

namespace StreamService.Infrastructure;

//用IDesignTimeDbContextFactory坑最少，最省事
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<UploadDbContext>
{
    public UploadDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = DbContextOptionsBuilderFactory.Create<UploadDbContext>();
        return new UploadDbContext(optionsBuilder.Options, null);
    }
}
