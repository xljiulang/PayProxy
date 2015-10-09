using NetworkSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayProxy.Http
{
    /// <summary>
    /// Http回复
    /// </summary>
    public class HttpResponse
    {
        /// <summary>
        /// 会话
        /// </summary>
        private SessionBase session;

        /// <summary>
        /// 表示http回复
        /// </summary>
        /// <param name="session">会话</param>
        public HttpResponse(SessionBase session)
        {
            this.session = session;
        }

        /// <summary>
        /// 输出结果
        /// </summary>
        /// <param name="content">内容</param>
        public void ContentResult(string content)
        {
            var head = @"HTTP/1.1 200 OK
Cache-Control: private
Content-Type: text/html; charset=utf-8
Vary: Accept-Encoding
Server: Microsoft-IIS/7.5
X-AspNetMvc-Version: 5.0
X-AspNet-Version: 4.0.30319
X-Powered-By: ASP.NET
Date: Fri, 18 Sep 2015 05:39:50 GMT
Content-Length: 12003

";
            var response = head + content;
            var bytes = new ByteRange(Encoding.UTF8.GetBytes(response));
            session.Send(bytes);
            session.Close();
        }

        /// <summary>
        /// 输出空结果
        /// </summary>
        public void EmptyResult()
        {
            this.ContentResult(null);
        }
    }
}
