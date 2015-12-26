using NetworkSocket.Core;
using NetworkSocket.Fast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayProxy
{
    public class FastService : FastApiService
    {
        /// <summary>
        /// 获取服务组件版本号
        /// </summary>       
        /// <returns></returns>
        [Api]        
        public string GetVersion()
        {
            return typeof(FastApiService).Assembly.GetName().Version.ToString();
        }
    }
}
