using System.Reflection;

namespace rocket_cat_c_sharp;

public class ScanTool
{
    // 扫描所有类并判断是否存在 Action 注解, 然后扫描所有方法并判断是否存在 Action 注解，最后执行方法
    public static void ScanMethod()
    {
        // 扫描所有类
        foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
        {
            // 判断是否存在 Action 注解
            var typeAttribute = type.GetCustomAttribute<Action>();
            if (typeAttribute != null)
            {
                // 扫描所有方法
                foreach (var method in type.GetMethods())
                {
                    // 判断是否存在 Action 注解
                    var methodAttribute = method.GetCustomAttribute<Action>();
                    if (methodAttribute != null)
                    {
                        // 创建类
                        var obj = Activator.CreateInstance(type);
                        // 执行方法
                        method.Invoke(obj, null);
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