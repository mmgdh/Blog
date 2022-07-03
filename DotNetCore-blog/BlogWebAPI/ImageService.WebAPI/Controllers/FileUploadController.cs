using FileService.Domain;
using FileService.Domain.Entities;
using FileService.Infrastructure;
using FileService.WebAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FileService.WebAPI.Controllers
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
        public async Task<Uri> Upload([FromForm] UploadRequest request)
        {
            var file = request.File;
            var ret = await repository.UploadFileAsync(file);
            await _context.SaveChangesAsync();
            return ret;
        }
    }
} 
