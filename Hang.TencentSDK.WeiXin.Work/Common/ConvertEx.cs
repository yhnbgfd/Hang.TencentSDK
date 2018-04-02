using System.Collections.Generic;
using System.Text;

namespace Hang.TencentSDK.WeiXin.Work.Common
{
    public static class ConvertEx
    {
        /// <summary>
        /// 将数组转换成按拼接符拼接的字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr">源数组</param>
        /// <param name="splitChar">拼接的字符</param>
        /// <returns></returns>
        public static string ToStitchingString(this IEnumerable<string> enumerable, char splitChar)
        {
            if (enumerable == null)
            {
                return string.Empty;
            }
            var sb = new StringBuilder();
            foreach (var item in enumerable)
            {
                sb.Append($"{item}{splitChar}");
            }
            return sb.ToString().TrimEnd(splitChar);
        }
    }
}
