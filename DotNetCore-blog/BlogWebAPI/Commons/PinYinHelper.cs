using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.International.Converters.PinYinConverter;

namespace Commons
{
    public class PinYinHelper
    {
        /// <summary>
        /// 获得一个字符串的拼音
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetPinYin(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char item in str)
            {
                //判断是不是汉字，如果不是原字符返回
                if (ChineseChar.IsValidChar(item))
                {
                    sb.Append(GetPinYin(item.ToString()));
                }
                else
                {
                    sb.Append(item);
                }
            }


            return sb.ToString();
        }


        /// <summary>
        /// 获取首字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetFrist(string str)
        {
            string PYstr = "";
            foreach (char item in str.ToCharArray())
            {
                if (ChineseChar.IsValidChar(item))
                {
                    ChineseChar cc = new ChineseChar(item);
                    PYstr += cc.Pinyins[0][0];
                }
                else
                {
                    PYstr += item.ToString()[0];
                }
            }
            return PYstr;
        }
    }
}
