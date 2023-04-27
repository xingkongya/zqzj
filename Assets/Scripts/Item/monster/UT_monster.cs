using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class UT_monster : MonoBehaviour
{
    //引用
    public string 当前地图脚本名字;
    public string 怪物名字;
    private Color nowColor;
    private PropMgr pm;
    public List<string> 掉落信息 = new List<string>();
    public Dictionary<string, float> 技能与粒度 = new Dictionary<string, float>();

    //基本
    public float 怪物1出现概率;
    public float 怪物2出现概率;
    public float 怪物3出现概率;
    public float 怪物4出现概率;
    public float 怪物5出现概率;
    public int 怪物品质;
    public bool 是否主动攻击;
    public string 怪物图标="";
    public bool 眩晕状态 = false;
    public int 最低等级;
    public int 最高等级;
    public int 基础攻击力;
    public int 基础防御力;
    public int 基础血量;
    public float 基础暴击率;
    public int 基础回血值;
    public float 攻击力资质;
    public float 防御力资质;
    public float 血量资质;
    public float 成长;
    public float 攻击速度;
    public int 基础经验值;
    public int 经验值系数;
    public int 铜币;
    public int 金币=0;
    public int 当前等级=0;

    //自定义
    private G_Util utg;
    private basicMgr bm;
    private I_monster 当前地图脚本;


    private void Awake()
    {
        bm = basicMgr.GetInstance();
        utg = NameMgr.画布.GetComponent<G_Util>();
        pm = PropMgr.GetInstance();
        当前地图脚本 = (I_monster)gameObject.transform.GetComponent(当前地图脚本名字);
    }

    // Start is called before the first frame update
    void Start()
    {

    }




    public void 怪物赋值(combat cb, bool 建筑) {
        string SceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        if (SceneName.Equals("通天塔"))
        {
            当前地图脚本.怪物1赋值(cb);
        }
        else {
            if (建筑)
            {
                当前地图脚本.建筑赋值(cb);
            }
            else
            {
                //特殊怪物
                if (utg.概率(1, 1000)) {
                    特殊怪物赋值(当前等级,cb);
                }else if (怪物5出现概率 != 0 && utg.概率(1, (int)Math.Round(1 / 怪物5出现概率)))//从大到小...(round: 四舍五入)
                    当前地图脚本.怪物5赋值(cb);
                else if (怪物4出现概率 != 0 && utg.概率(1, (int)Math.Round(1 / 怪物4出现概率)))
                    当前地图脚本.怪物4赋值(cb);
                else if (怪物3出现概率 != 0 && utg.概率(1, (int)Math.Round(1 / 怪物3出现概率)))
                    当前地图脚本.怪物3赋值(cb);
                else if (怪物2出现概率 != 0 && utg.概率(1, (int)Math.Round(1 / 怪物2出现概率)))
                    当前地图脚本.怪物2赋值(cb);
                else
                    当前地图脚本.怪物1赋值(cb);
            }
        }

        加载赋值后的属性(cb,建筑);
    }

    public void 加载赋值后的属性(combat cb,bool 建筑) { 
        
        Random 随机类 = new Random(Guid.NewGuid().GetHashCode());


        //给comabat脚本赋值
        string 等级 = bm.Xor( 随机类.Next(最低等级, 最高等级 + 1)+"");
        string 攻击系数="10";
        string 防御系数="10";
        string 血量系数="100";
        if (怪物品质 == 0) {//白
            攻击系数 = "3";
            防御系数 = "0";
            血量系数 = "20";
        }
        else if (怪物品质 == 1)//绿
        {
            攻击系数 = "3";
            防御系数 = "1";
            血量系数 = "30";
        }
        else if (怪物品质 == 2)//蓝
        {
            攻击系数 = "4";
            防御系数 = "1";
            血量系数 = "50";
        }
        else if (怪物品质 == 3)//紫
        {
            攻击系数 = "5";
            防御系数 = "2";
            血量系数 = "80";
        }
        else if (怪物品质 == 4)//金
        {
            攻击系数 = "6";
            防御系数 = "3";
            血量系数 = "200";
        }
        else if (怪物品质 == 5)
        {
            攻击系数 = "10";
            防御系数 = "5";
            血量系数 = "500";
        }
        string 攻击力 = bm.Xor((int)Math.Ceiling(int.Parse(bm.Xor(等级)) * 成长 * 攻击力资质 * int.Parse(攻击系数) + 基础攻击力) + "");//ceiling: 向上取整
        string 防御力 = bm.Xor((int)Math.Ceiling(int.Parse(bm.Xor(等级)) * 成长 * 防御力资质 * int.Parse(防御系数) + 基础防御力) + "");
        string 血量 = bm.Xor((int)Math.Ceiling(int.Parse(bm.Xor(等级)) * 成长 * 血量资质 * int.Parse(血量系数) + 基础血量) + "");
        string 剩余血量 = 血量;
        string 回血值 = bm.Xor(基础回血值 + "");
        string 回血时间 = bm.Xor(5.0f + "");
        string 经验值 = bm.Xitos ((int)Math.Ceiling((基础经验值 + (int.Parse( bm.Xor(等级)) - 1) * 经验值系数)*0.5f));//Ceiling--向上取整...降低经验--目前定在1倍
        string 攻击速度_ = bm.Xor(攻击速度 + "");
        string 暴击率 = bm.Xor(基础暴击率 + "");
        string 铜币_= bm.Xor(铜币 + "");
        string 金币_ = bm.Xor(金币 + "");
        cb.怪物赋值(怪物名字, 等级, 怪物品质,是否主动攻击, 眩晕状态, 攻击力, 防御力, 血量,剩余血量, 回血值, 回血时间, 攻击速度_, 暴击率 ,经验值, 铜币_,金币_);
        铜币 = 0;//不常用属性手动初始化

        //怪物名字赋值与变色
        Text 名字文本 = cb.gameObject.transform.Find("name").GetComponent<Text>();
        if (建筑)
            名字文本.text = "<size=48>" + 怪物名字 + "</size>";
        else if (cb.gameObject.tag == ("boss") && 怪物品质 == 3)
            名字文本.text = "<size=100>" + 怪物名字 + "</size> <size=80>-仙兽 LV" + bm.Xstoi(等级) + "</size>";
        else if (cb.gameObject.tag == ("boss") && 怪物品质 == 4)
            名字文本.text = "<size=100>" + 怪物名字 + "</size> <size=80>-BOSS LV" + bm.Xstoi(等级) + "</size>";
        else
            名字文本.text = "<size=50>LV." + bm.Xstoi(等级) + "</size> "+" \n<size=46>" + 怪物名字 + "</size>";

        //1阶怪名字变黑色
        if (怪物品质 == 0)
            ColorUtility.TryParseHtmlString("#323232", out nowColor);
        //2阶精英怪名字变绿色
        else if (怪物品质 == 1)
            ColorUtility.TryParseHtmlString("#409C0A", out nowColor);
        //3阶精英怪名字变蓝色
        else if (怪物品质 == 2)
            ColorUtility.TryParseHtmlString("#114CEC", out nowColor);
        //4阶boss怪名字变紫色
        else if (怪物品质 == 3)
            ColorUtility.TryParseHtmlString("#CB1EB2", out nowColor);
        //5阶传说怪名字变橙色
        else if (怪物品质 == 4)
            ColorUtility.TryParseHtmlString("#FF9900", out nowColor);
        //6阶神话怪名字变红色
        else if (怪物品质 == 5)
            ColorUtility.TryParseHtmlString("#F60202", out nowColor);
        名字文本.color = nowColor;
    }

    public void 删除对象() {
        Destroy(gameObject);
    }


    public void 添加掉落(bool 概率, Dictionary<Prop_bascis, int> 掉落, string 掉落物名字, int 掉落数量)
    {
        if (概率)
        {
            if (!掉落.ContainsKey(pm.检索物品(掉落物名字)))
            {
                掉落.Add(pm.检索物品(掉落物名字), 掉落数量);
            }

            if (!掉落信息.Contains(掉落物名字)) {
                掉落信息.Add(掉落物名字);
            }

        }
    }


    public void 特殊怪物赋值(int 等级, combat cb) {
        if (等级>100||等级 == 0)
        {
            怪物名字 = "≮礼物龙≯";
            怪物品质 = 5;
            是否主动攻击 = false;
            最低等级 = 等级;
            最高等级 = 等级;
            基础攻击力 = 10 * 等级;
            基础防御力 = 5 * 等级;
            基础血量 = 100 * 等级;
            基础暴击率 = 20;
            基础回血值 = 8 * 等级;
            攻击力资质 = 1.5f;
            防御力资质 = 1.0f;
            血量资质 = 2.0f;
            成长 = 等级/10;
            攻击速度 = 1.5f;
            基础经验值 = 100000 * 等级;
            经验值系数 = 100;
            铜币 = (int)(500 * 成长);
            if (!cb.技能与粒度.ContainsKey("≮化龙≯"))
                cb.技能与粒度.Add("≮化龙≯", 0.25f);
            添加掉落(true, cb.掉落集合, "500仙晶卡", 1);
            添加掉落(utg.概率(1, 6), cb.掉落集合, "≮黄金羽衣≯", 1);
            添加掉落(utg.概率(1, 6), cb.掉落集合, "≮黄金羽冠≯", 1);
            添加掉落(utg.概率(1, 6), cb.掉落集合, "≮黄金羽翼≯", 1);
            添加掉落(utg.概率(1, 6), cb.掉落集合, "≮金色琉璃≯", 1);
        }
        else if (等级 <= 30)
        {
            怪物名字 = "≮黄金鸟≯";
            怪物品质 = 4;
            是否主动攻击 = false;
            最低等级 = 等级;
            最高等级 = 等级;
            基础攻击力 = 6 * 等级;
            基础防御力 = 2 * 等级;
            基础血量 = 50 * 等级;
            基础暴击率 = 10;
            基础回血值 = 3 * 等级;
            攻击力资质 = 0.68f;
            防御力资质 = 0.6f;
            血量资质 = 0.8f;
            成长 = 1;
            攻击速度 = 1.2f;
            基础经验值 = 300*等级;
            经验值系数 = 50;
            铜币 = (int)(100 * 成长);
            if (!cb.技能与粒度.ContainsKey("金色闪光"))
                cb.技能与粒度.Add("金色闪光", 0.25f);
            添加掉落(utg.概率(1, 6), cb.掉落集合, "≮黄金利爪≯", 1);
            添加掉落(utg.概率(1, 6), cb.掉落集合, "≮黄金羽衣≯", 1);
            添加掉落(utg.概率(1, 6), cb.掉落集合, "≮黄金羽冠≯", 1);
            添加掉落(utg.概率(1, 6), cb.掉落集合, "≮黄金羽翼≯", 1);
            添加掉落(utg.概率(1, 6), cb.掉落集合, "≮金色琉璃≯", 1);
        }
        else if (等级 <= 50) {
            怪物名字 = "≮波姆兔王≯";
            怪物品质 = 4;
            是否主动攻击 = false;
            最低等级 = 等级;
            最高等级 = 等级;
            基础攻击力 = 6 * 等级;
            基础防御力 = 4 * 等级;
            基础血量 = 90 * 等级;
            基础暴击率 = 10;
            基础回血值 = 4 * 等级;
            攻击力资质 = 0.75f;
            防御力资质 = 0.6f;
            血量资质 = 0.9f;
            成长 = 2;
            攻击速度 = 1.3f;
            基础经验值 = 1200*等级;
            经验值系数 = 50;
            铜币 = (int)(100 * 成长);
            /*if (!cb.技能与粒度.ContainsKey("再生"))
                cb.技能与粒度.Add("再生", 0.15f);*/
            if (!cb.技能与粒度.ContainsKey("撞击"))
                cb.技能与粒度.Add("撞击", 0.25f);
            添加掉落(utg.概率(1, 8), cb.掉落集合, "≮波姆宝石≯", 1);
            添加掉落(utg.概率(1, 8), cb.掉落集合, "≮波姆圣甲≯", 1);
            添加掉落(utg.概率(1, 8), cb.掉落集合, "≮波姆王冠≯", 1);
            添加掉落(utg.概率(1, 8), cb.掉落集合, "≮波姆战靴≯", 1);
            添加掉落(utg.概率(1, 8), cb.掉落集合, "≮四叶草≯", 1);
        }
        else if (等级 <= 80)
        {
            怪物名字 = "≮狐仙≯";
            怪物品质 = 4;
            是否主动攻击 = false;
            最低等级 = 等级;
            最高等级 = 等级;
            基础攻击力 = 8 * 等级;
            基础防御力 = 3 * 等级;
            基础血量 = 48 * 等级;
            基础暴击率 = 10;
            基础回血值 = 5 * 等级;
            攻击力资质 = 0.85f;
            防御力资质 = 0.6f;
            血量资质 = 1.1f;
            成长 = 5;
            攻击速度 = 1.1f;
            基础经验值 = 5200*等级;
            经验值系数 = 50;
            铜币 = (int)(100 * 成长);
            if (!cb.技能与粒度.ContainsKey("狐火"))
                cb.技能与粒度.Add("狐火", 0.15f);
            if (!cb.技能与粒度.ContainsKey("魅惑"))
                cb.技能与粒度.Add("魅惑", 0.25f);
            添加掉落(utg.概率(1, 10), cb.掉落集合, "≮狐仙宝珠≯", 1);
            添加掉落(utg.概率(1, 10), cb.掉落集合, "≮狐仙宝甲≯", 1);
            添加掉落(utg.概率(1, 10), cb.掉落集合, "≮狐仙桂冠≯", 1);
            添加掉落(utg.概率(1, 10), cb.掉落集合, "≮狐仙宝靴≯", 1);
            添加掉落(utg.概率(1, 10), cb.掉落集合, "≮狐火印记≯", 1);
        }
        else if (等级 <= 100)
        {
            怪物名字 = "≮炽天使≯";
            怪物品质 = 4;
            是否主动攻击 = false;
            最低等级 = 等级;
            最高等级 = 等级;
            基础攻击力 = 12 * 等级;
            基础防御力 = 5 * 等级;
            基础血量 = 75 * 等级;
            基础暴击率 = 10;
            基础回血值 = 8 * 等级;
            攻击力资质 = 0.75f;
            防御力资质 = 0.6f;
            血量资质 = 0.9f;
            成长 = 10;
            攻击速度 = 1f;
            基础经验值 = 25000*等级;
            经验值系数 = 50;
            铜币 = (int)(100 * 成长);
            if (!cb.技能与粒度.ContainsKey("天使降临"))
                cb.技能与粒度.Add("天使降临", 0.15f);
            if (!cb.技能与粒度.ContainsKey("审判"))
                cb.技能与粒度.Add("审判", 0.25f);
            添加掉落(utg.概率(1, 12), cb.掉落集合, "≮天使圣剑≯", 1);
            添加掉落(utg.概率(1, 12), cb.掉落集合, "≮天使圣铠≯", 1);
            添加掉落(utg.概率(1, 12), cb.掉落集合, "≮天使圣盔≯", 1);
            添加掉落(utg.概率(1, 12), cb.掉落集合, "≮天使战靴≯", 1);
            添加掉落(utg.概率(1, 12), cb.掉落集合, "≮天使之羽≯", 1);
        }



    }

    public void 添加基础掉落(combat cb,int index) {
        if (index == 1)
        {
            添加基础掉落信息(cb);
        }
        else if (index == 2)
        {
            添加基础掉落信息(cb);
            添加掉落(utg.概率(1, 30), cb.掉落集合, "美味大骨", 1);
            掉落信息.Add("美味大骨");
        }
        else if (index == 3)
        {
            添加基础掉落信息(cb);
            添加掉落(utg.概率(1, 30), cb.掉落集合, "宠爱零食", 1);
            掉落信息.Add("宠爱零食");
        }
        else if (index == 4)
        {
            添加基础掉落信息(cb);
            添加掉落(utg.概率(1, 30), cb.掉落集合, "神圣果子", 1);
            掉落信息.Add("神圣果子");
        }
        else if (index == 5)
        {
            添加基础掉落信息(cb);
            添加掉落(utg.概率(1, 30), cb.掉落集合, "下品涅槃果", 1);
            掉落信息.Add("下品涅槃果");
        }
        else if (index == 5)
        {
            添加基础掉落信息(cb);
            添加掉落(utg.概率(1, 30), cb.掉落集合, "中品涅槃果", 1);
            掉落信息.Add("下品涅槃果");
        }
    }


    private void 添加基础掉落信息(combat cb) {
        添加掉落(utg.概率(1, 50), cb.掉落集合, "兽骨", 1);
        添加掉落(utg.概率(1, 50), cb.掉落集合, "兽皮",1);
        添加掉落(utg.概率(1, 80), cb.掉落集合, "灰色精华", 1);
        掉落信息.Clear();
        掉落信息.Add("兽皮");
        掉落信息.Add("兽骨");
        添加掉落(utg.概率(1, 30), cb.掉落集合, "狗粮", 1);
        掉落信息.Add("灰色精华");
    }


    public void 添加通天塔十层掉落(combat cb)
    {
        添加掉落(utg.概率(1, 4), cb.掉落集合, "神圣果子", 1);
        添加掉落(utg.概率(1, 4), cb.掉落集合, "灵芝", 1);
        添加掉落(utg.概率(1, 5), cb.掉落集合, "通天塔礼包", 1);
        掉落信息.Clear();
        掉落信息.Add("通天塔礼包");
    }


    public void 添加通天塔三十层掉落(combat cb)
    {
        添加掉落(utg.概率(1, 4), cb.掉落集合, "下品涅槃果",1);
        添加掉落(utg.概率(1, 4), cb.掉落集合, "百年灵芝",utg.概率(1,3)?2:1);
        添加掉落(utg.概率(1, 3), cb.掉落集合, "通天塔礼包",1);
        掉落信息.Clear();
        掉落信息.Add("下品涅槃果");
        掉落信息.Add("百年灵芝");
        掉落信息.Add("通天塔礼包");
    }

    public void 添加通天塔六十层掉落(combat cb)
    {
        添加掉落(utg.概率(1, 4), cb.掉落集合, "中品涅槃果",1);
        添加掉落(utg.概率(1, 4), cb.掉落集合, "千年灵芝", utg.概率(1, 3) ? 2 : 1);
        添加掉落(utg.概率(1, 2), cb.掉落集合, "通天塔礼包",1);
        掉落信息.Clear();
        掉落信息.Add("中品涅槃果");
        掉落信息.Add("千年灵芝");
        掉落信息.Add("通天塔礼包");
    }

    public void 添加通天塔九十层掉落(combat cb)
    {
        添加掉落(utg.概率(1, 2), cb.掉落集合, "中品涅槃果", utg.概率(1, 3) ? 2 : 1);
        添加掉落(utg.概率(1, 3), cb.掉落集合, "血菩提", utg.概率(1, 3) ? 2 : 1);
        添加掉落(true, cb.掉落集合, "通天塔礼包", utg.概率(1, 3) ? 2 : 1);
        掉落信息.Clear();
        掉落信息.Add("中品涅槃果");
        掉落信息.Add("血菩提");
        掉落信息.Add("通天塔礼包");
    }

    public void 添加通天塔一百层掉落(combat cb)
    {
        添加掉落(utg.概率(1, 4), cb.掉落集合, "上品涅槃果",1);
        添加掉落(utg.概率(1, 4), cb.掉落集合, "血菩提", utg.概率(1, 3) ? 2 : 1);
        添加掉落(true, cb.掉落集合, "通天塔礼包",utg.概率(1, 3) ? 3 : 2);
        掉落信息.Clear();
        掉落信息.Add("上品涅槃果");
        掉落信息.Add("血菩提");
        掉落信息.Add("通天塔礼包");
    }

    public void 退出副本()
    {
        role_Data myData = NameMgr.IO.load();
        utg.跳转场景(myData.复活城市);
        副本脚本清零();
    }

    public void 副本脚本清零()
    {
        utg.副本步数 = 0;//当前为副本地图,先清零步数
    }

    private void OnDisable()
    {
        技能与粒度.Clear();
    }
}
