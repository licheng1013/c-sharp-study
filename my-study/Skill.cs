namespace my_study;

[Serializable]
public class Skill
{
    /// 名称 
    public string name;
    
    /// 描述 
    public string desc;
    
    /// 是否延迟技能
    public bool isDelay;
    
    /// 是否蓄力技能
    public bool isCharge;
    
    /// 技能倍率
    public float rate;
    
    /// 技能冷却时间
    public float cd;
    
    
}



[Serializable]
public  class UserA
{
    // 名称
    public string Name;
    // 年龄
    public int Age;

}


