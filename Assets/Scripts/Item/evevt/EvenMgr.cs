using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EvenMgr : BaseManager<EvenMgr>
{
    private basicMgr bm = basicMgr.GetInstance();
    public static Event_UI eu = DataMgr.GetInstance().本地对象["UI"].transform.Find("战斗页/事件界面").GetComponent<Event_UI>();
    public  Dictionary<string, Action> 事件表= 加载事件表();



    public static Dictionary<string, Action> 加载事件表() {
        Dictionary<string, Action> 事件表 = new Dictionary<string, Action>();
        事件表.Add("三年之约", EvenMgr.eu.三年之约_UI);
        事件表.Add("山海小师弟", EvenMgr.eu.山海小师弟_UI);
        事件表.Add("获得宠物", EvenMgr.eu.获得宠物_UI);
        事件表.Add("开启职业", EvenMgr.eu.开启职业_UI);

        return 事件表;
    }


    public void 事件模板(string str,UnityAction 事件,GameObject 内容父物体)
    {
        GameObject 事件实例;
        GameObject 事件模板 = Resources.Load<GameObject>("活动/事件文字模板");
        事件实例 =GameObject.Instantiate(事件模板);
        事件实例.transform.Find("文本/标题/Text").GetComponent<Text>().text = str;
        事件实例.transform.SetParent(内容父物体.transform, false);
        GameObject 按钮 = 事件实例.transform.Find("button/前往").gameObject;
        bm.Banding(按钮, 事件);
    }


    public void 事件调用(string 事件名) {
        事件表[事件名]();
    }


    public void 存档添加事件(string 事件名字,string 剩余时间) {
        G_Util gut = NameMgr.画布.GetComponent<G_Util>();
        gut.生成事件框();
        role_Data myData = NameMgr.IO.load();
        myData.事件表.Add(事件名字, 剩余时间);
        NameMgr.IO.save(myData);

        //UI
        控制事件图标的显隐(true);
    }

    public void 存档移除事件(string 事件名字)
    {
        role_Data myData = NameMgr.IO.load();
        if (myData.事件表.ContainsKey(事件名字))
        {
            myData.事件表.Remove(事件名字);
            NameMgr.IO.save(myData);

            //UI
            if (myData.事件表.Count == 0) {
                控制事件图标的显隐(false);
            }
        }
       
    }


    public void 控制事件图标的显隐(bool isActive) {
        DataMgr.GetInstance().本地对象["UI"].transform.Find("战斗页/event").gameObject.SetActive(isActive);
    }
}
