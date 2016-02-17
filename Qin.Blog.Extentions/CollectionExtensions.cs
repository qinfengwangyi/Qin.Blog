using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Blog.Extentions
{
    public static class CollectionExtensions
    {
        /// <summary>
        ///    把NameValueCollection类型输出的格式： key1=value1&amp;key2=value2...
        /// </summary>
        /// <param name="Collections"></param>
        /// <returns>返回格式：key1=value1&amp;key2=value2...</returns>
        public static string ToUrlString(this NameValueCollection Collections)
        {
            string outStr = string.Empty;
            if (Collections == null || Collections.Count == 0)
                return outStr;
            foreach (string key in Collections.Keys)
            {
                outStr = string.Format("{0}{1}={2}&", outStr, key, Collections[key]);
            }
            if (!string.IsNullOrWhiteSpace(outStr) && outStr.EndsWith("&"))
            {
                outStr = outStr.Substring(0, outStr.Length - 1);
            }
            return outStr;
        }

        /// <summary>
        /// 把Dictionary&lt;string, object&gt;输出的格式： key1=value1&amp;key2=value2...
        /// </summary>
        /// <param name="Collections"></param>
        /// <returns>返回格式：key1=value1&amp;key2=value2...</returns>
        public static string ToUrlString(this Dictionary<string, object> Collections)
        {
            string outStr = string.Empty;
            if (Collections == null || Collections.Count == 0)
                return outStr;
            foreach (string key in Collections.Keys)
            {
                outStr += string.Format("{0}={1}&", key, Collections[key]);
            }
            if (!string.IsNullOrWhiteSpace(outStr) && outStr.EndsWith("&"))
            {
                outStr = outStr.Substring(0, outStr.Length - 1);
            }
            return outStr;
        }
    }
}
