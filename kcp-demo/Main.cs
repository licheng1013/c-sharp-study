using System.Buffers;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets.Kcp;
using System.Net.Sockets.Kcp.Simple;
using System.Text;

namespace kcp_demo;

internal abstract class Program
{
    private static void Main(string[] args)
    {
        // 使用kcp建立连接
        var kcp = new SimpleKcpClient(50001, new IPEndPoint(IPAddress.Loopback, 12355));
        kcp.kcp.TraceListener = new ConsoleTraceListener();
        Task.Run(async () =>
        {
            while (true)
            {
                kcp.kcp.Update(DateTimeOffset.UtcNow);
                await Task.Delay(10);
                var resp = await kcp.ReceiveAsync();
                var rest = Encoding.UTF8.GetString(resp);
                Console.WriteLine($"收到服务器回复:    {rest}");
            }
        });
        while (true)
        {
            Send(kcp, "发送一条消息");
            // 休眠一下，让kcp有时间发送
            Thread.Sleep(3000);
            // 发送消息打印
            Console.WriteLine($"发送消息:   发送一条消息");
        }
    }

    static void Send(SimpleKcpClient client, string v)
    {
        var buffer = Encoding.UTF8.GetBytes(v);
        client.SendAsync(buffer, buffer.Length);
    }
}