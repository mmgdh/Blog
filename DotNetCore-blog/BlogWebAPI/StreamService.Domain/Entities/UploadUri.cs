using DomainCommon;

namespace StreamService.Domain.Entities
{
    public class UploadUri :BaseEntity
    {
        public string Uri { get; private set; } = "";

        public string Key { get; private set; } = "";

        public EnumStorageType UrlType { get; private set; }

        public UploadItem UploadItem { get; private set; } = new UploadItem();



        public static UploadUri CreateUploadUrl(string Uri,string key, EnumStorageType urlType)
        {
            UploadUri uploadUri = new UploadUri();
            uploadUri.Uri = Uri;
            uploadUri.UrlType = urlType;
            uploadUri.Key = key;
            return uploadUri;
        }

    }
}
