using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus
{
    public class EventBusParameter
    {
        public record CallBackNeed(Guid? MasterGuidId, EnumCallBackEntity CallBackEntity, string CallBackEventName);

        public record CallBackUpdateEntity(CallBackNeed CallBackNeed, Guid FileGuidId);

        public record UploadFile(string Base64, int Offset, int Length, string Name, string FileName,string ContentType);
        public record FileUpload_Parameter(string UploadType,UploadFile UploadFile, CallBackNeed CallBackNeed);


    }
}
