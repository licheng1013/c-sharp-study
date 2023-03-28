// 一个简单的反射例子

using System.Reflection;
using rocket_cat_c_sharp;
using Action = rocket_cat_c_sharp.Action;

namespace demo
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            // 反射获取MyClass类上面的注解
            var myClass = new MyClass();
            var myClassType = myClass.GetType();
            var myClassAttribute = myClassType.GetCustomAttribute<Action>();
            Console.WriteLine(myClassAttribute.Cmd);
            // 反射获取MyMethod方法上面的注解
            var myMethod = myClassType.GetMethod("MyMethod");
            var myMethodAttribute = myMethod.GetCustomAttribute<Action>();
            Console.WriteLine(myMethodAttribute.Cmd);
            // 开始时间
            var startTime = DateTime.Now;
            // 反射执行MyMethod方法
            for (int i = 0; i < 60; i++)
            {
                myMethod.Invoke(myClass, null);
               //myClass.MyMethod();
            }
            // 结束时间
            var endTime = DateTime.Now;
            // 输出毫秒
            Console.WriteLine((endTime - startTime).TotalMilliseconds);
        }
    }
    

    
    // 定义一个类
    [Action(Cmd = 1)]
    public class MyClass
    {
        // 定义一个方法
        [Action(SubCmd = 2)]
        public void MyMethod()
        {
           // Console.WriteLine("MyMethod");
        }
    }
    
}