using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_UI : MonoBehaviour
{
    private basicMgr bm;
    GameObject 事件实例;
    private Dictionary<string, string> 事件列表;
    private GameObject 内容父物体;
    private GameObject 图标父物体;
    private G_Util gut;
    private io io_;

    private void Awake()
    {
        io_ = io.GetInstance();
        bm = basicMgr.GetInstance();
        内容父物体 = gameObject.transform.Find("事件信息面板/事件内容").gameObject;
        图标父物体 = gameObject.transform.Find("事件信息面板/头部背景/Scroll View/Viewport/Content").gameObject;
        gut = NameMgr.画布.GetComponent<G_Util>();
    }



    public void OnEnable()
    {
        gut.操作子物体(图标父物体, 3);
        gut.操作子物体(内容父物体, 3);
        role_Data myData = NameMgr.IO.load();
        事件列表 = myData.事件表;
        int i = 事件列表.Count-1;
        foreach (string ActName in 事件列表.Keys)
        {
            gut.生成活动项(图标父物体, ActName, i,"事件");
            i--;
        }
    }




  

    public void 点击事件图标()
    {
        gut.操作子物体(内容父物体, 3);
        string ActName = GameObject.FindGameObjectWithTag("选中").transform.Find("名字").GetComponent<Text>().text;
        EvenMgr.GetInstance().事件调用(ActName);
    }


    public void 三年之约_UI() {
        GameObject 三年之约 = Resources.Load<GameObject>("活动/三年之约") as GameObject;
        事件实例 = Instantiate(三年之约);
        事件实例.transform.SetParent(内容父物体.transform, false);
    }

    public void 山海小师弟_UI()
    {
        GameObject 山海小师弟 = Resources.Load<GameObject>("活动/山海小师弟") as GameObject;
        事件实例 = Instantiate(山海小师弟);
        事件实例.transform.SetParent(内容父物体.transform, false);
    }

    public void 获得宠物_UI()
    {
        EvenMgr.GetInstance().事件模板("父亲在<color=blue>荒原</color>打猎,似乎发现了什么东西有助于你打败青梅竹马...快去看看吧", 跳转场景_荒原, 内容父物体);
    }

    public void 开启职业_UI()
    {
        EvenMgr.GetInstance().事件模板("拿着签名纸,前往<color=blue>临海城</color>找寻当年受父亲恩惠的人,看看是否有变强的方法", 跳转场景_临海城, 内容父物体);
    }

    public void 跳转场景_荒原() {
        gut.移动("荒原");
        隐藏自己();
    }

    public void 跳转场景_临海城()
    {
        gut.移动("临海城");
        隐藏自己();
    }


    public void 隐藏自己() {
        gameObject.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
