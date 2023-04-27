using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PropEffect : BaseManager<PropEffect>
{
    private PropMgr pm = PropMgr.GetInstance();
    private io io_ = io.GetInstance();
    private SkillMgr sm = SkillMgr.GetInstance();
    public string 返回状态 = "";
    private G_Util gut = NameMgr.画布.GetComponent<G_Util>();
    private Dictionary<Prop_bascis, int> 物品列表;
    private PetMgr pet = PetMgr.GetInstance();
    private basicMgr bm = basicMgr.GetInstance();
    private RoleMgr rm = RoleMgr.GetInstance();


    public Dictionary<string, Action> 加载道具效果()
    {
        Dictionary<string, Action> 道具名字和其效果 = new Dictionary<string, Action>();
        道具名字和其效果.Add("馒头", 使用背包CD道具);
        道具名字和其效果.Add("桃子", 使用背包CD道具);
        道具名字和其效果.Add("止血草", 使用背包CD道具);
        道具名字和其效果.Add("金疮药", 使用背包CD道具);
        道具名字和其效果.Add("补元散", 使用背包CD道具);
        道具名字和其效果.Add("愈伤膏", 使用背包CD道具);
        道具名字和其效果.Add("人参精华", 使用背包CD道具);
        道具名字和其效果.Add("飞羽", 使用背包CD道具);
        道具名字和其效果.Add("绣花针", 使用背包CD道具);
        道具名字和其效果.Add("飞镖", 使用背包CD道具);
        道具名字和其效果.Add("暗箭", 使用背包CD道具);
        道具名字和其效果.Add("十字弩", 使用背包CD道具);
        道具名字和其效果.Add("含笑半步颠", 使用背包CD道具);
        道具名字和其效果.Add("CD道具-桃子", 桃子);
        道具名字和其效果.Add("CD道具-馒头", 馒头);
        道具名字和其效果.Add("CD道具-止血草", 止血草);
        道具名字和其效果.Add("CD道具-金疮药", 金疮药);
        道具名字和其效果.Add("CD道具-补元散", 补元散);
        道具名字和其效果.Add("CD道具-愈伤膏", 愈伤膏);
        道具名字和其效果.Add("CD道具-人参精华", 人参精华);
        道具名字和其效果.Add("CD道具-飞羽", 飞羽);
        道具名字和其效果.Add("CD道具-绣花针", 绣花针);
        道具名字和其效果.Add("CD道具-飞镖", 飞镖);
        道具名字和其效果.Add("CD道具-暗箭", 暗箭);
        道具名字和其效果.Add("CD道具-十字弩", 十字弩);
        道具名字和其效果.Add("CD道具-含笑半步颠", 含笑半步颠);
        道具名字和其效果.Add("大松果", 大松果);
        道具名字和其效果.Add("灵芝", 灵芝);
        道具名字和其效果.Add("百年灵芝", 百年灵芝);
        道具名字和其效果.Add("千年灵芝", 千年灵芝);
        道具名字和其效果.Add("祝福果子", 祝福果子);
        道具名字和其效果.Add("血菩提", 血菩提);
        道具名字和其效果.Add("造化金丹", 造化金丹);
        道具名字和其效果.Add("真龙精血", 真龙精血);
        道具名字和其效果.Add("钱袋子", 钱袋子);
        道具名字和其效果.Add("新手保送50级大礼包", 新手保送50级大礼包);
        道具名字和其效果.Add("10级新手礼包", 十级级新手礼包);
        道具名字和其效果.Add("20级新手礼包", 二十级级新手礼包);
        道具名字和其效果.Add("30级新手礼包", 三十级级新手礼包);
        道具名字和其效果.Add("40级新手礼包", 四十级级新手礼包);
        道具名字和其效果.Add("50级新手礼包", 五十级级新手礼包);
        道具名字和其效果.Add("2仙晶卡", 二仙晶卡);
        道具名字和其效果.Add("5仙晶卡", 五仙晶卡);
        道具名字和其效果.Add("10仙晶卡", 十仙晶卡);
        道具名字和其效果.Add("50仙晶卡", 五十仙晶卡);
        道具名字和其效果.Add("100仙晶卡", 一百仙晶卡);
        道具名字和其效果.Add("1000仙晶卡", 一千仙晶卡);
        道具名字和其效果.Add("☆9999仙晶卡☆", 九千九仙晶卡);
        道具名字和其效果.Add("≮99999仙晶卡≯", 九万九仙晶卡);
        道具名字和其效果.Add("≮三千年壬水蟠桃≯", 三千年壬水蟠桃);
        道具名字和其效果.Add("≮六千年壬水蟠桃≯", 六千年壬水蟠桃);
        道具名字和其效果.Add("≮九千年壬水蟠桃≯", 九千年壬水蟠桃);
        道具名字和其效果.Add("≮黄中李≯", 黄中李);
        道具名字和其效果.Add("≮人参果≯", 人参果);
        道具名字和其效果.Add("Vip礼包1", Vip礼包1);
        道具名字和其效果.Add("技能书<桃之夭夭(初级)>", 初级桃之夭夭技能书);
        道具名字和其效果.Add("技能书<包扎>", 包扎技能书);
        道具名字和其效果.Add("技能书<焚诀>", 焚诀技能书);
        道具名字和其效果.Add("技能书<磐石>", 磐石技能书);
        道具名字和其效果.Add("技能书<磐石身>", 磐石身技能书);
        道具名字和其效果.Add("技能书<吞噬>", 吞噬技能书);
        道具名字和其效果.Add("技能书<化龙>", 化龙技能书);
        道具名字和其效果.Add("技能书<治愈之水>", 治愈之水技能书);
        道具名字和其效果.Add("技能书<夺命三仙剑(伪)>", 夺命三仙剑_伪技能书);
        道具名字和其效果.Add("技能书<大荒囚天拳(伪)>", 大荒囚天拳_伪技能书);
        道具名字和其效果.Add("虾兵卵", 虾兵卵);
        道具名字和其效果.Add("蟹将卵", 蟹将卵);
        道具名字和其效果.Add("鲲鹏之卵", 鲲鹏卵);
        道具名字和其效果.Add("坤坤之卵", 坤坤卵);
        道具名字和其效果.Add("≮黄金鸟≯之卵", 黄金鸟卵);
        道具名字和其效果.Add("(完美)≮黄金鸟≯之卵", 完美_黄金鸟卵);
        道具名字和其效果.Add("(完美)小鸡之卵", 完美_小鸡卵);
        道具名字和其效果.Add("下品涅槃果", 下品涅槃果);
        道具名字和其效果.Add("中品涅槃果", 中品涅槃果);
        道具名字和其效果.Add("上品涅槃果", 上品涅槃果);
        道具名字和其效果.Add("极品涅槃果", 极品涅槃果);
        道具名字和其效果.Add("狗粮", 狗粮);
        道具名字和其效果.Add("神圣果子", 神圣果子);
        道具名字和其效果.Add("美味大骨", 美味大骨);
        道具名字和其效果.Add("宠爱零食", 宠爱零食);
        道具名字和其效果.Add("通天塔礼包", 通天塔礼包);
        道具名字和其效果.Add("2023礼包", 二零二三礼包);
        道具名字和其效果.Add("听海石", 听海石);

        return 道具名字和其效果;

    }

    public void 添加CD事件(string 物品名) { 
    
    }
    /// <summary>
    /// 效果道具
    /// </summary>
    /// 
    public void 馒头()
    {
        if (pm.失去物品("馒头", 1).Equals("成功"))
        {
            role_Data myData = io_.load();
            combat cb = DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>();
            cb.剩余血量 = bm.Xitos(bm.Xstoi(cb.剩余血量) + 50);
            myData.剩余血量 = bm.Xitos(bm.Xstoi(myData.剩余血量) + 50);
            io_.save(myData);
        }
    }

    public void 桃子()
    {
        if (pm.失去物品("桃子", 1).Equals("成功"))
        {
            role_Data myData = io_.load();
            combat cb = DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>();
            cb.剩余血量 =bm.Xitos(bm.Xstoi(cb.剩余血量)+  200);
            myData.剩余血量 = bm.Xitos(bm.Xstoi(myData.剩余血量) + 200);
            io_.save(myData);
        }
    }

    public void 止血草()
    {
        if (pm.失去物品("止血草", 1).Equals("成功"))
        {
            role_Data myData = io_.load();
            combat cb = DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>();
            cb.剩余血量 = bm.Xitos(bm.Xstoi(cb.剩余血量) + 500);
            myData.剩余血量 = bm.Xitos(bm.Xstoi(myData.剩余血量) + 500);
            io_.save(myData);
        }
    }

    public void 金疮药()
    {
        if (pm.失去物品("金疮药", 1).Equals("成功"))
        {
            role_Data myData = io_.load();
            combat cb = DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>();
            cb.剩余血量 = bm.Xitos(bm.Xstoi(cb.剩余血量) + 1500);
            myData.剩余血量 = bm.Xitos(bm.Xstoi(myData.剩余血量) + 1500);
            io_.save(myData);
        }
    }

    public void 补元散()
    {
        if (pm.失去物品("补元散", 1).Equals("成功"))
        {
            role_Data myData = io_.load();
            combat cb = DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>();
            cb.剩余血量 = bm.Xitos(bm.Xstoi(cb.剩余血量) + 3000);
            myData.剩余血量 = bm.Xitos(bm.Xstoi(myData.剩余血量) + 3000);
            io_.save(myData);
        }
    }

    public void 愈伤膏()
    {
        if (pm.失去物品("愈伤膏", 1).Equals("成功"))
        {
            role_Data myData = io_.load();
            combat cb = DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>();
            cb.剩余血量 = bm.Xitos(bm.Xstoi(cb.剩余血量) + 5000);
            myData.剩余血量 = bm.Xitos(bm.Xstoi(myData.剩余血量) + 5000);
            io_.save(myData);
        }
    }

    public void 人参精华()
    {
        if (pm.失去物品("人参精华", 1).Equals("成功"))
        {
            role_Data myData = io_.load();
            combat cb = DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>();
            cb.剩余血量 = bm.Xitos(bm.Xstoi(cb.剩余血量) + 8000);
            myData.剩余血量 = bm.Xitos(bm.Xstoi(myData.剩余血量) + 8000);
            io_.save(myData);
        }
    }

    public void 造化金丹()
    {
        if (pm.失去物品("造化金丹", 1).Equals("成功"))
        {
            role_Data myData = io_.load();
            combat cb = DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>();
            myData.灵根 = bm.Xitos(bm.Xstoi(myData.灵根) + 1);
            io_.save(myData);
            cb.人物属性刷新();
        }
    }

    public void 真龙精血()
    {
        role_Data myData = io_.load();
        if (myData.记录.ContainsKey("真龙精血") && int.Parse(myData.记录["真龙精血"]) >= 5)
        {
            返回状态 = "使用达上限";
            return;
        }
        else
        {
            if (pm.失去物品("真龙精血", 1).Equals("成功"))
            {
                myData = io_.load();
                combat cb = DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>();
                myData.灵根 = bm.Xitos(bm.Xstoi(myData.灵根) + 1);
                io_.save(myData);
                cb.人物属性刷新();
                rm.记录数据_数值增长("真龙精血", "1");
            }
        }
    }

    public void 人参果()
    {
        role_Data myData = io_.load();
        if (myData.记录.ContainsKey("≮人参果≯") && int.Parse(myData.记录["≮人参果≯"]) >= 99)
        {
            返回状态 = "使用达上限";
            return;
        }
        else
        {
            if (pm.失去物品("≮人参果≯", 1).Equals("成功"))
            {
                myData = io_.load();
                combat cb = DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>();
                myData.灵根 = bm.Xitos(bm.Xstoi(myData.灵根) + 3);
                io_.save(myData);
                cb.人物属性刷新();
                rm.记录数据_数值增长("≮人参果≯", "1");
            }
        }
    }

    public void 黄中李()
    {
        if (pm.失去物品("≮黄中李≯", 1).Equals("成功"))
        {
            role_Data myData = io_.load();
            combat cb = DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>();
            myData.灵根 = bm.Xitos(bm.Xstoi(myData.灵根) + 50);
            io_.save(myData);
            cb.人物属性刷新();
        }
    }

    public void 灵芝()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("灵芝", 1).Equals("成功"))
        {
            gut.加经验值(50000);
        }
    }

    public void 百年灵芝()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("百年灵芝", 1).Equals("成功"))
        {
            gut.加经验值(100000);
        }
    }

    public void 千年灵芝()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("千年灵芝", 1).Equals("成功"))
        {
            gut.加经验值(500000);
        }
    }

    public void 三千年壬水蟠桃()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("≮三千年壬水蟠桃≯", 1).Equals("成功"))
        {
            gut.加经验值(500000000);
        }
    }

    public void 六千年壬水蟠桃()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("≮六千年壬水蟠桃≯", 1).Equals("成功"))
        {
            gut.加经验值(2000000000);
        }
    }

    public void 九千年壬水蟠桃()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("≮九千年壬水蟠桃≯", 1).Equals("成功"))
        {
            gut.加经验值(10000000000);
        }
    }

    public void 大松果()
    {
         gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("大松果", 1).Equals("成功"))
        {
            gut.加经验值(800);
        }
    }

    public void 祝福果子()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("祝福果子", 1).Equals("成功"))
        {
            gut.加经验值(6666);
        }
    }

    public void 血菩提()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("血菩提", 1).Equals("成功"))
        {
            gut.加经验值(1000000);
        }
    }

    public void 下品涅槃果()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("下品涅槃果", 1).Equals("成功"))
        {
            gut.经验池加经验值(100000);
        }
    }

    public void 中品涅槃果()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("中品涅槃果", 1).Equals("成功"))
        {
            gut.经验池加经验值(1000000);
        }
    }

    public void 上品涅槃果()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("上品涅槃果", 1).Equals("成功"))
        {
            gut.经验池加经验值(10000000);
        }
    }

    public void 极品涅槃果()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("极品涅槃果", 1).Equals("成功"))
        {
            gut.经验池加经验值(100000000);
        }
    }

    public void 神圣果子()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("神圣果子", 1).Equals("成功"))
        {
            gut.经验池加经验值(50000);
        }
    }


    public void 狗粮()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("狗粮", 1).Equals("成功"))
        {
            gut.经验池加经验值(1000);
        }
    }


    public void 美味大骨()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("美味大骨", 1).Equals("成功"))
        {
            gut.经验池加经验值(5000);
        }
    }

    public void 宠爱零食()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("宠爱零食", 1).Equals("成功"))
        {
            gut.经验池加经验值(20000);
        }
    }


    public void 飞羽() {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("飞羽", 1).Equals("成功"))
        {
            gut.暗器伤害(200);
        }
    }

    public void 绣花针()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("绣花针", 1).Equals("成功"))
        {
            gut.暗器伤害(300);
        }
    }

    public void 飞镖()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("飞镖", 1).Equals("成功"))
        {
            gut.暗器伤害(800);
        }
    }

    public void 暗箭()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("暗箭", 1).Equals("成功"))
        {
            gut.暗器伤害(1800);
        }
    }

    public void 十字弩()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("十字弩", 1).Equals("成功"))
        {
            gut.暗器伤害(3000);
        }
    }


    public void 含笑半步颠()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("含笑半步颠", 1).Equals("成功"))
        {
            gut.暗器伤害(9999);
        }
    }


    /// <summary>
    /// 加金钱
    /// </summary>
    /// 
    public void 钱袋子()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("钱袋子", 1).Equals("成功"))
        {
            Dictionary<string, int> 钱币 = new Dictionary<string, int>() { { "铜币", 100 } };
            gut.加金钱(钱币);
        }
    }


    /// <summary>
    /// 仙晶卡
    /// </summary>
    public void 二仙晶卡()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("2仙晶卡", 1).Equals("成功"))
        {
            Dictionary<string, int> 钱币 = new Dictionary<string, int>() { { "仙晶", 2 } };
            gut.加金钱(钱币);
        }
    }

    public void 五仙晶卡()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("5仙晶卡", 1).Equals("成功"))
        {
            Dictionary<string, int> 钱币 = new Dictionary<string, int>() { { "仙晶", 5 } };
            gut.加金钱(钱币);
        }
    }

    public void 十仙晶卡()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("10仙晶卡", 1).Equals("成功"))
        {
            Dictionary<string, int> 钱币 = new Dictionary<string, int>() { { "仙晶", 10 } };
            gut.加金钱(钱币);
        }
    }

    public void 五十仙晶卡()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("50仙晶卡", 1).Equals("成功"))
        {
            Dictionary<string, int> 钱币 = new Dictionary<string, int>() { { "仙晶", 50 } };
            gut.加金钱(钱币);
        }
    }

    public void 一百仙晶卡()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("100仙晶卡", 1).Equals("成功"))
        {
            Dictionary<string, int> 钱币 = new Dictionary<string, int>() { { "仙晶", 100 } };
            gut.加金钱(钱币);
        }
    }

    public void 一千仙晶卡()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("1000仙晶卡", 1).Equals("成功"))
        {
            Dictionary<string, int> 钱币 = new Dictionary<string, int>() { { "仙晶", 1000 } };
            gut.加金钱(钱币);
        }
    }

    public void 九千九仙晶卡()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("☆9999仙晶卡☆", 1).Equals("成功"))
        {
            Dictionary<string, int> 钱币 = new Dictionary<string, int>() { { "仙晶", 9999 } };
            gut.加金钱(钱币);
        }
    }


    public void 九万九仙晶卡()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("≮99999仙晶卡≯", 1).Equals("成功"))
        {
            Dictionary<string, int> 钱币 = new Dictionary<string, int>() { { "仙晶", 99999 } };
            gut.加金钱(钱币);
        }
    }


    /// <summary>
    /// 礼包
    /// </summary>
    public void 新手保送50级大礼包()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();

        if (pm.失去物品("新手保送50级大礼包", 1).Equals("成功"))
        {
            物品列表 = new Dictionary<Prop_bascis, int>();
            物品列表.Add(pm.检索物品("10级新手礼包"),1);
            物品列表.Add(pm.检索物品("止血草"), 10);
            pm.获取多个物品并显示特效(物品列表,"背包");
        }       
    }

    public void 十级级新手礼包()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();

        role_Data myData = io_.load();
        if (bm.Xstoi(myData.等级) < 10)
        {
            返回状态 = "等级不足";
            return;
        }

        if (pm.失去物品("10级新手礼包", 1).Equals("成功"))
        {
            物品列表 = new Dictionary<Prop_bascis, int>();
            物品列表.Add(pm.检索物品("20级新手礼包"), 1);
            物品列表.Add(pm.检索物品("灰色精华"), 10);
            物品列表.Add(pm.检索物品("50仙晶卡"), 1);
            pm.获取多个物品并显示特效(物品列表, "背包");
        }
    }

    public void 二十级级新手礼包()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();

        role_Data myData = io_.load();
        if (bm.Xstoi(myData.等级) < 20)
        {
            返回状态 = "等级不足";
            return;
        }

        if (pm.失去物品("20级新手礼包", 1).Equals("成功"))
        {
            物品列表 = new Dictionary<Prop_bascis, int>();
            物品列表.Add(pm.检索物品("30级新手礼包"), 1);
            物品列表.Add(pm.检索物品("(完美)≮黄金鸟≯之卵"), 1);
            物品列表.Add(pm.检索物品("绿色精华"), 5);
            物品列表.Add(pm.检索物品("神圣果子"), 1);
            物品列表.Add(pm.检索物品("100仙晶卡"), 2);
            pm.获取多个物品并显示特效(物品列表, "背包");
        }
    }


    public void 三十级级新手礼包()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();

        role_Data myData = io_.load();
        if (bm.Xstoi(myData.等级) < 30)
        {
            返回状态 = "等级不足";
            return;
        }

        if (pm.失去物品("30级新手礼包", 1).Equals("成功"))
        {
            物品列表 = new Dictionary<Prop_bascis, int>();
            物品列表.Add(pm.检索物品("40级新手礼包"), 1);
            物品列表.Add(pm.检索物品("下品涅槃果"), 1);
            物品列表.Add(pm.检索物品("蓝色精华"), 3);
            物品列表.Add(pm.检索物品("100仙晶卡"), 3);
            pm.获取多个物品并显示特效(物品列表, "背包");
        }
    }

    public void 四十级级新手礼包()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();

        role_Data myData = io_.load();
        if (bm.Xstoi(myData.等级) < 40)
        {
            返回状态 = "等级不足";
            return;
        }

        if (pm.失去物品("40级新手礼包", 1).Equals("成功"))
        {
            物品列表 = new Dictionary<Prop_bascis, int>();
            物品列表.Add(pm.检索物品("50级新手礼包"), 1);
            物品列表.Add(pm.检索物品("下品涅槃果"), 5);
            物品列表.Add(pm.检索物品("紫色精华"), 2);
            物品列表.Add(pm.检索物品("100仙晶卡"), 5);
            pm.获取多个物品并显示特效(物品列表, "背包");
        }
    }


    public void 五十级级新手礼包()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();

        role_Data myData = io_.load();
        if (bm.Xstoi(myData.等级) < 50)
        {
            返回状态 = "等级不足";
            return;
        }

        if (pm.失去物品("50级新手礼包", 1).Equals("成功"))
        {
            物品列表 = new Dictionary<Prop_bascis, int>();
            物品列表.Add(pm.检索物品("造化金丹"), 1);
            物品列表.Add(pm.检索物品("仙兽精华"), 1);
            物品列表.Add(pm.检索物品("1000仙晶卡"), 1);
            pm.获取多个物品并显示特效(物品列表, "背包");
        }
    }

    public void 通天塔礼包() {
        gut = NameMgr.画布.GetComponent<G_Util>();

        Dictionary<string, int> 礼包容器 = new Dictionary<string, int>();
        礼包容器.Add("坤坤之卵", 1);
        礼包容器.Add("☆9999仙晶卡☆", 2);
        礼包容器.Add("造化金丹",10);
        礼包容器.Add("神兽精华", 10);
        礼包容器.Add("技能书<通天秘典>", 10);
        礼包容器.Add("1000仙晶卡", 20);
        礼包容器.Add("通天剑", 25);
        礼包容器.Add("通天甲", 25);
        礼包容器.Add("通天头盔", 25); 
        礼包容器.Add("通天战靴", 25);
        礼包容器.Add("通天战戒", 25);
        礼包容器.Add("神兽气息", 50);
        礼包容器.Add("仙兽精华", 60);
        礼包容器.Add("小金币", 60);
        礼包容器.Add("100仙晶卡", 100);
        礼包容器.Add("五帝铜钱", 150);
        礼包容器.Add("生命之果", 300);
        礼包容器.Add("仙兽气息", 300);
        礼包容器.Add("血菩提", 300);
        礼包容器.Add("下品涅槃果", 400);//v1898
        礼包容器.Add("紫色精华", 500);
        礼包容器.Add("归元露", 1000);
        礼包容器.Add("蓝色精华", 1000);
        礼包容器.Add("绿色精华", 1200);
        礼包容器.Add("神圣果子", 1200);
        礼包容器.Add("兽皮", 1100);
        礼包容器.Add("兽骨", 1100);
        if (pm.失去物品("通天塔礼包", 1).Equals("成功"))
        {
            string 物品名 = gut.概率_礼包(礼包容器, "进化宝石");//概率总数为1万,上面那些都没出的话,保底出进化宝石
            物品列表 = new Dictionary<Prop_bascis, int>();
            if (物品名.Equals("兽皮") || 物品名.Equals("兽骨"))
            {
                物品列表.Add(pm.检索物品(物品名), 5);
            }
            else
            {
                物品列表.Add(pm.检索物品(物品名), 1);
            }
            pm.获取多个物品并显示特效(物品列表, "背包");
        }
    }

    public void 二零二三礼包()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();

        Dictionary<string, int> 礼包容器 = new Dictionary<string, int>();
        礼包容器.Add("技能书<夺命三仙剑(伪)>", 3333);
        礼包容器.Add("技能书<大荒囚天拳(伪)>", 3333);
        if (pm.失去物品("2023礼包", 1).Equals("成功"))
        {
            string 物品名 = gut.概率_礼包(礼包容器, "技能书<不死印诀(伪)>");//概率总数为1万,上面那些都没出的话,保底出
            物品列表 = new Dictionary<Prop_bascis, int>();
            物品列表.Add(pm.检索物品(物品名), 1);
            pm.获取多个物品并显示特效(物品列表, "背包");
        }
    }

    public void Vip礼包1()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();

        if (pm.失去物品("Vip礼包1", 1).Equals("成功"))
        {
            物品列表 = new Dictionary<Prop_bascis, int>();
            物品列表.Add(pm.检索物品("VIP左卡"), 1);
            物品列表.Add(pm.检索物品("VIP右卡"), 1);
            物品列表.Add(pm.检索物品("1000仙晶卡"), 1);
            pm.获取多个物品并显示特效(物品列表, "背包");
        }
    }

    /// <summary>
    /// 地图钥匙
    /// </summary>
    public void 听海石()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
          if (pm.失去物品("听海石", 1).Equals("成功"))
            {
            string SceneName = SceneManager.GetActiveScene().name;
            if (SceneName.Equals("碎石滩"))
            {
             GameObject 隐藏地图= GameObject.Find("combat_other").transform.Find("隐藏地图").gameObject;
                隐藏地图.SetActive(true);
            }
            else
                gut.生成警告框("什么都没发生");
            }
      
    }




    /// <summary>
    /// 技能书
    /// </summary>
    public void 初级桃之夭夭技能书() {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (sm.学习技能("桃之夭夭(初级)"))
        {
            if (!pm.失去物品("技能书<桃之夭夭(初级)>", 1).Equals("成功"))
            {

            }
        }
        else {
            返回状态 = "null";//返回状态为"null"时,不显示警告框
        }
    }

    public void 包扎技能书()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (sm.学习技能("包扎"))
        {
            if (!pm.失去物品("技能书<包扎>", 1).Equals("成功"))
            {

            }
        }
        else
        {
            返回状态 = "null";
        }
    }

    public void 大荒囚天拳_伪技能书()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (sm.学习技能("大荒囚天拳(伪)"))
        {
            if (!pm.失去物品("技能书<大荒囚天拳(伪)>", 1).Equals("成功"))
            {

            }
        }
        else
        {
            返回状态 = "null";
        }
    }

    public void 夺命三仙剑_伪技能书()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (sm.学习技能("夺命三仙剑(伪)"))
        {
            if (!pm.失去物品("技能书<夺命三仙剑(伪)>", 1).Equals("成功"))
            {

            }
        }
        else
        {
            返回状态 = "null";
        }
    }

    public void 焚诀技能书()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (sm.学习技能("≮焚诀≯"))
        {
            if (!pm.失去物品("技能书<焚诀>", 1).Equals("成功"))
            {

            }
        }
        else
        {
            返回状态 = "null";
        }
    }

    public void 磐石技能书()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (sm.学习技能("磐石"))
        {
            if (!pm.失去物品("技能书<磐石>", 1).Equals("成功"))
            {

            }
        }
        else
        {
            返回状态 = "null";
        }
    }

    public void 磐石身技能书()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (sm.学习技能("磐石身"))
        {
            if (!pm.失去物品("技能书<磐石身>", 1).Equals("成功"))
            {

            }
        }
        else
        {
            返回状态 = "null";
        }
    }

    public void 化龙技能书()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (sm.学习技能("≮化龙≯"))
        {
            if (!pm.失去物品("技能书<化龙>", 1).Equals("成功"))
            {

            }
        }
        else
        {
            返回状态 = "null";
        }
    }

    public void 吞噬技能书()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (sm.学习技能("吞噬"))
        {
            if (!pm.失去物品("技能书<吞噬>", 1).Equals("成功"))
            {

            }
        }
        else
        {
            返回状态 = "null";
        }
    }

    public void 治愈之水技能书()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (sm.学习技能("治愈之水"))
        {
            if (!pm.失去物品("技能书<治愈之水>", 1).Equals("成功"))
            {

            }
        }
        else
        {
            返回状态 = "null";
        }
    }


    /// <summary>
    /// 宠物蛋
    /// </summary>
    public void 虾兵卵() {
        gut = NameMgr.画布.GetComponent<G_Util>();
        role_Data myData = io_.load();
        if (myData.宠物栏.Count < 6)
        {
            if (pm.失去物品("虾兵卵", 1).Equals("成功"))
            {
                 myData = io_.load();
                Pet_Data 初始虾兵 = PropMgr.宠物表["虾兵"];
                pet.获取唯一ID(初始虾兵);
                myData.宠物栏.Add(pet.宠物资质初始化(初始虾兵));
                io_.save(myData);
            }
        }
        else {
            返回状态 = "宠物栏已满";
            return;
        }
    }

    public void 蟹将卵()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        role_Data myData = io_.load();
        if (myData.宠物栏.Count < 6)
        {
            if (pm.失去物品("蟹将卵", 1).Equals("成功"))
            {
                myData = io_.load();
                Pet_Data 初始蟹将 = PropMgr.宠物表["蟹将"];
                pet.获取唯一ID(初始蟹将);
                myData.宠物栏.Add(pet.宠物资质初始化(初始蟹将));
                io_.save(myData);
            }
        }
        else
        {
            返回状态 = "宠物栏已满";
            return;
        }
    }


    public void 黄金鸟卵()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        role_Data myData = io_.load();
        if (myData.宠物栏.Count < 6)
        {
            if (pm.失去物品("≮黄金鸟≯之卵", 1).Equals("成功"))
            {
                myData = io_.load();
                Pet_Data 初始黄金鸟 = PropMgr.宠物表["≮黄金鸟≯"];
                pet.获取唯一ID(初始黄金鸟);
                myData.宠物栏.Add(pet.宠物资质初始化(初始黄金鸟));
                io_.save(myData);
            }
        }
        else
        {
           返回状态="宠物栏已满";
            return;
        }
    }

    public void 鲲鹏卵()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        role_Data myData = io_.load();
        if (myData.宠物栏.Count < 6)
        {
            if (pm.失去物品("鲲鹏之卵", 1).Equals("成功"))
            {
                myData = io_.load();
                Pet_Data 初始宠物 = PropMgr.宠物表["鲲鹏"];
                pet.获取唯一ID(初始宠物);
                myData.宠物栏.Add(pet.宠物资质初始化(初始宠物));
                io_.save(myData);
            }
        }
        else
        {
            返回状态 = "宠物栏已满";
            return;
        }
    }

    public void 坤坤卵()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        role_Data myData = io_.load();
        if (myData.宠物栏.Count < 6)
        {
            if (pm.失去物品("坤坤之卵", 1).Equals("成功"))
            {
                myData = io_.load();
                Pet_Data 初始宠物 = PropMgr.宠物表["坤坤"];
                pet.获取唯一ID(初始宠物);
                myData.宠物栏.Add(pet.宠物资质初始化(初始宠物));
                io_.save(myData);
            }
        }
        else
        {
            返回状态 = "宠物栏已满";
            return;
        }
    }

    public void 完美_黄金鸟卵()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        role_Data myData = io_.load();
        if (myData.宠物栏.Count < 6)
        {
            if (pm.失去物品("(完美)≮黄金鸟≯之卵", 1).Equals("成功"))
            {
                myData = io_.load();
                Pet_Data 初始黄金鸟 = PropMgr.宠物表["≮黄金鸟≯"];
                pet.获取唯一ID(初始黄金鸟);
                myData.宠物栏.Add(pet.宠物初始满资质(初始黄金鸟));
                io_.save(myData);
            }
        }
        else
        {
            返回状态 = "宠物栏已满";
            return;
        }
    }

    public void 完美_小鸡卵()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        role_Data myData = io_.load();
        if (myData.宠物栏.Count < 6)
        {
            if (pm.失去物品("(完美)小鸡之卵", 1).Equals("成功"))
            {
                myData = io_.load();
                Pet_Data 小鸡 = PropMgr.宠物表["小鸡"];
                pet.获取唯一ID(小鸡);
                myData.宠物栏.Add(pet.宠物初始满资质(小鸡));
                io_.save(myData);
            }
        }
        else
        {
            返回状态 = "宠物栏已满";
            return;
        }
    }


    public void 使用背包CD道具() {
        gut = NameMgr.画布.GetComponent<G_Util>();
        gut.背包使用CD道具();
    }


}
