using StreamService.Domain;
using StreamService.Infrastructure;
using StreamService.WebAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using CommonHelpers;

namespace StreamService.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class FileUploadController : Controller
    {
        private readonly IUploadItemRepository repository;
        private readonly UploadDbContext _context;
        private readonly IWebHostEnvironment hostEnv;
        private readonly RedisHelper redisHelper;

        public FileUploadController(IUploadItemRepository repository, UploadDbContext context, IWebHostEnvironment hostEnv, RedisHelper redisHelper)
        {
            this.repository = repository;
            _context = context;
            this.hostEnv = hostEnv;
            this.redisHelper = redisHelper;
            redisHelper.DbNum = 2;
        }

        [HttpPost]
        public async Task<Guid> Upload([FromForm] UploadRequest request)
        {
            
            var file = request.File;
            var Type = request.UploadType;
            var ret = await repository.UploadFileAsync(Type,file);
            await _context.SaveChangesAsync();
            return ret.Id;
        }
        [HttpGet]
        public async Task<FileContentResult> GetImage(Guid Id)
        {
            var redisRet = await redisHelper.GetFileCacheAsync(Id.ToString());
            if (redisRet != null)
            {
                return new FileContentResult(redisRet.Item1, redisRet.Item2);
            }

            var Tuple =await repository.GetFastFile(Id);
            await redisHelper.AddFileCache(Id.ToString(),Tuple.Item1, Tuple.Item2);
            
            return new FileContentResult(Tuple.Item1, Tuple.Item2);
        }
    }
} 
