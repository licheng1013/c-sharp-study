using System.Reflection;

namespace rocket_cat_c_sharp;

public class ScanTool
{
    // 保存所有反射实例,主路由命令为key,实例为value
    private static readonly Dictionary<int, object> CacheMap = new();
    // 保存所有反射方法,路由命令为key,方法为value
    private static readonly Dictionary<int, MethodInfo> MethodMap = new();
    // 保存参数类型 MethodMap.key = Type类型
    private static readonly Dictionary<int, Type> ParamTypeMap = new();

    // 测试
    public static void Run()
    {
        const int cmd = 1;
        const int subCmd = 1;
        // 扫描所有类和方法
        ScanMethod();
        // 执行方法
        MethodMap[RouterUtil.GetMergeCmd(cmd,subCmd)].Invoke(CacheMap[cmd], null);
        MethodMap[RouterUtil.GetMergeCmd(cmd,2)].Invoke(CacheMap[cmd], null);
    }

    // 扫描所有类并判断是否存在 Action 注解, 然后扫描所有方法并判断是否存在 Action 注解，最后执行方法
    private static void ScanMethod()
    {
        var types = new List<Type> { new UserAction().GetType() };
        // 扫描所有类
        foreach (var type in types)
        {
            // 判断是否存在 Action 注解
            var cls = type.GetCustomAttribute<ActionClass>();
            if (cls == null) continue;
            // 不存在则创建实例
            if (!CacheMap.ContainsKey(cls.Cmd))
            {
                var obj = Activator.CreateInstance(type);   // 创建类
                if (obj == null) continue;
                CacheMap.Add(cls.Cmd, obj);
            }
            // 扫描所有方法
            foreach (var method in type.GetMethods())
            {
                // 判断是否存在 Action 注解
                var sub = method.GetCustomAttribute<ActionMethod>();
                if (sub == null) continue;
                // 创建类
                // 执行方法
                MethodMap.Add(RouterUtil.GetMergeCmd(cls.Cmd,sub.SubCmd), method);
            }
        }
    }
}

// 定义一个方法和类上面的注解
[AttributeUsage(AttributeTargets.Class)]
public class ActionClass : Attribute
{
    public int Cmd { get; set; }
}

// 定义一个方法和类上面的注解
[AttributeUsage(AttributeTargets.Method)]
public class ActionMethod : Attribute
{
    public int SubCmd { get; set; }
}