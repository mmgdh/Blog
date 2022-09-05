using Microsoft.AspNetCore.Http;
using CommomConst;
namespace EventBus
{
    public class EventBusHelper
    {
        public static EventBusParameter.UploadFile IFormFileToEventBusParameter(Microsoft.AspNetCore.Http.IFormFile file)
        {
            try
            {

                string strRet = "";
                int offset = 0;
                byte[] bt = null;
                using Stream fs = file.OpenReadStream();
                try
                {
                    if (fs == null) return null;
                    bt = new byte[fs.Length];
                    fs.Read(bt, offset, bt.Length);
                    strRet = Convert.ToBase64String(bt);
                    fs.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                EventBusParameter.UploadFile ret = new EventBusParameter.UploadFile(strRet, offset, bt.Length, file.Name, file.FileName,file.ContentType);
                return ret;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static void EventBusFunc_UploadImg(EnumCallBackEntity enumCallBackEntity, string UploadImgType, IFormFile formFile,Guid MasterId,IEventBus eventBus)
        {
            var UploadFile = EventBusHelper.IFormFileToEventBusParameter(formFile);
            var CallBackNeed = new EventBusParameter.CallBackNeed(MasterId, enumCallBackEntity, ConstEventName.Article_FileCallBackUpdated);
            EventBusParameter.FileUpload_Parameter parameter = new EventBusParameter.FileUpload_Parameter(UploadImgType, UploadFile, CallBackNeed);
            eventBus.publish(ConstEventName.FileUpload, parameter);
        }
    }
}
