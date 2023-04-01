using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading;

namespace KcpProject.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new UDPSession
            {
                AckNoDelay = true,
                WriteDelay = false
            };

            connection.Connect("127.0.0.1", 12355);
            var counter = 0;
            var text = Encoding.UTF8.GetBytes($"Hello KCP: {++counter}");
            // 子线程接收数据
            var thread = new Thread(() =>
            {
                // 字节大小
                var bytes = new byte[4096];
                while (true)
                {
                    var n = connection.Recv(bytes, 0, bytes.Length);
                    if (n == 0)
                        continue;
                    if (n < 0)
                    {
                        Console.WriteLine("读取消息失败.");
                        break;
                    }
                    var resp = Encoding.UTF8.GetString(bytes, 0, n);
                    Console.WriteLine("收到消息: " + resp);
                }
            });
            thread.Start();
            while (true) {
                Thread.Sleep(1000);
                connection.Update();
                Console.WriteLine(Encoding.UTF8.GetString(text, 0, text.Length));
                var sent = connection.Send(text, 0, text.Length);
                if (sent < 0) {
                    Console.WriteLine("写入消息失败.");
                    break;
                }
            }
        }
    }
}
