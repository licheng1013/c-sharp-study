using System.Reflection.Metadata.Ecma335;

namespace rocket_cat_c_sharp;

[Action(Cmd = 1)]
public class UserAction
{

    [Action(SubCmd = 1)]
    public void Login()
    {
        Console.WriteLine("Login");
    }
    
}