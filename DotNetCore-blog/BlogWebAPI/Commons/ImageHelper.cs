

using Microsoft.AspNetCore.Http;

namespace Commons
{
    public class ImageHelper
    {
        /// <summary>
        /// base64 转 Image
        /// </summary>
        /// <param name="base64"></param>
        public static byte[] Base64ToImage(string base64)
        {
            base64 = base64.Replace("data:image/png;base64,", "").Replace("data:image/jgp;base64,", "").Replace("data:image/jpg;base64,", "").Replace("data:image/jpeg;base64,", "");//将base64头部信息替换
            byte[] bytes = Convert.FromBase64String(base64);
            return bytes;
            //Image mImage = Image.FromStream(memStream);
            //Bitmap bp = new Bitmap(mImage);
        }

        /// <summary>
        /// Image 转成 base64
        /// </summary>
        /// <param name="fileFullName"></param>
        public static ReturnMessage UploadImage(IFormFile file)
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
                ReturnMessage ret = new ReturnMessage(strRet, offset, bt.Length,file.Name,file.FileName);
                return ret;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public record ReturnMessage(string Base64,int Offset,int Length,string Name ,string FileName)
        {
        }

    }
}
