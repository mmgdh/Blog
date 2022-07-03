using DomainCommon;

namespace FileService.Domain.Entities
{
    public  class UploadItem :BaseEntity
    {
        
        public DateTime CreationTime { get; private set; }

        /// <summary>
        /// 文件大小（尺寸为字节）
        /// </summary>
        public long FileSizeInBytes { get; private set; }
        /// <summary>
        /// 用户上传的原始文件名（没有路径）
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// 两个文件的大小和散列值（SHA256）都相同的概率非常小。因此只要大小和SHA256相同，就认为是相同的文件。
        /// SHA256的碰撞的概率比MD5低很多。
        /// </summary>
        public string FileSHA256Hash { get; private set; }
        /// <summary>
        /// 存放地址
        /// </summary>
        public List<UploadUrl> Uris { get; private set; }=new List<UploadUrl>();

        public static UploadItem Create(long fileSizeInBytes, string fileName, string fileSHA256Hash)
        {
            UploadItem item = new UploadItem()
            {
                CreationTime = DateTime.Now,
                FileName = fileName,
                FileSHA256Hash = fileSHA256Hash,
                FileSizeInBytes = fileSizeInBytes
            };
            return item;
        }
    }
}
