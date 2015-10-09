using NetworkSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkSocket.Fast;
using System.Text.RegularExpressions;
using PayProxy.Http;

namespace PayProxy.Services
{
    /// <summary>
    /// Http监听服务
    /// </summary>
    public class HttpListener : TcpServerBase<SessionBase>
    {
        /// <summary>
        /// Tcp服务
        /// </summary>
        private TcpListener tcpListener = new TcpListener();

        /// <summary>
        /// 开始启动监听
        /// </summary>
        public void StartListen()
        {
            this.tcpListener.StartListen(AppConfig.TcpPort);
            base.StartListen(AppConfig.HttpURL.Port);
        }

        /// <summary>
        /// 创建新会话
        /// </summary>
        /// <returns></returns>
        protected override SessionBase OnCreateSession()
        {
            return new SessionBase();
        }

        /// <summary>
        /// 收到tcp请求
        /// </summary>
        /// <param name="session">会话</param>
        /// <param name="buffer">数据</param>
        protected override void OnReceive(SessionBase session, ReceiveBuffer buffer)
        {
            var request = HttpRequest.From(buffer);
            if (request == null)
            {
                return;
            }
            this.OnHttpRequest(session, request);
        }

        /// <summary>
        /// 收到Http请求
        /// </summary>
        /// <param name="session">会话</param>
        /// <param name="request">请求内容</param>
        private void OnHttpRequest(SessionBase session, HttpRequest request)
        {
            // 请求URL
            var requestURL = new Uri(string.Concat(AppConfig.HttpURL.Scheme, "://", AppConfig.HttpURL.Authority, request.Path));

            if (string.Equals(AppConfig.HttpURL.AbsolutePath, requestURL.AbsolutePath, StringComparison.OrdinalIgnoreCase))
            {
                var parameters = string.Empty;
                if (string.Equals(request.Method, "GET", StringComparison.OrdinalIgnoreCase))
                {
                    parameters = requestURL.Query.TrimStart('?');
                }
                else
                {
                    parameters = request.Body;
                }
                this.OnHttpURLRequest(parameters);
            }

            var response = new HttpResponse(session);
            response.EmptyResult();
        }

        /// <summary>
        /// 收到符合HttpURL的http请求
        /// </summary>
        /// <param name="parameters">参数</param>
        private void OnHttpURLRequest(string parameters)
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(string.Format("{0} 收到Http请求：{1}", DateTime.Now.ToString("HH:mm:ss.fff"), parameters));

            this.tcpListener.TryPushParametersAsync(parameters);
        }
    }
}
