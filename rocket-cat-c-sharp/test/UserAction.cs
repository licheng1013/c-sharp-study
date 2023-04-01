using System.Reflection.Metadata.Ecma335;
using ProtoBuf;

namespace rocket_cat_c_sharp;

[ActionClass(Cmd = 1)]
public class UserAction
{
    [ActionMethod(SubCmd = 1)]
    public void Login(UserInfo userInfo)
    {
        Console.WriteLine("Login -> " + userInfo.Name);
    }
    
    
    [ActionMethod(SubCmd = 2)]
    public void LoginOut(Person user)
    {
        Console.WriteLine("logout");
    }
}


[ProtoContract]
public class Person {
    [ProtoMember(1)]
    public int Id {get;set;}
    [ProtoMember(2)]
    public string Name {get;set;}
}