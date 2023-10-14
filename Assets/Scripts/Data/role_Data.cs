using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class role_Data : AllBasic
{
    private basicMgr bm = basicMgr.GetInstance();
    public string 名字;
    public string 灵根;
    public string 等级;
    public string 限制等级;
    public string 当前经验;
    public string 攻击力;
    public string 防御力;
    public string 暴击率;
    public string 血量;
    public string 回血值;
    public string 固定伤害;
    public string 固定减伤;
    public string 伤害加成;
    public string 伤害减免;
    public string 固定吸血;
    public string 吸血加成;
    public string 剩余血量;
    public string 攻击速度_;
    public string 移动速度;
    public string 回血时间;  
    public string 登录场景;
    public string 复活城市;
    public string 出战宠物UID;
    public Dictionary<string, string> 金钱; 
    public List<int> 打造图鉴;
    public Dictionary<string, string> 记录;
    public Dictionary<string, Dictionary<string,string>> 天赋;
    public Dictionary<string, string> 职业;
    public Dictionary<string, Dictionary<string, string>> 树形记录;
    public Dictionary<string, Dictionary<string, List<string>>> 列表型记录;
    public Dictionary<string, string> CD;
    public Dictionary<string, Equipment> 装备槽;
    public Dictionary<string, string> 技能槽;
    public Dictionary<string, string> 事件表;
    public Dictionary<string, List<string>> 被动技能;
    public List< Pet_Data> 宠物栏;
    public Dictionary<string, Prop_bascis> 材料背包; //Prop_bascis是物品的基类
    public Dictionary<string, Equipment> 装备背包; //Prop_bascis是物品的基类
    public Dictionary<string, List<SkillData>> 技能背包;
    public Dictionary<string, Dictionary<string, string>> 拓展;

    //构造函数
    public role_Data()
    {

        名字 = "王大牛";
        灵根 =bm.Xor( "1");
        等级 = bm.Xor("1");
        限制等级 = bm.Xor("50");
        当前经验 = bm.Xor("0");
        攻击力 = bm.Xor("10");
        防御力 = bm.Xor("2");
        血量 = bm.Xor("80");
        剩余血量 = bm.Xor("80");
        回血值 = bm.Xor("5");
        暴击率 = bm.Xor("0.0");
        固定伤害 = bm.Xor("0");
        固定减伤 = bm.Xor("0");
        伤害加成 = bm.Xor("1.0");
        伤害减免 = bm.Xor("0.0");
        固定吸血 = bm.Xor("0");
        吸血加成 = bm.Xor("0.0");
        攻击速度_ = bm.Xor("1.2");
        移动速度 = bm.Xor("1.0");
        回血时间 = bm.Xor("5.0");
        登录场景 = "木屋(家)";
        复活城市 = "桃源村";
        出战宠物UID=null;
        金钱 = new Dictionary<string, string>();
        金钱.Add("铜币", bm.Xor("0"));
        金钱.Add("金币", bm.Xor("0"));
        金钱.Add("仙晶", bm.Xor("0"));
        金钱.Add("积分", bm.Xor("0"));
        金钱.Add("广告币", bm.Xor("0"));
        金钱.Add("黑钻", bm.Xor("0"));
        记录 = new Dictionary<string, string>();
        记录.Add("背景音乐", "50");
        记录.Add("游戏音效", "50");
        打造图鉴 = new List<int>();
        打造图鉴.Add(1);
        装备槽 = new Dictionary<string, Equipment>();
        装备槽.Add("左卡", null);
        装备槽.Add("右卡", null);
        装备槽.Add("武器", null);
        装备槽.Add("衣服", null);
        装备槽.Add("头部", null);
        装备槽.Add("下装", null);
        装备槽.Add("饰品", null);
        技能槽 = new Dictionary<string, string>();
        技能槽.Add("0","");
        技能槽.Add("1", "");
        技能槽.Add("2", "");
        技能槽.Add("3", "");
        技能槽.Add("4", "");
        被动技能 = new Dictionary<string, List<string>>();
        被动技能.Add("职业", new List<string>());
        宠物栏 = new List<Pet_Data>();
        材料背包 = new Dictionary<string, Prop_bascis>();
        装备背包 = new Dictionary<string, Equipment>();
        技能背包 = new Dictionary<string, List<SkillData>>();
        CD = new Dictionary<string, string>();
        列表型记录 = new Dictionary<string, Dictionary<string, List<string>>>();
        列表型记录.Add("战斗设置", new Dictionary<string, List<string>>() { { "战斗设置", new List<string>() } });
        列表型记录.Add("出售设置", new Dictionary<string, List<string>>() { { "类型", new List<string>() }, { "颜色", new List<string>() } });
        拓展 = new Dictionary<string, Dictionary<string, string>>();
        树形记录 = new Dictionary<string, Dictionary<string, string>>();
        树形记录.Add("每日记录", new Dictionary<string, string>());
        天赋 = new Dictionary<string, Dictionary<string, string>>() { { "职业", new Dictionary<string, string>() },{"天赋",new Dictionary<string, string>() } };
        事件表=new Dictionary<string, string>();
    }


    public role_Data(string 名字,string 灵根,string 等级,string 当前经验,string 攻击力,string 防御力,string 暴击率,string 血量,string 回血值,string 固定伤害,string 伤害比率,string 固定吸血,string 吸血加成,string 剩余血量,string 攻击速度_, string 移动速度, string 回血时间,string 登录场景,string 复活城市, Dictionary<string, string> 金钱, List<int> 打造图鉴, Dictionary<string, string> 记录, Dictionary<string, string> 技能槽, Dictionary<string, Prop_bascis> 材料背包, Dictionary<string, Equipment> 装备背包, Dictionary<string, string> CD, Dictionary<string, Dictionary<string, List<string>>> 列表型记录,Dictionary<string,Dictionary<string,string>>拓展)
    {
        this.名字 = 名字;
        this.灵根 = 灵根;
        this.等级 = 等级;
        this.当前经验 = 当前经验;
        this.攻击力 = 攻击力;
        this.防御力 = 防御力;
        this.暴击率 = 暴击率;
        this.血量 = 血量;
        this.回血值 = 回血值;
        this.固定伤害 = 固定伤害;
        this.伤害加成 = 伤害比率;
        this.固定吸血 = 固定吸血;
        this.吸血加成 = 吸血加成;
        this.剩余血量 = 剩余血量;
        this.攻击速度_ = 攻击速度_;
        this.移动速度 = 移动速度;
        this.回血时间 = 回血时间;
        this.登录场景 = 登录场景;
        this.复活城市 = 复活城市;
        this.金钱 = 金钱;
        this.打造图鉴 = 打造图鉴;
        this.记录 = 记录;
        this.列表型记录 = 列表型记录;
        this.技能槽 = 技能槽;
        this.材料背包 = 材料背包;
        this.装备背包 = 装备背包;
        this.CD = CD;
        this.拓展 = 拓展;
    }

}
