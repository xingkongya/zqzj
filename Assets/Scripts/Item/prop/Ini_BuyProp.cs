using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ini_BuyProp : MonoBehaviour
{
    private basicMgr bm;
    private PropMgr pm;
    private G_Util gut;
    private GameObject 列表;
    private GameObject 铜币按钮;
    private GameObject 金币按钮;
    private GameObject 仙晶按钮;
    private GameObject 黑钻按钮;

    private void Awake()
    {
        bm = basicMgr.GetInstance();
        pm = PropMgr.GetInstance();
        gut = NameMgr.画布.GetComponent<G_Util>();
        列表 = gameObject.transform.Find("Panel/Scroll View/Viewport/Content").gameObject;
        //获取游戏对象
        铜币按钮 = gameObject.transform.Find("Panel").Find("商城页/选项/铜币商店").gameObject;
        金币按钮 = gameObject.transform.Find("Panel").Find("商城页/选项/金币商店").gameObject;
        仙晶按钮 = gameObject.transform.Find("Panel").Find("商城页/选项/仙晶商店").gameObject;
        黑钻按钮 = gameObject.transform.Find("Panel").Find("商城页/选项/黑钻商店").gameObject;
      

    }

    private void OnEnable()
    {
        gameObject.transform.Find("Panel").transform.Find("商城页").gameObject.SetActive(true);//显示样式
    }

    public void ini_building(string str_index)
    {
        gut.操作子物体(列表, 3);

        int index = 0;
        GameObject 中心瞄点 = new GameObject();
        中心瞄点.transform.position = new Vector3(0, 0, 0);
        中心瞄点.AddComponent<RectTransform>();
        中心瞄点.transform.SetParent(列表.transform, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        中心瞄点.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);//recttransform必不可少的属性(半知半解)
        中心瞄点.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小


        Dictionary<string, Dictionary<string, string>> 物品列表 = new Dictionary<string, Dictionary<string, string>>();

        if (str_index.Equals("铜币商城"))
        {
            铜币点击颜色();
            物品列表 = BuyPropMGR.铜币商城购买表;
        }
        else if (str_index.Equals("金币商城"))
        {
            金币点击颜色();
            物品列表 = BuyPropMGR.金币商城购买表;
        }
        else if (str_index.Equals("仙晶商城"))
        {
            仙晶点击颜色();
            物品列表 = BuyPropMGR.仙晶商城购买表;
        }
        else if (str_index.Equals("黑钻商城"))
        {
            黑钻点击颜色();
            物品列表 = BuyPropMGR.黑钻商城购买表;
        }


        foreach (string 物品名 in 物品列表.Keys)
        {
            Prop_bascis pb = pm.检索物品(物品名);
            gut.生成道具购买项(pb, int.Parse( 物品列表[物品名]["价格"]), 物品列表[物品名]["货币"], 中心瞄点, index, 物品列表.Count, 物品列表[物品名]["数量"]);
            index++;

        }

    }



    public void ini_building(Dictionary<string, Dictionary<string, string>> 物品列表,string str_index)
    {
        gut.操作子物体(列表, 3);

        int index = 0;
        GameObject 中心瞄点 = new GameObject();
        中心瞄点.transform.position = new Vector3(0, 0, 0);
        中心瞄点.AddComponent<RectTransform>();
        中心瞄点.transform.SetParent(列表.transform, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        中心瞄点.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);//recttransform必不可少的属性(半知半解)
        中心瞄点.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小



        foreach (string 物品名 in 物品列表.Keys)
        {
            Prop_bascis pb = pm.检索物品(物品名);
            gut.生成道具购买项(pb, int.Parse(物品列表[物品名]["价格"]), 物品列表[物品名]["货币"], 中心瞄点, index, 物品列表.Count, 物品列表[物品名]["数量"]);
            index++;

        }

    }

    private void OnDisable()
    {
        gut.操作子物体(列表, 3);
        gameObject.transform.Find("Panel").Find("商城页").gameObject.SetActive(false);
        gameObject.GetComponent<Ini_BuyProp>().enabled = false;
    }

    public void 点击铜币选项()
    {
        铜币点击颜色();
        初始化铜币商城();
    }

    private void 铜币点击颜色() {
        bm.改变颜色(铜币按钮.GetComponent<Image>(), "FFFFFF");
        bm.改变颜色(金币按钮.GetComponent<Image>(), "A4A0A0");
        bm.改变颜色(仙晶按钮.GetComponent<Image>(), "A4A0A0");
        bm.改变颜色(黑钻按钮.GetComponent<Image>(), "A4A0A0");
    }


    private void 初始化铜币商城()
    {
        gut.生成商城界面("铜币商城");
    }

    public void 点击金币选项()
    {
        金币点击颜色();
        初始化金币商城();
    }

    private void 金币点击颜色()
    {
        bm.改变颜色(铜币按钮.GetComponent<Image>(), "A4A0A0");
        bm.改变颜色(金币按钮.GetComponent<Image>(), "FFFFFF");
        bm.改变颜色(仙晶按钮.GetComponent<Image>(), "A4A0A0");
        bm.改变颜色(黑钻按钮.GetComponent<Image>(), "A4A0A0");
    }


    private void 初始化金币商城()
    {
        gut.生成商城界面("金币商城");
    }

    public void 点击仙晶选项()
    {
        仙晶点击颜色();
        初始化仙晶商城();
    }

    private void 仙晶点击颜色()
    {
        bm.改变颜色(铜币按钮.GetComponent<Image>(), "A4A0A0");
        bm.改变颜色(金币按钮.GetComponent<Image>(), "A4A0A0");
        bm.改变颜色(仙晶按钮.GetComponent<Image>(), "FFFFFF");
        bm.改变颜色(黑钻按钮.GetComponent<Image>(), "A4A0A0");
    }


    private void 初始化仙晶商城() { 
        gut.生成商城界面("仙晶商城");
    }

    public void 点击黑钻选项()
    {
        黑钻点击颜色();
        初始化黑钻商城();
    }

    private void 黑钻点击颜色()
    {
        bm.改变颜色(铜币按钮.GetComponent<Image>(), "A4A0A0");
        bm.改变颜色(金币按钮.GetComponent<Image>(), "A4A0A0");
        bm.改变颜色(仙晶按钮.GetComponent<Image>(), "A4A0A0");
        bm.改变颜色(黑钻按钮.GetComponent<Image>(), "FFFFFF");
    }


    private void 初始化黑钻商城()
    {
        gut.生成商城界面("黑钻商城");
    }


}
