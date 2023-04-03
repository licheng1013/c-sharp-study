namespace rocket_cat_c_sharp;

public class LanguageManager
{
    // 获取实例
    public static LanguageManager Instance { get; } = new();
    // 设置一个语言map
    private readonly Dictionary<LanguageEnum, Dictionary<string, string>> _languageMap = new();
    // 闭包函数列表
    private readonly List<Action<Dictionary<string,string>>> _changeLanguageActionList = new();
    // 设置一个默认语言
    private LanguageEnum _language = LanguageEnum.Chinese;
    // 添加语言
    public void AddLanguage(LanguageEnum language, Dictionary<string, string> languageMap)
    {
        _languageMap.Add(language, languageMap);
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
    public void UseLanguage(Action<Dictionary<string,string>> action)
    {
        _changeLanguageActionList.Add(action);
        action(_languageMap[_language]);
    }
}