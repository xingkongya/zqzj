using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class suit_Data : Suit_Interface
{
    public string s_id;
    public string s_name;
    public string s_qua;
    public string s_total;
    public string s_index;
    public string s_two;
    public string s_three;
    public string s_four;
    public string s_five;
    public string s_six;
    public string s_seven;
    public static Dictionary<string, string> 属性 = new Dictionary<string, string>();
    basicMgr bm = basicMgr.GetInstance();
    char 字符 = '+';
    Dictionary<int, Action> 事件列表 = new Dictionary<int, Action>();


    public suit_Data() {

    }

    public suit_Data (string s_id,string s_name, string s_qua, string s_total, string s_index, string s_two, string s_three, string s_four, string s_five , string s_six, string s_seven){
         this.s_id = s_id;
         this.s_name = s_name;
         this.s_qua = s_qua;
         this.s_total = s_total;
         this.s_index = s_index;
         this.s_two = s_two;
         this.s_three = s_three;
         this.s_four = s_four;
         this.s_six = s_six;
         this.s_seven = s_seven;
    }


    public void 七件套()
    {

         字符串数组变为属性(bm.字符串分割(s_seven,字符 ));
    }

    public void 三件套()
    {
         字符串数组变为属性(bm.字符串分割(s_three, 字符));
    }

    public void 二件套()
    {
        字符串数组变为属性(bm.字符串分割(s_two, 字符));
    }

    public void 五件套()
    {
         字符串数组变为属性(bm.字符串分割(s_five, 字符));
    }

    public void 六件套()
    {
         字符串数组变为属性(bm.字符串分割(s_six, 字符));
    }

    public void 四件套()
    {
         字符串数组变为属性(bm.字符串分割(s_four, 字符));
    }

    public Dictionary<string, string> 激活套装()
    {
        属性.Clear();
        if (事件列表.Count == 0)
        {
            事件列表.Add(2, 二件套);
            事件列表.Add(3, 三件套);
            事件列表.Add(4, 四件套);
            事件列表.Add(5, 五件套);
            事件列表.Add(6, 六件套);
            事件列表.Add(7, 七件套);
        }

        if (int.Parse( s_index) <= int.Parse( s_total)&&int.Parse( s_index)>=2) {
            for (int i=2;i<=int.Parse( s_index);i++) {
                事件列表[i].Invoke();
            }
        }

        return 属性;
    }

    public void 字符串数组变为属性(string [] 字符组) {
        if (字符组 == null)
            return ;
        if (属性.ContainsKey(字符组[0]))
            属性[字符组[0]] += int.Parse(字符组[1]);
        else
            属性.Add(字符组[0], bm.Xor(字符组[1]));

    }


}
