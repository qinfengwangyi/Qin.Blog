using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Blog.Extentions
{
    public static class GuidExtensions
    {
        public static string ToId(this Guid Guid)
        {

            return Guid.ToString().Replace("-", string.Empty);
        }
    }
}
