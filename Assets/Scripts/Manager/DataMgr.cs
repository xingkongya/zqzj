using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataMgr : BaseManager<DataMgr>
{
    private basicMgr bm = basicMgr.GetInstance();
    private PropMgr pm = PropMgr.GetInstance();
    public Dictionary<string, string> 装备属性 = basicMgr.GetInstance().返回空的战斗属性();//装备属性包含装备,套装属性
    public Dictionary<string, string> 技能属性 = basicMgr.GetInstance().返回空的战斗属性();
    public Dictionary<string, string> 光环属性 = basicMgr.GetInstance().返回空的战斗属性();
    public Dictionary<string, string> 天赋属性 = basicMgr.GetInstance().返回空的战斗属性();
    public Dictionary<string, int> 临时经验与钱币 = new Dictionary<string, int>();
    public Dictionary<Prop_bascis, int> 临时物品列表 = new Dictionary<Prop_bascis, int>();
    public Dictionary<string, GameObject> 本地对象 = new Dictionary<string, GameObject>();
    private int index=0;


    private   Dictionary<string, Dictionary<string, string>> 返回属性系数表() {
        Dictionary<string, Dictionary<string, string>> 属性系数 = new Dictionary<string, Dictionary<string, string>>();
        Dictionary<string, string> 基础系数 = new Dictionary<string, string>();
        基础系数.Add("攻击",bm.Xitos(3));
        基础系数.Add("防御",bm.Xitos(1));
        基础系数.Add("血量",bm.Xitos(30));
        基础系数.Add("攻击速度",bm.Xftos(1.2f));
        Dictionary<string, string> 剑修系数 = new Dictionary<string, string>();
        剑修系数.Add("攻击", bm.Xitos(3));
        剑修系数.Add("防御", bm.Xitos(1));
        剑修系数.Add("血量", bm.Xitos(30));
        剑修系数.Add("攻击速度", bm.Xftos(1.2f));
        Dictionary<string, string> 体修系数 = new Dictionary<string, string>();
        体修系数.Add("攻击", bm.Xitos(3));
        体修系数.Add("防御", bm.Xitos(1));
        体修系数.Add("血量", bm.Xitos(30));
        体修系数.Add("攻击速度", bm.Xftos(1.2f));
        Dictionary<string, string> 灵修系数 = new Dictionary<string, string>();
        灵修系数.Add("攻击", bm.Xitos(3));
        灵修系数.Add("防御", bm.Xitos(1));
        灵修系数.Add("血量", bm.Xitos(30));
        灵修系数.Add("攻击速度", bm.Xftos(1.2f));
        属性系数.Add("基础", 基础系数);
        属性系数.Add("剑修", 剑修系数);
        属性系数.Add("体修", 体修系数);
        属性系数.Add("灵修", 灵修系数);
        return 属性系数;
    }

    public float 返回属性系数(string index) {
        Dictionary<string, Dictionary<string, string>> 属性系数 = 返回属性系数表();
        role_Data myData = io.GetInstance().load();
        string 职业 = myData.职业["职业"] == "" ? "基础" : myData.职业["职业"];
        if (index.Equals("攻击") || index.Equals("防御") || index.Equals("血量") || index.Equals("攻击速度"))
        {
            return bm.Xstof(属性系数[职业][index]);
        }
        else {
            Debug.Log("错误的属性index");
            return 0;
        }
    }

    public void 储存缓存数据() {
        Debug.Log("进行储存");
        if (临时物品列表.Count != 0 || 临时经验与钱币.Count != 0) {
            pm.获取多个物品(临时物品列表);
            pm.获取缓存经验金钱(临时经验与钱币);
        }
    }

    public void 计数() {
        index++;
        if (index >= 50)
        {
            储存缓存数据();
            index = 0;
        }
    }


    public void 本地对象的添加(string 对象名字,GameObject 对象) {
        if (本地对象.ContainsKey(对象名字))
        {
            本地对象[对象名字] = 对象;
        }
        else {
            本地对象.Add(对象名字, 对象);
        }
    }


    public void 添加临时物品列表(Dictionary<Prop_bascis, int> 掉落列表) {
        foreach (Prop_bascis 物品 in 掉落列表.Keys) {
            if (临时物品列表.ContainsKey(物品))
            {
                临时物品列表[物品] += 掉落列表[物品];
            }
            else {
                临时物品列表.Add(物品, 掉落列表[物品]);
            }
        }
    }

    public void 属性相加(Dictionary<string, string> 待加载属性, Dictionary<string, string> 额外属性)
    {
        foreach (string 属性名 in 额外属性.Keys)
        {
            if (属性名.Equals("攻击力"))
               待加载属性["攻击力"] = bm.Xitos(bm.Xstoi(待加载属性["攻击力"]) + bm.Xstoi(额外属性["攻击力"]));
            else if (属性名.Equals("防御力"))
               待加载属性["防御力"] = bm.Xitos(bm.Xstoi(待加载属性["防御力"]) + bm.Xstoi(额外属性["防御力"]));
            else if (属性名.Equals("血量"))
               待加载属性["血量"] = bm.Xitos(bm.Xstoi(待加载属性["血量"]) + bm.Xstoi(额外属性["血量"]));
            else if (属性名.Equals("回血值"))
               待加载属性["回血值"] = bm.Xitos(bm.Xstoi(待加载属性["回血值"]) + bm.Xstoi(额外属性["回血值"]));
            else if (属性名.Equals("攻击力加成"))
               待加载属性["攻击力加成"] = bm.Xitos(bm.Xstoi(待加载属性["攻击力加成"]) + bm.Xstoi(额外属性["攻击力加成"]));
            else if (属性名.Equals("防御力加成"))
               待加载属性["防御力加成"] = bm.Xitos(bm.Xstoi(待加载属性["防御力加成"]) + bm.Xstoi(额外属性["防御力加成"]));
            else if (属性名.Equals("血量加成"))
               待加载属性["血量加成"] = bm.Xitos(bm.Xstoi(待加载属性["血量加成"]) + bm.Xstoi(额外属性["血量加成"]));
            else if (属性名.Equals("回血值加成"))
               待加载属性["回血值加成"] = bm.Xitos(bm.Xstoi(待加载属性["回血值加成"]) + bm.Xstoi(额外属性["回血值加成"]));
            else if (属性名.Equals("固定伤害"))
               待加载属性["固定伤害"] = bm.Xitos(bm.Xstoi(待加载属性["固定伤害"]) + bm.Xstoi(额外属性["固定伤害"]));
            else if (属性名.Equals("伤害加成"))
               待加载属性["伤害加成"] = bm.Xitos(bm.Xstoi(待加载属性["伤害加成"]) + bm.Xstoi(额外属性["伤害加成"]));
            else if (属性名.Equals("固定减伤"))
               待加载属性["固定减伤"] = bm.Xitos(bm.Xstoi(待加载属性["固定减伤"]) + bm.Xstoi(额外属性["固定减伤"]));
            else if (属性名.Equals("伤害减免"))
               待加载属性["伤害减免"] = bm.Xitos(bm.Xstoi(待加载属性["伤害减免"]) + bm.Xstoi(额外属性["伤害减免"]));
            else if (属性名.Equals("固定吸血"))
               待加载属性["固定吸血"] = bm.Xitos(bm.Xstoi(待加载属性["固定吸血"]) + bm.Xstoi(额外属性["固定吸血"]));
            else if (属性名.Equals("吸血加成"))
               待加载属性["吸血加成"] = bm.Xitos(bm.Xstoi(待加载属性["吸血加成"]) + bm.Xstoi(额外属性["吸血加成"]));
            else if (属性名.Equals("暴击率"))
               待加载属性["暴击率"] = bm.Xitos(bm.Xstoi(待加载属性["暴击率"]) + bm.Xstoi(额外属性["暴击率"]));
            else if (属性名.Equals("金钱加成"))
               待加载属性["金钱加成"] = bm.Xitos(bm.Xstoi(待加载属性["金钱加成"]) + bm.Xstoi(额外属性["金钱加成"]));
            else if (属性名.Equals("经验加成"))
               待加载属性["经验加成"] = bm.Xitos(bm.Xstoi(待加载属性["经验加成"]) + bm.Xstoi(额外属性["经验加成"]));
            else if (属性名.Equals("攻击速度"))
               待加载属性["攻击速度"] = bm.Xitos(bm.Xstoi(待加载属性["攻击速度"]) + bm.Xstoi(额外属性["攻击速度"]));
            else if (属性名.Equals("移动速度"))
               待加载属性["移动速度"] = bm.Xitos(bm.Xstoi(待加载属性["移动速度"]) + bm.Xstoi(额外属性["移动速度"]));
            else if (属性名.Equals("稀有怪概率"))
               待加载属性["稀有怪概率"] = bm.Xitos(bm.Xstoi(待加载属性["稀有怪概率"]) + bm.Xstoi(额外属性["稀有怪概率"]));
            else if (属性名.Equals("爆率"))
               待加载属性["爆率"] = bm.Xitos(bm.Xstoi(待加载属性["爆率"]) + bm.Xstoi(额外属性["爆率"]));


        }
    }



    public void 加载额外属性(combat cb) {
        Dictionary<string, string> 待加载属性 = basicMgr.GetInstance().返回空的战斗属性();
        if (cb!=null&&cb.gameObject.CompareTag("人物"))
        {
            装备属性 = PropMgr.GetInstance().加载装备配置(cb);//先给--属性--赋值
            SkillMgr.GetInstance().加载技能配置();
            光环属性 = UtilMaGr.GetInstance().加载光环属性();
            天赋属性 = TianFuMgr.GetInstance().加载天赋属性();
            属性相加(待加载属性, 光环属性);
            属性相加(待加载属性, 装备属性);
            属性相加(待加载属性, 技能属性);
            属性相加(待加载属性, 天赋属性);
        }
        cb.属性刷新(待加载属性);
    }

    public void 加载临时属性(combat cb) {
        cb.加载临时属性();
         
    }


  
}
