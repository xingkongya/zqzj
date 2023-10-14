using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bag : MonoBehaviour
{
    public bool isEquipment;
    private G_Util utg;
    private GameObject 道具标签;
    private GameObject 装备标签;
    private io io_;
    private PropMgr pm;
    private basicMgr bm;
    // Start is called before the first frame update

    private void Awake()
    {
        utg = NameMgr.画布.GetComponent<G_Util>();
        io_ = io.GetInstance();
        pm = PropMgr.GetInstance();
        bm = basicMgr.GetInstance();
        道具标签 = gameObject.transform.Find("Panel/标签/道具").gameObject;
        装备标签 = gameObject.transform.Find("Panel/标签/装备").gameObject;
    }

    private void Start()
    {
        初始化背包();
    }


    // Update is called once per frame
    void Update()
    {

    }


    public void 初始化背包()
    {

        GameObject 展示栏 = gameObject.transform.Find("Panel/Scroll View/Viewport/Bag_Content").gameObject;
        //删除遗留项
        for (int i = 0; i < 展示栏.transform.childCount; i++)
        {
            Destroy(展示栏.transform.GetChild(i).gameObject);
        }


        role_Data myData = io.GetInstance().load();
        //当前为道具页
        int n = 0;
        if (!isEquipment)
        {          
            foreach (string str in myData.材料背包.Keys)
            {
                n++;
                Prop_bascis 道具 = myData.材料背包[str];
                utg.生成物品项(n, 道具, bm.Xstoi(道具.num));
            }
        }
        else//当前为装备页
        {
            foreach (string str in myData.装备背包.Keys)
            {
                n++;
                utg.生成装备项(n, str,myData);
            }

        }
        展示栏.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 80 * n);//下拉框长度

    }


    public void 初始化出售设置()
    {
        //检索一键出售设置
        role_Data myData = io_.load();
        if (!myData.列表型记录.ContainsKey("出售设置"))
            myData.列表型记录.Add("出售设置", new Dictionary<string, List<string>>() { { "类型", new List<string>() }, { "颜色", new List<string>() } });

        Dictionary<string, List<string>> 出售设置 = myData.列表型记录["出售设置"];
        foreach (string xing in 出售设置["类型"])
        {
            gameObject.transform.Find("Panel/界面/一键出售界面/框架/" + xing + "/Button/打勾").gameObject.SetActive(true);
        }
        foreach (string color in 出售设置["颜色"])
        {
            gameObject.transform.Find("Panel/界面/一键出售界面/框架/" + color + "/Button/打勾").gameObject.SetActive(true);
        }
        io_.save(myData);

        //刷新出售设置标题();
    }


    public void 点击道具页() {
        if (isEquipment)
        {
            basicMgr bm = basicMgr.GetInstance();
            //道具标签不透明
            道具标签.GetComponent<Image>().color = bm.改变透明度(道具标签, 255);
            道具标签.transform.Find("Text").GetComponent<Text>().color = bm.改变透明度(道具标签.transform.Find("Text").gameObject, 255);
            //装备标签透明
            装备标签.GetComponent<Image>().color = bm.改变透明度(装备标签, 100);
            装备标签.transform.Find("Text").GetComponent<Text>().color = bm.改变透明度(装备标签.transform.Find("Text").gameObject, 120);
            gameObject.transform.Find("Panel/head/数量").GetComponent<Text>().text = "数量";
            isEquipment = false;
            初始化背包();
            //刷新出售设置标题();
        }

    }

    public void 刷新出售设置标题() {
        if (isEquipment)
            gameObject.transform.Find("Panel/界面/一键出售界面/title/Text").GetComponent<Text>().text = "装备";
        else
            gameObject.transform.Find("Panel/界面/一键出售界面/title/Text").GetComponent<Text>().text = "材料";
    }

    public void 点击装备页()
    {
        if (!isEquipment)
        {
            basicMgr bm = basicMgr.GetInstance();
            //道具标签透明
            道具标签.GetComponent<Image>().color = bm.改变透明度(道具标签, 100);
            道具标签.transform.Find("Text").GetComponent<Text>().color = bm.改变透明度(道具标签.transform.Find("Text").gameObject, 120);
            //装备标签不透明
            装备标签.GetComponent<Image>().color = bm.改变透明度(装备标签, 255);
            装备标签.transform.Find("Text").GetComponent<Text>().color = bm.改变透明度(装备标签.transform.Find("Text").gameObject, 255);
            gameObject.transform.Find("Panel/head/数量").GetComponent<Text>().text = "等级";
            isEquipment = true;
            初始化背包();
            //刷新出售设置标题();
        }
    }

    public void 出售() {
        role_Data myData = io_.load();
        GameObject 选中项 = GameObject.FindGameObjectWithTag("选中");
        if ( 选中项== null) {
            utg.生成警告框("未选中");
            return;
        }
        string 物品名 = 选中项.gameObject.transform.Find("名字").GetComponent<Text>().text;
        Prop_bascis pb = pm.检索物品(物品名);
        int 数量;
        if (PropMgr.装备表.ContainsKey(物品名))
        {
            数量 = 1;
            if (myData.装备背包[选中项.name].islock.Equals("1"))
            {
                utg.生成警告框("已锁定");
                return;
            }
        }
        else
        {
            数量 = bm.Xstoi(myData.材料背包[物品名].num);
            if (myData.材料背包[物品名].islock.Equals("1"))
            {
                utg.生成警告框("已锁定");
                return;
            }
        }
        /*
        int money = 数量 * bm.Xstoi(pb.price);
        utg.生成获得框("铜币", money);
        utg.加金钱(new Dictionary<string, int>() { { "铜币", money } });
        pm.失去物品(物品名,数量);
        初始化背包();
         */
        utg.生成拉条(物品名,选中项.name,bm.Xstoi(pb.price), 数量, "铜币", "出售");

    }


    public void 一键出售() {
        gameObject.transform.Find("Panel/界面/一键出售界面").gameObject.SetActive(false);//隐藏出售界面
        role_Data myData = io_.load();
        Dictionary<string, List<string>> 出售设置 = myData.列表型记录["出售设置"];
        //List<int> xing = new List<int>();
        List<int> color = new List<int>();
        if ( 出售设置["颜色"].Count == 0)//有未设置完整的,不执行方法
            return;
        else
        {
            /*foreach (string 星级 in 出售设置["星级"]) {
                if (星级.Equals("一星"))
                    xing.Add(1);
                else if(星级.Equals("二星"))
                    xing.Add(2);
                else if (星级.Equals("三星"))
                    xing.Add(3);
                else if (星级.Equals("四星"))
                    xing.Add(4);
                else if (星级.Equals("五星"))
                    xing.Add(5);
            }*/

            foreach (string 颜色 in 出售设置["颜色"])
            {
                if (颜色.Equals("白色"))
                    color.Add(0);
                else if (颜色.Equals("绿色"))
                    color.Add(1);
                else if (颜色.Equals("蓝色"))
                    color.Add(2);
                else if (颜色.Equals("紫色"))
                    color.Add(3);
                else if (颜色.Equals("金色"))
                    color.Add(4);
            }
        }

        int type;
        int money = 0;
        if (isEquipment)
        {
            Dictionary<int, string> 品质与晶矿 = new Dictionary<int, string>() { { 0, "灰色晶矿" }, { 1, "绿色晶矿" }, { 2, "蓝色晶矿" }, { 3, "紫色晶矿" }, { 4, "橙色晶矿" } };
            Dictionary<string, Equipment> 装备列表;
            装备列表 = myData.装备背包;

            foreach (string 装备键 in 装备列表.Keys)
            {
                Equipment 装备 = 装备列表[装备键];
                if ( color.Contains(bm.Xstoi(装备.qua))&& 装备.islock.Equals("0"))
                {
                    pm.获取物品(品质与晶矿[bm.Xstoi(装备.qua)],1);
                    pm.失去物品(装备键,1);
                }
            }
        }
        else
        {
           List<string> 物品名列表 = new List<string>();
            type = 1;
            Dictionary<string, Prop_bascis> 背包列表 = myData.材料背包;
            foreach (string 物品名 in 背包列表.Keys)
            {
                物品名列表.Add(物品名);
            }

            for (int i = 0; i < 物品名列表.Count; i++)
            {
                Prop_bascis pb = pm.检索物品(物品名列表[i]);
                if ( color.Contains(bm.Xstoi(pb.qua)) && int.Parse(pb.type) == type && 背包列表[物品名列表[i]].islock.Equals("0"))
                {
                    money += bm.Xstoi(pb.price) * bm.Xstoi(背包列表[物品名列表[i]].num);
                    pm.失去物品(物品名列表[i], bm.Xstoi(背包列表[物品名列表[i]].num));
                }
            }
        }
       
       
        utg.加金钱(new Dictionary<string, int>() { { "铜币", money } });
        utg.生成获得框("铜币", money);
        初始化背包();
    }


    public void 一键出售装备()
    {
        gameObject.transform.Find("Panel/界面/一键出售界面").gameObject.SetActive(false);//隐藏出售界面
        role_Data myData = io_.load();
        Dictionary<string, List<string>> 出售设置 = myData.列表型记录["出售设置"];
        List<int> color = new List<int>();
        if (出售设置["颜色"].Count == 0)//有未设置完整的,不执行方法
            return;
        else
        {
            foreach (string 颜色 in 出售设置["颜色"])
            {
                if (颜色.Equals("白色"))
                    color.Add(0);
                else if (颜色.Equals("绿色"))
                    color.Add(1);
                else if (颜色.Equals("蓝色"))
                    color.Add(2);
                else if (颜色.Equals("紫色"))
                    color.Add(3);
                else if (颜色.Equals("金色"))
                    color.Add(4);
            }
        }

        /*int type;
        List<string> 键名列表 = new List<string>();
        int money = 0;
            type = 3;
            Dictionary<string, Equipment> 背包列表 = myData.装备背包;
            foreach (string 物品名 in 背包列表.Keys)
            {
            键名列表.Add(物品名);
            }

            for (int i = 0; i < 键名列表.Count; i++)
            {
            Equipment pb = myData.装备背包[键名列表[i]];
                if (color.Contains(bm.Xstoi(pb.qua)) && int.Parse(pb.type) == type && 背包列表[键名列表[i]].islock.Equals("0"))
                {
                money += bm.Xstoi(pb.price);
                    pm.失去装备(键名列表[i]);
                }
            }
      


        utg.加金钱(new Dictionary<string, int>() { { "铜币", money } });
        utg.生成获得框("铜币", money);*/
        Dictionary<int, string> 品质与晶矿 = new Dictionary<int, string>() { { 0, "灰色晶矿" }, { 1, "绿色晶矿" }, { 2, "蓝色晶矿" }, { 3, "紫色晶矿" }, { 4, "橙色晶矿" } };
        Dictionary<string, Equipment> 装备列表;
        装备列表 = myData.装备背包;

        int num = 0;
        foreach (string 装备键 in 装备列表.Keys)
        {
            Equipment 装备 = 装备列表[装备键];
            if (color.Contains(bm.Xstoi(装备.qua)) && 装备.islock.Equals("0"))
            {
                num++;
                pm.获取物品(品质与晶矿[bm.Xstoi(装备.qua)], 1);
                pm.失去装备(装备键);
            }
        }
        utg.生成获得框("晶矿", num); 
        初始化背包();
    }

    public void 关闭背包() {
        Destroy(gameObject);
    }

}
