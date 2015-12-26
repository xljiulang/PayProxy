using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PayProxy
{
    public static class AppConfig
    {
        /// <summary>
        /// 获取回调的URL
        /// </summary>
        public readonly static Uri LinstenURL = new Uri(ConfigurationManager.AppSettings["LinstenURL"]);

        /// <summary>
        /// 获取路径过滤
        /// </summary>
        public readonly static Regex PathPattern = new Regex(ConfigurationManager.AppSettings["PathPattern"], RegexOptions.IgnoreCase);
    }
}
