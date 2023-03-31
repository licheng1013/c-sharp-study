using System.Reflection.Metadata.Ecma335;

namespace rocket_cat_c_sharp;

[ActionClass(Cmd = 1)]
public class UserAction
{
    [ActionMethod(SubCmd = 1)]
    public void Login()
    {
        Console.WriteLine("Login");
    }
    
    
    [ActionMethod(SubCmd = 2)]
    public void LoginOut()
    {
        Console.WriteLine("logout");
    }
}