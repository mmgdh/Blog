using DomainCommon;

namespace FileService.Domain.Entities
{
    public class UploadUrl :BaseEntity
    {
        public string Url { get; private set; }   

        public EnumStorageType UrlType { get; private set; }

        public UploadItem UploadItem { get; private set; }



        public static UploadUrl CreateUploadUrl(string Url, EnumStorageType urlType)
        {
            UploadUrl uploadUrl = new UploadUrl();
            uploadUrl.Url = Url;
            uploadUrl.UrlType = urlType;
            return uploadUrl;
        }

    }
}
