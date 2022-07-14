using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                EventBusParameter.UploadFile ret = new EventBusParameter.UploadFile(strRet, offset, bt.Length, file.Name, file.FileName);
                return ret;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
