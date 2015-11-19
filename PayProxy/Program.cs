using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkSocket;
using System.Text.RegularExpressions;
using PayProxy.Services;


namespace PayProxy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "支付回调代理服务";

            WriteLog("正在启动服务 ..");
            var http = new HttpListener();
            http.StartListen();
            WriteLog("支付回调代理服务启动完成 ..");
            Console.WriteLine();

            while (true)
            {
                WriteLog("按任意键触发一个http模拟请求 ..");
                Console.ReadKey();

                WriteLog("正在模拟Http请求 ..");
                var httpClient = new System.Net.WebClient();
                httpClient.Headers.Add(System.Net.HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
                var postString = "type=test&time=" + DateTime.Now.ToString(@"yyyy\/MM\/dd HH:mm:ss");

                var bytes = httpClient.UploadData(AppConfig.HttpURL, "post", Encoding.UTF8.GetBytes(postString));
                var response = Encoding.UTF8.GetString(bytes);

                WriteLog("服务器收到参数：" + response);
                WriteLog("请检查tcp客户端是否收到模拟请求的参数 ..");
            }
        }

        static void WriteLog(string log)
        {
            Console.ResetColor();
            Console.WriteLine(string.Format("{0} {1}", DateTime.Now.ToString("HH:mm:ss.fff"), log));
        }
    }
}
