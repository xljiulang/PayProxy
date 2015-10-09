using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace PayClient
{
    class Program
    {
        private static TcpClient client = new TcpClient();

        static void Main(string[] args)
        {
            Console.Title = "支付回调代理客户端";

            WriteLog("正在连接到支付回调代理服务 ..");
            var ipEndPoint = ConfigurationManager.AppSettings["PayProxyTcp"].Split(':');
            var host = ipEndPoint.FirstOrDefault();
            var port = int.Parse(ipEndPoint.LastOrDefault());
            var state = client.Connect(host, port).Result;
            WriteLog(string.Format("连接到支付回调代理服务{0} ..", state ? "成功" : "失败"));

            while (true)
            {
                Console.ReadLine();
            }
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="log"></param>
        private static void WriteLog(string log)
        {
            Console.ResetColor();
            Console.WriteLine(string.Format("{0} {1}", DateTime.Now.ToString("HH:mm:ss.fff"), log));
        }
    }
}
