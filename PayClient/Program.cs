using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace PayClient
{
    class Program
    {
        private static ProxyClient client = new ProxyClient();

        static void Main(string[] args)
        {
            Console.Title = "支付回调代理客户端";

            WriteLog("正在连接到支付回调代理服务 ..");
            var hostPort = ConfigurationManager.AppSettings["PayProxy"].Split(':');
            var host = hostPort.FirstOrDefault();
            var port = int.Parse(hostPort.LastOrDefault());

            var state = client.Connect(host, port).Result;
            WriteLog(string.Format("连接到支付回调代理服务{0} ..", state ? "成功" : "失败"));

            var version = client.InvokeApi<string>("GetVersion").Result;
            WriteLog(string.Format("服务器通讯库版本号：V{0}", version));

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
            Console.WriteLine(string.Format("{0} {1}", DateTime.Now.ToString("HH:mm:ss.fff"), log));
        }
    }
}
