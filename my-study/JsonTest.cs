using my_study;
using Newtonsoft.Json;

public class JsonTest{
    public static void test(){
        Console.WriteLine("HelloWorld");
        var myTest = new MyTest{
            playerEnum = PlayerEnum.p1,
            skillEnum = SkillEnum.s技能1
        };
        var json = JsonConvert.SerializeObject(myTest);
        Console.WriteLine(json);
        var myTest2 = JsonConvert.DeserializeObject<MyTest>(json);
        Console.WriteLine(myTest2);
        
    }
}


public class MyTest{
    public PlayerEnum playerEnum;
    public SkillEnum skillEnum;
}
