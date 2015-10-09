using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace PayProxy
{
    /// <summary>
    /// 配置项
    /// </summary>
    public static class AppConfig
    {
        /// <summary>
        /// 当HttpURL收到Http请求时,将请求参数转发给tcp客户端
        /// </summary>
        public static Uri HttpURL
        {
            get
            {
                return new Uri(ConfigurationManager.AppSettings["HttpURL"]);
            }
        }

        /// <summary>
        /// Tcp服务器监听的端口
        /// </summary>
        public static int TcpPort
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["TcpPort"]);
            }
        }
    }
}
