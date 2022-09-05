using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus
{
    public class ConstEventName
    {
        public const string FileUpload = "FileUpload";
        public const string Article_FileCallBackUpdated = "ArticleFileCallBackUpdated";
    }

    public enum EnumCallBackEntity
    {
        ArticleClassify,
        ArticleImage,
        ArticleTitleImage
    }
    
}
