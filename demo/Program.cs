// 一个简单的反射例子

using System.Buffers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using ProtoBuf;
using rocket_cat_c_sharp;

namespace demo
{
    internal static class Program
    {
        
        // 测试
        public static void Main()
        {
            ScanTool.Encoder = new JsonEncoder();
            // 扫描所有类和方法
            ScanTool.ScanMethod(new UserAction());
            // 定义User
            var user = new UserInfo { Name = "张三", Age = 18 };
            // 序列化
            var data = JsonSerializer.SerializeToUtf8Bytes(user);
            // 执行方法
            ScanTool.InvokeMethod(RouterUtil.GetMergeCmd(1, 1), data);
        }

        public static void RunProto()
        {
            ScanTool.Encoder = new ProtoEncoder();
            // 扫描所有类和方法
            ScanTool.ScanMethod(new UserAction());
            // 定义User
            var user = new Person() { Name = "张三", Id = 18 };
            // 序列化
            IBufferWriter<byte> data = new ArrayBufferWriter<byte>();
            Serializer.Serialize(data, user);
            // 打印data大小
            Console.WriteLine(data.GetMemory().Length);
            // 执行方法
            //InvokeMethod(RouterUtil.GetMergeCmd(1, 2), data.GetMemory().ToArray());
        }
    }
    

    

    
}