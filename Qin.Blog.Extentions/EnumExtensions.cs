using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Qin.Blog.Extentions
{
    /// <summary>
    /// 枚举扩展方法
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举的DescriptionAttribute属性
        /// </summary>
        /// <param name="enumeration"></param>
        /// <returns></returns>
        public static string ToDescriptionName(this Enum enumeration)
        {
            var type = enumeration.GetType();
            var memInfo = type.GetMember(enumeration.ToString());
            if (memInfo.Length > 0)
            {
                var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return enumeration.ToString();
        }

        /// <summary>
        /// 将枚举转换为字典
        /// </summary>
        /// <param name="enumeration"></param>
        /// <returns></returns>
        public static Dictionary<int, string> ToDictionaryDescription(this Enum enumeration)
        {
            var list = (from Enum d in Enum.GetValues(enumeration.GetType())
                        select new
                        {
                            ID = (int)Enum.Parse(enumeration.GetType(), Enum.GetName(enumeration.GetType(), d))
                            ,
                            Name = d.ToDescriptionName()
                        }
                       ).ToList();
            return list.ToDictionary(c => c.ID, c => c.Name);
        }

        public static Dictionary<int, string> ToDictionary(this Enum enumeration)
        {
            var list = (from Enum d in Enum.GetValues(enumeration.GetType())
                        select new
                        {
                            ID = (int)Enum.Parse(enumeration.GetType(), Enum.GetName(enumeration.GetType(), d)),
                            Name = d.ToString()
                        }
                     ).ToList();
            return list.ToDictionary(c => c.ID, c => c.Name);
        }

        public static List<SelectListItem> ToSelectList(this Enum enumeration)
        {
            var list = (from Enum d in Enum.GetValues(enumeration.GetType())
                        select new SelectListItem
                        {
                            Value = ((int)Enum.Parse(enumeration.GetType(), Enum.GetName(enumeration.GetType(), d))).ToString(),
                            Text = d.ToDescriptionName()
                        }
                     ).ToList();
            return list;
        }

        public static List<string> ToStringList(this Enum enumeration)
        {
            var list = (from Enum d in Enum.GetValues(enumeration.GetType())
                        select d.ToString()).ToList();
            return list;
        }

        public static List<SelectListItem> ToSelectList(this Enum enumeration, string defaultText, string defaultValue)
        {
            var list = (from Enum d in Enum.GetValues(enumeration.GetType())
                        select new SelectListItem
                        {
                            Value = ((int)Enum.Parse(enumeration.GetType(), Enum.GetName(enumeration.GetType(), d))).ToString(),
                            Text = d.ToDescriptionName()
                        }
                     ).ToList();
            list.Insert(0, new SelectListItem { Text = defaultText, Value = defaultValue });
            return list;
        }

        public static int GetMax(this Enum enumeration)
        {
            return Enum.GetValues(enumeration.GetType()).Cast<int>().Max();
        }
    }
}
