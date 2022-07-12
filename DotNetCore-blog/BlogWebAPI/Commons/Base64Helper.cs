using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons
{
    internal class Base64Helper
    {
        public static String FileToBase64(Stream fs)
        {
            string strRet = null;

            try
            {
                if (fs == null) return null;
                byte[] bt = new byte[fs.Length];
                fs.Read(bt,0 , bt.Length);
                strRet = Convert.ToBase64String(bt);
                fs.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return strRet;
        }

        /// <summary>
        /// Base64字符串转换成文件
        /// </summary>
        /// <param name="strInput">base64字符串</param>
        /// <param name="fileName">保存文件的绝对路径</param>
        /// <returns></returns>
        public static byte[] Base64ToFile(string strInput, string fileName)
        {

            try
            {
                byte[] buffer = Convert.FromBase64String(strInput);
                return buffer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
