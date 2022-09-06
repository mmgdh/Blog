using BlogInfoService.Domain.Entities.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogInfoService.Infrastructure.Repository
{
    public class BlogParameterRepository : IBlogParamRepository
    {
        BlogParamDbContext DbContext;

        public BlogParameterRepository(BlogParamDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<bool> JudgeBlogParamIsExistAsync(string ParamName)
        {
           return await DbContext.blogParameters.AnyAsync(x => x.ParamName == ParamName.TrimEnd());
        }
    }
}
