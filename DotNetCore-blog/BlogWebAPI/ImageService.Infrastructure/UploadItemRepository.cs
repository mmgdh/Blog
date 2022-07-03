using CommonInfrastructure;
using Commons;
using FileService.Domain;
using FileService.Domain.Entities;
using FileService.Infrastructure.StorageClients;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FileService.Infrastructure
{
    internal class UploadItemRepository : IUploadItemRepository
    {
        private readonly UploadDbContext _context;

        public List<IStorageClient> storageClients;

        public UploadItemRepository(UploadDbContext context, IEnumerable<IStorageClient> _storageClients)
        {
            _context = context;
            storageClients = _storageClients.ToList();
        }

        public async Task<UploadItem?> GetUploadItemAsync(Guid id)
        {
            return await _context.Query<UploadItem>().Include(x => x.Uris).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Uri> UploadFileAsync(IFormFile file)
        {
            using Stream stream = file.OpenReadStream();
            string fileName = file.FileName;
            string hash = HashHelper.ComputeSha256Hash(stream);
            long fileSize = stream.Length;
            DateTime today = DateTime.Today;
            string key = $"{today.Year}/{today.Month}/{today.Day}/{hash}/{fileName}";
            List<Task<Uri>> uploadTasks = new List<Task<Uri>>();
            UploadItem uploadItem = UploadItem.Create(fileSize, fileName, hash);
            foreach (var storage in storageClients)
            {
                var task = Task.Run(() =>
                 {
                     var ret = storage.UploadFileASync(key, file).Result;
                     uploadItem.Uris.Add(UploadUrl.CreateUploadUrl(ret.AbsoluteUri, storageClients.First().StorageType));
                     return ret;
                 });
                uploadTasks.Add(task);
            }
            _context.uploads.Add(uploadItem);
            var ret = Task.WhenAny(uploadTasks);
            return ret.Result.Result;


        }

    }
}
