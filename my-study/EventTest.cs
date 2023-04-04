namespace my_study;

// 订阅事件学习
public class SyncManager
{

    public delegate void TickFrame(uint a, User b);
    public event TickFrame Callback = null!; 
    public static void Main()
    {
        var manager = new SyncManager();
        manager.Callback += (u, user) =>
        {
            Console.WriteLine("Tick1 -> " + user.Name);
        };
        
        manager.Callback += (u, user) =>
        {
            Console.WriteLine("Tick2 -> " + user.Name);
        };
        manager.Callback.Invoke(1, new User() {Name = "test"});
        
        
        var count = 0;
        
        // 每秒20次调用
        var timer = new System.Timers.Timer(50D);
        timer.Elapsed += (_, _) =>
        {
            count++;
            manager.Callback.Invoke(1, new User() {Name = "test"});
        };
        timer.Start();

        while (count <= 120)
        {
            Console.WriteLine("执行次数 -> " + count);
        }
    }
    
}

public  class User
{
    // 名称
    public string Name { get; set; }
    // 年龄
    public int Age { get; set; }
    
}