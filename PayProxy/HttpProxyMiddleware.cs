using NetworkSocket;
using NetworkSocket.Fast;
using NetworkSocket.Http;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace PayProxy
{
    /// <summary>
    /// 代码http中间件
    /// </summary>
    public class HttpProxyMiddleware : HttpMiddlewareBase
    {
        /// <summary>
        /// 收到http请求
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="requestContext">请求上下文</param>
        protected override void OnHttpRequest(IContenxt context, NetworkSocket.Http.RequestContext requestContext)
        {
            var request = requestContext.Request;
            var response = requestContext.Response;

            if (AppConfig.PathPattern.IsMatch(request.Path) == false)
            {
                var message = "路径不匹配，数据未转发 ..";
                response.Write(message);
                Console.WriteLine(message);
            }
            else
            {
                var fastSessions = context.AllSessions.FilterWrappers<FastSession>().ToArray();
                foreach (var session in fastSessions)
                {
                    this.PushRequest(session, request);
                }

                var message = string.Format("数据已转发到{0}到客户端 ..", fastSessions.Length);
                response.Write(message);
                Console.WriteLine(message);
            }
        }

        /// <summary>
        /// 推送请求内容到客户端
        /// </summary>
        /// <param name="session">会话</param>
        /// <param name="request">请求</param>
        /// <returns></returns>
        private bool PushRequest(FastSession session, HttpRequest request)
        {
            try
            {
                session.InvokeApi("OnHttpRequest", request.Url.PathAndQuery, request.Body);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
