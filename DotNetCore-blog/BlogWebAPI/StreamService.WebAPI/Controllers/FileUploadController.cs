using StreamService.Domain;
using StreamService.Infrastructure;
using StreamService.WebAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace StreamService.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class FileUploadController : Controller
    {
        private readonly IUploadItemRepository repository;
        private readonly UploadDbContext _context;
        private readonly IWebHostEnvironment hostEnv;

        public FileUploadController(IUploadItemRepository repository, UploadDbContext context, IWebHostEnvironment hostEnv)
        {
            this.repository = repository;
            _context = context;
            this.hostEnv = hostEnv;
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
            var Tuple =await repository.GetFastFile(Id);
            return new FileContentResult(Tuple.Item1, Tuple.Item2);
        }
    }
} 
