using System.Reflection;

namespace rocket_cat_c_sharp;

public class ScanTool
{   
    // 保存所有反射实例
    private static readonly Dictionary<int, object> cacheMap = new();
    // 保存所有反射方法
    private static readonly Dictionary<int, MethodInfo> methodMap = new();
    
    
    // 另一个执行程序
    public static void Run()
    {
        // 扫描所有类和方法
        ScanMethod();
        // 执行方法
        methodMap[1].Invoke(cacheMap[1], null);
    }


    // 扫描所有类并判断是否存在 Action 注解, 然后扫描所有方法并判断是否存在 Action 注解，最后执行方法
    private static void ScanMethod()
    {

        var types = new List<Type> { new UserAction().GetType() };

        // 扫描所有类
        foreach (var type in types)
        {
            // 判断是否存在 Action 注解
            var classAttribute = type.GetCustomAttribute<Action>();
            if (classAttribute != null)
            {
                // 扫描所有方法
                foreach (var method in type.GetMethods())
                {
                    // 判断是否存在 Action 注解
                    var methodAttribute = method.GetCustomAttribute<Action>();
                    if (methodAttribute != null)
                    {
                        // 打印 subCmd
                        Console.WriteLine(methodAttribute.SubCmd);

                        // 不存在则创建实例
                        if (!cacheMap.ContainsKey(classAttribute.Cmd))
                        {
                            // 创建类
                            var obj = Activator.CreateInstance(type);
                            if (obj!=null)
                            {
                                // 保存实例
                                cacheMap.Add(classAttribute.Cmd, obj);
                            }
                        }
                        // 创建类
                        // 执行方法
                        methodMap.Add(methodAttribute.SubCmd, method);
                    }
                }
            }
        }
    }
    
    
}

// 定义一个方法和类上面的注解
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class Action : Attribute
{
    public int Cmd { get; set; }
    public int SubCmd { get; set; }
}