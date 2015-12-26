using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkSocket;
using System.Text.RegularExpressions;
using NetworkSocket.Fast;


namespace PayProxy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "支付回调代理服务";

            var listener = new TcpListener();
            listener.Use<FastMiddleware>();
            listener.Use<HttpProxyMiddleware>();
            listener.Start(AppConfig.LinstenURL.Port);

            Console.WriteLine("支付回调代理服务启动完成 ..");
            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}
