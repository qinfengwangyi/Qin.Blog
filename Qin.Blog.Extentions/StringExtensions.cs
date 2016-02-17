using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Qin.Blog.Extentions
{
    /// <summary>
    /// string的扩展类
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsIP(this string input)
        {
            return Regex.IsMatch(input, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        /// <summary>
        /// 检测是否符合email格式
        /// </summary>
        /// <param name="strEmail">要判断的email字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsValidEmail(this string strEmail)
        {
            return Regex.IsMatch(strEmail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        /// 检测是否是正确的Url
        /// </summary>
        /// <param name="strUrl">要验证的Url</param>
        /// <returns>判断结果</returns>
        public static bool IsURL(this string strUrl)
        {
            return Regex.IsMatch(strUrl, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
        }

        /// <summary>
        /// 删除字符串尾部的回车/换行/空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RTrim(this string str)
        {
            for (int i = str.Length; i >= 0; i--)
            {
                if (str[i].Equals(" ") || str[i].Equals("\r") || str[i].Equals("\n"))
                {
                    str.Remove(i, 1);
                }
            }
            return str;
        }

        /// <summary>
        /// 替换模板内容方法
        /// </summary>
        /// <typeparam name="T">替换的对象</typeparam>
        /// <param name="template">模板</param>
        /// <param name="t">对象</param>
        /// <returns></returns>
        public static string ReplaceTemplate<T>(this string template, T t)
        {
            if (string.IsNullOrEmpty(template))
            {
                return template;
            }

            var type = t.GetType();
            if (type == typeof(IDictionary) || t is IDictionary)
            {
                var dic = t as IDictionary;
                if (dic != null)
                {
                    foreach (var key in dic.Keys)
                    {
                        var value = dic[key];
                        template = template.Replace("{" + key + "}", value != null ? value.ToString() : "");
                    }
                }
            }
            else if (type.IsClass)
            {
                foreach (var propertyInfo in type.GetProperties())
                {
                    var value = propertyInfo.GetValue(t);
                    template = template.Replace("{" + propertyInfo.Name + "}", value != null ? value.ToString() : "");
                }
            }

            return template;
        }

        /// <summary>
        /// 移除HTML标签
        /// </summary>
        /// <param name="HTMLStr"></param>
        /// <returns></returns>
        public static string ParseTags(string HTMLStr)
        {
            return System.Text.RegularExpressions.Regex.Replace(HTMLStr, "<[^>]*>", "");
        }

        /// <summary>
        /// 滤除script引用和区块
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FilterScript(string str)
        {
            string pattern = @"<script[\s\S]+</script *>";
            return StripScriptAttributesFromTags(Regex.Replace(str, pattern, string.Empty, RegexOptions.IgnoreCase));
        }

        #region 过滤JAVASCRIPT相关私有方法

        /// <summary>
        /// 去除标签中的script属性
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string StripScriptAttributesFromTags(string str)
        {
            //整体去除
            string pattern = @"(?<ScriptAttr>on\w+=\s*(['""\s]?)([/s/S]*[^\1]*?)\1)[\s|>|/>]";
            Regex r = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (Match m in r.Matches(str))
            {
                string attrs = m.Groups["ScriptAttr"].Value;
                if (!string.IsNullOrEmpty(attrs))
                {
                    str = str.Replace(attrs, string.Empty);
                }
            }

            //滤除包含script的href
            str = FilterHrefScript(str);

            return str;
        }

        /// <summary>
        /// 滤除包含script的href
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string FilterHrefScript(string str)
        {
            //整体去除，不能去除不被单引号或双引号包含的属性值
            string regexstr = @" href[ ^=]*=\s*(['""\s]?)[\w]*script+?:([/s/S]*[^\1]*?)\1[\s]*";
            return Regex.Replace(str, regexstr, " ", RegexOptions.IgnoreCase);
        }
        #endregion
    }
}
