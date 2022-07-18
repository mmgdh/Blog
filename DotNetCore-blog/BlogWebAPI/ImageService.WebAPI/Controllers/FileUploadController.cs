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
            var ip = "localhost";
            if (hostEnv.IsProduction())
            {
                ip = HttpContext.Connection.LocalIpAddress?.MapToIPv4()?.ToString();
            }
            var port = HttpContext.Connection.LocalPort;
            //"https://localhost:7282/FileUpload/GetImage?Id=171ece50-6c34-44d6-b6c6-8880861e4820";
            var GetImgUrl = $"https://{ip}:{port}/FileUpload/GetImage?Id={ret.Id}";
            Uri uri = new Uri(GetImgUrl);
            return uri;
        }
        [HttpGet]
        public async Task<FileContentResult> GetImage(Guid Id)
        {
            var byteArray =await repository.GetFastFile(Id);
            byte[] a = null;
            return new FileContentResult(byteArray, "image/jpeg");
        }
    }
} 
