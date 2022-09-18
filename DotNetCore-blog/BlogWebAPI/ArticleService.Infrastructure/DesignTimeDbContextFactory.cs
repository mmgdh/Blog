
using CommonInitializer;
using Microsoft.EntityFrameworkCore.Design;

namespace ArticleService.Infrastructure;

//用IDesignTimeDbContextFactory坑最少，最省事
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ArticleDbContext>
{
    public ArticleDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = DbContextOptionsBuilderFactory.Create<ArticleDbContext>();
        return new ArticleDbContext(optionsBuilder.Options, null);
    }
}
