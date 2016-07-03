using QQ.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQ.WebService
{
    public class ResponseHelper
    {

        /// <summary>
        /// 转换为字节数组表示
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static byte[] ToBytes(string str)
        {
            var bytes = new byte[8];
            for (var i = 0; i < 8; i++)
            {
                bytes[i] = byte.Parse(str.Substring((i * 4) + 2, 2), NumberStyles.HexNumber);
            }
            return bytes;
        }

        private static string GetParameterValue(string str, string key)
        {
            var l = key.Length;
            var i = str.IndexOf(key);
            if (i == -1)
            {
                return string.Empty;
            }
            return str.Substring(i + l + 3, str.IndexOf(',', i + l + 4) - i - l - 3).Replace("\"", "");
        }
    }


}

