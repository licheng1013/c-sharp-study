using System.Reflection.Metadata.Ecma335;

namespace rocket_cat_c_sharp;

[ActionClass(Cmd = 1)]
public class UserAction
{
    [ActionMethod(SubCmd = 1)]
    public void Login(UserInfo userInfo)
    {
        Console.WriteLine("Login -> " + userInfo.Name);
    }
    
    
    // [ActionMethod(SubCmd = 2)]
    // public void LoginOut(User user)
    // {
    //     Console.WriteLine("logout");
    // }
}