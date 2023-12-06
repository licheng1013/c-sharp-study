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
    
    /// 技能消耗
    public int cost;
    
    
}



[Serializable]
public  class UserA
{
    // 名称
    public string Name;
    // 年龄
    public int Age;

}

public enum SkillEnum
{
    s普攻,
    s技能1,
    s技能2,
    s技能3,
    s技能4,
    s技能5,
    s技能6,
}


public enum PlayerEnum
{
    p1,
    p2,
    p3,
    p4,
}