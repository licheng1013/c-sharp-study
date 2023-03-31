namespace rocket_cat_c_sharp;

// 方法注解
[AttributeUsage(AttributeTargets.Method)]
public class ActionMethod : Attribute
{
    public int SubCmd { get; set; }
}