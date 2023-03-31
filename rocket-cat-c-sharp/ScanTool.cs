﻿using System.Reflection;
using System.Text.Json;

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
        // 扫描所有类和方法
        ScanMethod(new UserAction());
        // 定义User
        var user = new User { Name = "张三", Age = 18 };
        // 序列化
        var data = JsonSerializer.SerializeToUtf8Bytes(user);
        // 执行方法
        InvokeMethod(RouterUtil.GetMergeCmd(1, 1), data);
        InvokeMethod(RouterUtil.GetMergeCmd(1, 2));
    }

    // 执行方法
    // 如果出现 JsonReaderException 异常, 请检查是否有参数类型不匹配
    private static void InvokeMethod(int mergeCmd, byte[]? data = null)
    {
        // 拆分
        var cmd = RouterUtil.GetCmd(mergeCmd);
        var subCmd = RouterUtil.GetSubCmd(mergeCmd);

        if (ParamTypeMap.TryGetValue(mergeCmd, out var value))
        {
            // 反序列化
            var param = JsonSerializer.Deserialize(data, value);
            // 执行方法
            MethodMap[RouterUtil.GetMergeCmd(cmd, subCmd)].Invoke(CacheMap[cmd], new[] { param });
            return;
        }

        // 执行方法
        MethodMap[RouterUtil.GetMergeCmd(cmd, subCmd)].Invoke(CacheMap[cmd], null);
    }


    // 扫描所有类并判断是否存在 Action 注解, 然后扫描所有方法并判断是否存在 Action 注解，最后执行方法
    private static void ScanMethod(params object[] objects)
    {
        var types = objects.Select(o => o.GetType()).ToList();
        // 扫描所有类
        foreach (var type in types)
        {
            // 判断是否存在 Action 注解
            var cls = type.GetCustomAttribute<ActionClass>();
            if (cls == null) continue;
            // 不存在则创建实例
            if (!CacheMap.ContainsKey(cls.Cmd))
            {
                var obj = Activator.CreateInstance(type); // 创建类
                if (obj == null) continue;
                CacheMap.Add(cls.Cmd, obj);
            }

            foreach (var method in type.GetMethods()) // 扫描所有方法
            {
                var sub = method.GetCustomAttribute<ActionMethod>(); // 判断是否存在 Action 注解
                if (sub == null) continue;
                var mergeCmd = RouterUtil.GetMergeCmd(cls.Cmd, sub.SubCmd);
                // 获取方法参数类型
                if (method.GetParameters().Length != 0)
                {
                    var paramType = method.GetParameters()[0].ParameterType;
                    // 打印类型
                    ParamTypeMap.Add(mergeCmd, paramType);
                }

                MethodMap.Add(mergeCmd, method); // 添加方法进去
            }
        }
    }
}