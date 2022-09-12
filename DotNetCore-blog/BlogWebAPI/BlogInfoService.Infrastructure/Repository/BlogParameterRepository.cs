using BlogInfoService.Domain.Entities.IRepository;
using Microsoft.EntityFrameworkCore;

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
