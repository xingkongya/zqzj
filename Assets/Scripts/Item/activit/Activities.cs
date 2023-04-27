using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Activities : MonoBehaviour
{
    private basicMgr bm;
    GameObject 活动实例;
    private Dictionary<string, Action> 活动列表;
    private GameObject 内容父物体;
    private GameObject 图标父物体;
    private G_Util gut;
    private io io_;

    private void Awake()
    {
        io_ = io.GetInstance();
        bm = basicMgr.GetInstance();
        活动列表 = new Dictionary<string, Action>();
        内容父物体 = gameObject.transform.Find("活动信息面板/活动内容").gameObject;
        图标父物体 = gameObject.transform.Find("活动信息面板/头部背景/Scroll View/Viewport/Content").gameObject;
        活动列表.Add("挑战通天塔", 挑战通天塔);
        活动列表.Add("限时挑战", 测试);
        活动列表.Add("活动积分兑换", 测试);
        活动列表.Add("新人特惠", 测试);
        活动列表.Add("剩蛋小屋", 测试);
        gut = NameMgr.画布.GetComponent<G_Util>();
    }


    private void OnEnable()
    {
        gut.刷新网络时间();
        int i = 0;
        foreach (string ActName in 活动列表.Keys)
        {
            gut.生成活动项(图标父物体, ActName, i);
            i++;
        }
    }




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void 点击活动图标()
    {
        gut.操作子物体(内容父物体, 3);
        string ActName = GameObject.FindGameObjectWithTag("选中").transform.Find("名字").GetComponent<Text>().text;
        活动列表[ActName]();
    }



    public void 测试()
    {


    }

    public void 挑战通天塔()
    {
        role_Data myData = io_.load();
        GameObject 通天塔活动 = Resources.Load<GameObject>("活动/挑战通天塔");
        活动实例 = Instantiate(通天塔活动);
        活动实例.transform.SetParent(内容父物体.transform, false);
        Text 记录 = 活动实例.transform.Find("文本/记录").GetComponent<Text>();
        Text 当前层数 = 活动实例.transform.Find("文本/当前/Panel/Text").GetComponent<Text>();
        Text 花费 = 活动实例.transform.Find("button/重置/Text").GetComponent<Text>();
        GameObject 挑战按钮 = 活动实例.transform.Find("button/挑战").gameObject;
        GameObject 重置按钮 = 活动实例.transform.Find("button/重置").gameObject;
        if (myData.记录.ContainsKey("通天塔记录"))
        {
            记录.text = "*最高通过层数为:" + myData.记录["通天塔记录"] + "层";
        }
        else
        {
            记录.text = "*最高通过层数为:1层";
        }

        if (myData.树形记录["每日记录"].ContainsKey("通天塔重置次数"))
        {
            花费.text = "重置\n" + "(" + int.Parse(myData.树形记录["每日记录"]["通天塔重置次数"]) * 200 + "仙晶)";
        }
        else
        {
            花费.text = "重置\n" + "(免费)";
        }

        if (myData.树形记录["每日记录"].ContainsKey("通天塔层数"))
        {
            当前层数.text = myData.树形记录["每日记录"]["通天塔层数"];
        }
        else
        {
            当前层数.text = "1";
        }

        bm.Banding(挑战按钮, 跳转至通天塔);
        bm.Banding(重置按钮, 重置通天塔);
    }

    public void 跳转至通天塔()
    {
        gameObject.SetActive(false);
        gut.跳转场景("通天塔");
    }

    public void 重置通天塔() {
       
        role_Data myData = NameMgr.IO.load();
        if (myData.树形记录["每日记录"].ContainsKey("通天塔层数"))
        {
            myData.树形记录["每日记录"]["通天塔层数"] = "1";
        }
        else {
            myData.树形记录["每日记录"].Add("通天塔层数", "1");
        }

        if (myData.树形记录["每日记录"].ContainsKey("通天塔重置次数"))
        {
            myData.树形记录["每日记录"]["通天塔重置次数"] = int.Parse(myData.树形记录["每日记录"]["通天塔重置次数"]) + 1 + "";
        }
        else {
            myData.树形记录["每日记录"].Add("通天塔重置次数", "1");
        }
        NameMgr.IO.save(myData);
        Text 花费 = 活动实例.transform.Find("button/重置/Text").GetComponent<Text>();
        Text 当前层数 = 活动实例.transform.Find("文本/当前/Panel/Text").GetComponent<Text>();
        当前层数.text = "1";
        花费.text = "重置\n" + "(" + int.Parse(myData.树形记录["每日记录"]["通天塔重置次数"]) * 200 + "仙晶)";
    }



}
