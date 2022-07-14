

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

    }
}
