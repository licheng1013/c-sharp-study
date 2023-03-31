namespace rocket_cat_c_sharp;

// 类注解
[AttributeUsage(AttributeTargets.Class)]
public class ActionClass : Attribute
{
    public int Cmd { get; set; }
}