using System.Text.Json;

namespace rocket_cat_c_sharp;

public class LanguageManager
{
    // 获取实例
    public static LanguageManager Instance { get; } = new();

    // 设置一个语言map
    private readonly Dictionary<LanguageEnum, Dictionary<string, string>> _languageMap = new();

    // 闭包函数列表
    private readonly List<Action<Dictionary<string, string>>> _changeLanguageActionList = new();

    // 设置一个默认语言
    private LanguageEnum _language = LanguageEnum.Chinese;

    // 添加语言
    private void AddLanguage(LanguageEnum language, Dictionary<string, string> languageMap)
    {
        // 替换语言
        _languageMap[language] = languageMap;
    }

    // 修改语言
    public void ChangeLanguage(LanguageEnum language)
    {
        // 语言存在则修改
        if (_languageMap.ContainsKey(language))
            _language = language;
        foreach (var action in _changeLanguageActionList)
        {
            action(_languageMap[_language]);
        }
    }

    // 加入一个闭包函数使用语言
    public void UseLanguage(Action<Dictionary<string, string>> action)
    {
        _changeLanguageActionList.Add(action);
        action(_languageMap[_language]);
    }

    // 读取json文件
    private static Dictionary<string, string>? LoadJson(string path)
    {
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<Dictionary<string, string>>(json);
    }

    
    // 读取特定语言的json文件
    public void LoadProperties(LanguageEnum language, string path)
    {
        var text = File.ReadAllText(path);
        var map = ParseProperties(text);
        AddLanguage(language, map);
    }
    
    // 读取特定语言的json文件
    public void LoadJson(LanguageEnum language, string path)
    {
        var map = LoadJson(path);
        if (map == null)
        {
            Console.WriteLine("读取失败");
            return;
        }
        AddLanguage(language, map);
    }
    
    
    // 写一个解析 properties 文件转换为 map 的方法
    private static Dictionary<string, string> ParseProperties(string text)
    {
        var map = new Dictionary<string, string>();
        var lines = text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in lines)
        {
            var index = line.IndexOf('=');
            if (index == -1)
                continue;
            var key = line[..index];
            var value = line[(index + 1)..];
            map.Add(key, value);
        }
        return map;
    }

}