using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ini_Building : MonoBehaviour
{
    private List<int> 打造图鉴等级 = new List<int>();
    private Dictionary<string, Dictionary<string, int>> 装备打造表 = new Dictionary<string, Dictionary<string, int>>();
    private Dictionary<int, List<string>> 装备等级表 = new Dictionary<int, List<string>>();
    private int page = 0;//页数,默认为0--根据下标来的
    private G_Util gut;
    private PropMgr pm;
    private basicMgr bm;
    GameObject 列表;


    private void Awake()
    {
        bm = basicMgr.GetInstance();
        pm = PropMgr.GetInstance();
        gut = NameMgr.画布.GetComponent<G_Util>();
        列表= gameObject.transform.Find("Panel/Scroll View/Viewport/Content").gameObject;
        //有且只绑定一次
        bm.Banding(gameObject.transform.Find("Panel/装备页/Button/上一页").gameObject, 上一页);
        bm.Banding(gameObject.transform.Find("Panel/装备页/Button/下一页").gameObject, 下一页);
        bm.Banding(gameObject.transform.Find("Panel/装备页/Button/首页").gameObject, 首页);
        bm.Banding(gameObject.transform.Find("Panel/装备页/Button/尾页").gameObject, 尾页);
        装备等级表.Add(1, new List<string>() { "木剑", "破布衣", "破布帽", "破布鞋", "犬牙面具", "狗皮大衣" });//改了这里还要改打造表的主键
        //装备等级表.Add(10, new List<string>() { "匕首", "皮甲", "皮帽", "皮靴" });
        //装备等级表.Add(20, new List<string>() { "藤弓", "藤甲", "藤冠", "藤鞋","桃木履","桃花冠" });
        装备等级表.Add(30, new List<string>() { "蛟鳞▪剑", "蛟鳞▪甲" , "蛟蛇▪角", "蛟蛇▪尾" });
        //装备等级表.Add(40, new List<string>() { "古铜剑", "古铜甲", "古铜帽", "古铜鞋" });
        装备等级表.Add(50, new List<string>() { "精铁剑", "精铁甲", "精铁冠", "精铁鞋" });

        Dictionary<string, int> 一级白装材料 = new Dictionary<string, int>() { { "木材", 1 } };
        装备打造表.Add("木剑", 一级白装材料);
        装备打造表.Add("破布衣", 一级白装材料);
        装备打造表.Add("破布帽", 一级白装材料);
        装备打造表.Add("破布鞋", 一级白装材料);
        装备打造表.Add("犬牙面具", new Dictionary<string, int>() { { "木材", 2 }, { "疯狗牙", 1 } });
        装备打造表.Add("狗皮大衣", new Dictionary<string, int>() { { "木材", 2 }, { "疯狗皮毛", 1 } });
        Dictionary<string, int> 十级白装材料 = new Dictionary<string, int>() { { "木材", 3 } };
        装备打造表.Add("匕首", 十级白装材料);
        装备打造表.Add("皮甲", 十级白装材料);
        装备打造表.Add("皮帽", 十级白装材料);
        装备打造表.Add("皮靴", 十级白装材料);
        Dictionary<string, int> 二十级白装材料 = new Dictionary<string, int>() { { "木材", 3 } };
        Dictionary<string, int> 二十级蓝装材料 = new Dictionary<string, int>() { { "百年桃木", 1 }, { "桃妖之心", 1 } };
        装备打造表.Add("藤弓", 二十级白装材料);
        装备打造表.Add("藤甲", 二十级白装材料);
        装备打造表.Add("藤冠", 二十级白装材料);
        装备打造表.Add("藤鞋", 二十级白装材料);
        //装备打造表.Add("桃木履", 二十级蓝装材料);
        //装备打造表.Add("桃花冠", 二十级蓝装材料);
        Dictionary<string, int> 三十级绿装材料 = new Dictionary<string, int>() {  { "兽皮", 1 }, { "兽骨", 1 } };
        //装备打造表.Add("精铁剑", 三十级绿装材料);
        //装备打造表.Add("精铁甲", 三十级绿装材料);
        //装备打造表.Add("精铁冠", 三十级绿装材料);
        //装备打造表.Add("精铁鞋", 三十级绿装材料);
        Dictionary<string, int> 蛟鳞_剑 = new Dictionary<string, int>() { { "蛟鳞剑", 1 }, { "蛟血", 2 } };
        Dictionary<string, int> 蛟鳞_甲 = new Dictionary<string, int>() { { "蛟鳞甲", 1 }, { "蛟血", 2 } };
        Dictionary<string, int> 蛟鳞_首 = new Dictionary<string, int>() { { "蛟蛇角", 1 }, { "蛟血", 2 } };
        Dictionary<string, int> 蛟鳞_尾 = new Dictionary<string, int>() { { "蛟蛇尾", 1 }, { "蛟血", 2 } };
        装备打造表.Add("蛟鳞▪剑", 蛟鳞_剑);
        装备打造表.Add("蛟鳞▪甲", 蛟鳞_甲);
        装备打造表.Add("蛟蛇▪角", 蛟鳞_首);
        装备打造表.Add("蛟蛇▪尾", 蛟鳞_尾);
        Dictionary<string, int> 四十级绿装材料 = new Dictionary<string, int>() {  { "兽皮", 2 }, { "兽骨", 2 } };
        装备打造表.Add("古铜剑", 四十级绿装材料);
        装备打造表.Add("古铜甲", 四十级绿装材料);
        装备打造表.Add("古铜帽", 四十级绿装材料);
        装备打造表.Add("古铜鞋", 四十级绿装材料);
        Dictionary<string, int> 五十级绿装材料 = new Dictionary<string, int>() {  { "兽皮", 3 }, { "兽骨", 3 } };
        装备打造表.Add("钛钢剑", 五十级绿装材料);
        装备打造表.Add("钛钢甲", 五十级绿装材料);
        装备打造表.Add("钛钢帽", 五十级绿装材料);
        装备打造表.Add("钛钢鞋", 五十级绿装材料);
    }

    private void OnEnable()
    {
        gameObject.transform.Find("Panel").transform.Find("装备页").gameObject.SetActive(true);//显示打造装备页的样式
        刷新所有的打造状态();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

 

    public void ini_building(List<int> 打造图鉴等级) {
        this.打造图鉴等级 = 打造图鉴等级;
        打造图鉴等级.Sort();//排序-从小到大

        List<string> 该页装备名字 = 装备等级表[打造图鉴等级[page]];


        int index = 0;
        foreach (string 装备名 in 该页装备名字)
        {
            Equipment 装备 = (Equipment)pm.检索物品(装备名);
            gut.生成打造装备信息(装备, 列表, index, 装备打造表[装备名],该页装备名字.Count);
            index++;
        }

        gameObject.transform.Find("Panel/装备页/Title/lv/Text").GetComponent<Text>().text = 打造图鉴等级[page]+"";
        int num = page + 1;
        gameObject.transform.Find("Panel/装备页/Page").GetComponent<Text>().text = "( " + num + "/" + 打造图鉴等级.Count + " )";
    }


    public void 打造装备()
    {
        string 装备名字 = GameObject.FindGameObjectWithTag("选中").transform.Find("名字").GetComponent<Text>().text;
        Dictionary<string, int> 需求材料 = 装备打造表[装备名字];
        if (pm.检测物品是否满足(需求材料))
        {
            foreach (string 材料名字 in 需求材料.Keys)
            {
                string str = pm.失去物品(材料名字, 需求材料[材料名字]);
                if (!str.Equals("成功"))
                {
                    gut.生成警告框(str);
                    return;
                }
            }
            Equipment 装备 = (Equipment)pm.检索物品(装备名字);
            gut.生成获得框(装备.name,1);
            pm.获取物品(装备);
        }
        else
            gut.生成警告框("材料不足");
        刷新所有的打造状态();

    }

    public void 刷新所有的打造状态() {
        List<GameObject> 对象组 = new List<GameObject>();
        GameObject 父物体 = gameObject.transform.Find("Panel/Scroll View/Viewport/Content").gameObject;
        for (int i=0;i<父物体.transform.childCount;i++) {
            对象组.Add(父物体.transform.GetChild(i).gameObject);
        }
        foreach (GameObject 对象 in 对象组)
        {
            DazaoItem dz = 对象.GetComponent<DazaoItem>();
            dz.刷新打造状态();
        }

    }


    public void 下一页() {
        if (page < 打造图鉴等级.Count-1)
        {
            ++page;
            GameObject 父物体 = gameObject.transform.Find("Panel/Scroll View/Viewport/Content").gameObject;
            //gut.删光子物体(父物体);
            删除遗留Item();
            ini_building(打造图鉴等级);
        }
    }

    public void 上一页()
    {
        if (page >=1)
        {
            --page;
            GameObject 父物体 = gameObject.transform.Find("Panel/Scroll View/Viewport/Content").gameObject;
            //gut.删光子物体(父物体);
            删除遗留Item();
            ini_building(打造图鉴等级);
        }
    }

    public void 删除遗留Item() {
        List<GameObject> 对象组= bm.查找所有同名对象("打造装备信息(Clone)");
        foreach (GameObject 对象 in 对象组) {
            DazaoItem dz = 对象.GetComponent<DazaoItem>();
            dz.删除自己();
        }
    }

    public void 尾页() {
        if (page < 打造图鉴等级.Count - 1)
        {
            page= 打造图鉴等级.Count-1;
            GameObject 父物体 = gameObject.transform.Find("Panel/Scroll View/Viewport/Content").gameObject;
            //gut.删光子物体(父物体);
            删除遗留Item();
            ini_building(打造图鉴等级);
        }

    }


    public void 首页()
    {
        if (page >= 1)
        {
            page = 0;
            GameObject 父物体 = gameObject.transform.Find("Panel/Scroll View/Viewport/Content").gameObject;
            //gut.删光子物体(父物体);
            删除遗留Item();
            ini_building(打造图鉴等级);
        }

    }
    private void OnDisable()
    {
        gut.操作子物体(列表, 3);
        gameObject.GetComponent<Ini_Building>().enabled = false;
    }
}
