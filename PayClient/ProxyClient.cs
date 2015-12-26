using NetworkSocket.Core;
using NetworkSocket.Fast;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;

namespace PayClient
{
    /// <summary>
    /// 代理客户端
    /// </summary>
    public class ProxyClient : FastTcpClient
    {
        /// <summary>
        /// http请求接口
        /// </summary>
        /// <param name="pathAndQuery">请求路径和参数</param>
        /// <param name="body">请求表单内容</param>
        [Api]
        public void OnHttpRequest(string pathAndQuery, byte[] body)
        {
            var localURL = new Uri(ConfigurationManager.AppSettings["LocalURL"]);
            var targetUrl = string.Format("{0}://{1}{2}", localURL.Scheme, localURL.Authority, pathAndQuery);
            this.WriteLog("转发数据到本地路径：" + targetUrl);
           
            try
            {
                var httpClient = new WebClient();
                httpClient.Headers.Add(HttpRequestHeader.ContentType, "pplication/x-www-form-urlencoded");
                var response = httpClient.UploadData(targetUrl, "post", body);
                this.WriteLog("转发成功：" + Encoding.UTF8.GetString(response));
            }
            catch (Exception ex)
            {
                this.WriteLog("转发失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="log"></param>
        private void WriteLog(string log)
        {
            Console.WriteLine(string.Format("{0} {1}", DateTime.Now.ToString("HH:mm:ss.fff"), log));
        }
    }
}
