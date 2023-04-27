using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ini_PropPanel : MonoBehaviour
{
    private basicMgr bm;
    private PropMgr pm;
    private G_Util gut;
    private GameObject 列表;
    private Dictionary<string, Dictionary<string, int>> 普通道具购买表 = new Dictionary<string, Dictionary<string, int>>();

    private void Awake()
    {
        bm = basicMgr.GetInstance();
        pm = PropMgr.GetInstance();
        gut = NameMgr.画布.GetComponent<G_Util>();
        列表 = gameObject.transform.Find("Panel/Scroll View/Viewport/Content").gameObject;
        //有且只绑定一次
        普通道具购买表.Add("止血草", new Dictionary<string, int>() { { "铜币", 200 } });
        普通道具购买表.Add("金疮药", new Dictionary<string, int>() { { "铜币", 400 } });
        普通道具购买表.Add("补元散", new Dictionary<string, int>() { { "铜币", 800 } });
        普通道具购买表.Add("愈伤膏", new Dictionary<string, int>() { { "铜币", 2000 } });
        普通道具购买表.Add("含笑半步颠", new Dictionary<string, int>() { { "铜币", 9999 } });
        普通道具购买表.Add("绣花针", new Dictionary<string, int>() { { "铜币", 200 } });
        普通道具购买表.Add("飞镖", new Dictionary<string, int>() { { "铜币", 400 } });
        普通道具购买表.Add("暗箭", new Dictionary<string, int>() { { "铜币", 800 } });
        普通道具购买表.Add("十字弩", new Dictionary<string, int>() { { "铜币", 2000 } });
        普通道具购买表.Add("风雷石", new Dictionary<string, int>() { { "铜币", 10000 } });
        普通道具购买表.Add("火雷石", new Dictionary<string, int>() { { "铜币", 10000 } });
        普通道具购买表.Add("水雷石", new Dictionary<string, int>() { { "铜币", 10000 } });
    }

    private void OnEnable()
    {
        gameObject.transform.Find("Panel").transform.Find("道具页").gameObject.SetActive(true);//显示技能学习页的样式
    }

    public void ini_building(Dictionary<string, string> 物品列表)
    {
        int index = 0;
        GameObject 中心瞄点 = new GameObject();
        中心瞄点.transform.position = new Vector3(0, 0, 0);
        中心瞄点.AddComponent<RectTransform>();
        中心瞄点.transform.SetParent(列表.transform, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        中心瞄点.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);//recttransform必不可少的属性(半知半解)
        中心瞄点.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小

        foreach (string 物品名 in 物品列表.Keys)
        {
            Prop_bascis pb= pm.检索物品(物品名);
            gut.生成道具购买项(pb, 普通道具购买表[物品名]["铜币"], "铜币",中心瞄点 , index, 物品列表.Count,物品列表[物品名]);
            index++;

        }

    }

    private void OnDisable()
    {
        gut.操作子物体(列表, 3);
        gameObject.GetComponent<Ini_PropPanel>().enabled = false;
    }



}
