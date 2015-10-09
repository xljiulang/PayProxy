using NetworkSocket.Fast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayProxy.Services
{
    /// <summary>
    /// 推送服务器
    /// </summary>
    public class TcpListener : FastTcpServer
    {
        /// <summary>
        /// 推送参数到所有客户端对象
        /// </summary>
        /// <param name="parameters">http请求参数</param>
        /// <returns></returns>
        public bool TryPushParametersAsync(string parameters)
        {
            return this.AllSessions.Select(item =>
            {
                try
                {
                    item.InvokeApi("OnHttpRequest", parameters);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }).All(item => item);
        }
    }
}
