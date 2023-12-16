using System;
using System.Net.Http.Headers;

namespace my_study;

public class TestUtil
{
    public static void Test()
    {
       switch (0) {
           case 0:
               break;
           case 1:
               break;
           case 2:
               break;
           default:
               throw new Exception($"未知类型 {0}");
               break;
       }

    }
}