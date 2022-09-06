using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogInfoService.Domain.Entities.IRepository
{
    public interface IBlogParamRepository
    {
        public Task<bool> JudgeBlogParamIsExistAsync(string ParamName);
    }
}
