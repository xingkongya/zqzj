using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test
{
    static void Main(string[] args)//Main函数方法
    {
        List<int> i = new List<int>();
        i.Add(1);
        i.Add(2);
        Debug.Log(i);
        change(i);
    }

    public static void change(List<int> i)
    {
        Debug.Log(i);
    }
}
