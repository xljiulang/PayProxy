using NetworkSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkSocket.Http;
using System.Text.RegularExpressions;
using NetworkSocket.Fast;

namespace PayProxy.Services
{
    /// <summary>
    /// Http监听服务
    /// </summary>
    public class HttpListener : HttpServerBase
    {
        /// <summary>
        /// Tcp服务器
        /// </summary>
        private FastTcpServer tcpServer = new FastTcpServer();

        /// <summary>
        /// 开始启动监听
        /// </summary>
        public void StartListen()
        {
            this.tcpServer.StartListen(AppConfig.TcpPort);
            base.StartListen(AppConfig.HttpURL.Port);
        }

        /// <summary>
        /// 收到http请求
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        protected override void OnHttpRequest(HttpRequest request, HttpResponse response)
        {
            if (string.Equals(request.Path, AppConfig.HttpURL.AbsolutePath, StringComparison.OrdinalIgnoreCase))
            {
                var parameters = string.Join("&", request.Form, request.Query).Trim('&');
                this.TryPushParameters(parameters);
            }
            response.Write("OK");
        }

        /// <summary>
        /// 推送参数到所有客户端对象
        /// </summary>
        /// <param name="parameters">http请求参数</param>
        /// <returns></returns>
        private void TryPushParameters(string parameters)
        {
            foreach (var session in this.tcpServer.AllSessions)
            {
                try
                {
                    session.InvokeApi("OnHttpRequest", parameters);
                }
                catch (Exception) { }
            }
        }
    }
}
