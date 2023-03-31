namespace rocket_cat_c_sharp;

public class RouterUtil
{
    //---------------------------路由命令处理
    private static int GetCmd(int merge) {//获取cmd
        return merge >> 16;
    }
    private static int GetSubCmd(int merge) {//获取subCmd
        return merge & 0xFFFF;
    }
    public static int GetMergeCmd(int cmd, int subCmd) {  //获取mergeCmd
        return (cmd << 16) + subCmd;
    }
}