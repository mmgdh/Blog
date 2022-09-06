using CommonInitializer;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogInfoService.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BlogParamDbContext>
    {
        public BlogParamDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = DbContextOptionsBuilderFactory.Create<BlogParamDbContext>();
            return new BlogParamDbContext(optionsBuilder.Options, null);
        }
    }
}
