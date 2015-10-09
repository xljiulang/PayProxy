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
    /// Tcp客户端
    /// </summary>
    public class TcpClient : FastTcpClient
    {
        /// <summary>
        /// http请求接口
        /// </summary>
        /// <param name="parameters">请求参数</param>
        [Api]
        public void OnHttpRequest(string parameters)
        {
            this.WriteLog("收到Http参数：" + parameters);
            try
            {
                var address = ConfigurationManager.AppSettings["ForwardURL"];
                var httpClient = new WebClient();
                httpClient.Headers.Add(HttpRequestHeader.ContentType, "pplication/x-www-form-urlencoded");
                httpClient.UploadData(address, "post", Encoding.UTF8.GetBytes(parameters));
                this.WriteLog("推送成功：" + address);
            }
            catch (Exception ex)
            {
                this.WriteLog("推送失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="log"></param>
        private void WriteLog(string log)
        {
            Console.ResetColor();
            Console.WriteLine(string.Format("{0} {1}", DateTime.Now.ToString("HH:mm:ss.fff"), log));
        }
    }
}
