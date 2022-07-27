using CommonInfrastructure;
using Commons;
using FileService.Domain;
using FileService.Domain.Entities;
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

        public async Task<Tuple<Byte[],string>> GetFastFile(Guid id)
        {
            var Item =await GetUploadItemAsync(id);
            if (Item == null) throw new Exception("获取文件相关信息失败");
            List<Task<Tuple<Byte[], string>>> uploadTasks = new List<Task<Tuple<Byte[], string>>>();
            foreach(var uploadUri in Item.Uris)
            {
                var storage = storageClients.FirstOrDefault(x => x.StorageType == uploadUri.UrlType);
                if (storage == null) continue;
                var task = Task.Run(async () =>
                {
                    var ret =await storage.GetUploadFileByteArray(uploadUri);
                    return ret;
                });
                uploadTasks.Add(task);
            }
            var ret = await Task.WhenAny(uploadTasks);
            return ret.Result;
        }

        public async Task<UploadItem?> GetUploadItemAsync(Guid id)
        {
            return await _context.Query<UploadItem>().Include(x => x.Uris).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UploadItem> UploadFileAsync(IFormFile file)
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
                     uploadItem.Uris.Add(UploadUri.CreateUploadUrl(ret.AbsoluteUri, storageClients.First().StorageType));
                     return ret;
                 });
                uploadTasks.Add(task);
            }
            _context.uploads.Add(uploadItem);
            var ret = await Task.WhenAny(uploadTasks);
            return uploadItem;


        }

    }
}
