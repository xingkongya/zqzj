using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TianFuMgr : BaseManager<TianFuMgr>
{
    DataMgr dm = DataMgr.GetInstance();
    basicMgr bm = basicMgr.GetInstance();
    io io_ = io.GetInstance();
    PropMgr pm = PropMgr.GetInstance();
    public Dictionary<string, string> 临时天赋属性 = basicMgr.GetInstance().返回空的战斗属性();
    public GameObject user;

    public Dictionary<string, Action> 加载天赋表()
    {
        Dictionary<string, Action> 天赋效果表 = new Dictionary<string, Action>();
        天赋效果表.Add("强化骨骼", 强化骨骼);
        天赋效果表.Add("强化皮肤", 强化皮肤);
        天赋效果表.Add("强化呼吸", 强化呼吸);
        天赋效果表.Add("强化血液", 强化血液);
        天赋效果表.Add("修炼拳脚", 修炼拳脚);
        天赋效果表.Add("修炼体魄", 修炼体魄);
        天赋效果表.Add("修炼血脉", 修炼血脉);
        天赋效果表.Add("修炼灵识", 修炼灵识);
        天赋效果表.Add("琉璃金身", 琉璃金身);
        天赋效果表.Add("蛮牛之体", 蛮牛之体);
        天赋效果表.Add("九牛二虎", 九牛二虎);
        天赋效果表.Add("枯木逢春", 枯木逢春);
        天赋效果表.Add("势不可挡", 势不可挡);
        天赋效果表.Add("不动如山", 不动如山);
        天赋效果表.Add("雷霆万钧", 雷霆万钧);
        天赋效果表.Add("蹑影追风", 蹑影追风);
        天赋效果表.Add("锋锐", 锋锐);
        天赋效果表.Add("坚固", 坚固);
        天赋效果表.Add("饮血", 饮血);
        天赋效果表.Add("爆裂", 爆裂);
        天赋效果表.Add("疾风意境", 剑快剑);
        天赋效果表.Add("无限剑制", 无限剑制);
        天赋效果表.Add("剑意", 剑意);
        天赋效果表.Add("剑势", 剑势);
        天赋效果表.Add("心剑", 心剑);


        return 天赋效果表;
    }


    public Dictionary<string, string> 加载天赋属性() {
        临时天赋属性 = basicMgr.GetInstance().返回空的战斗属性();
        Dictionary<string, Action> 天赋表 = 加载天赋表();
        foreach (string TfName in 天赋表.Keys) {
            TianFu 天赋 = 返回天赋(TfName);
            if (天赋!=null&&天赋.type.Equals("被动增益")) {
                天赋表[TfName]();
            }
        }
        return 临时天赋属性;

    }


    public TianFu 返回天赋(string TfName)
    {
        if (PropMgr.天赋表.ContainsKey(TfName))
        {
            return PropMgr.天赋表[TfName];
        }
        else
        {
            Debug.Log("错误,没有该天赋--"+TfName);
            return null;
        }
    }


    /// <summary>
    /// type 只能是"职业"或者"天赋"
    /// </summary>
    /// <param name="TfName"></param>
    /// <param name="type"></param>
    public void 天赋升级_Dao(TianFu tf,string type) {
        role_Data myData = io_.load();
        if (bm.字符串字典返回(myData.天赋[type], tf.name).Equals("")) {
            myData.天赋[type].Add(tf.name, bm.Xitos(0));
        }
        myData.天赋[type][tf.name] = bm.Xitos(bm.Xstoi(myData.天赋[type][tf.name]) + 1);
        if (tf.type.Equals("被动")&&!myData.被动技能["职业"].Contains(tf.name)) {
            myData.被动技能["职业"].Add(tf.name);
        }
        io_.save(myData);
    }


    /// <summary>
    /// type 只能是"职业"或者"天赋"
    /// </summary>
    /// <param name="TfName"></param>
    /// <param name="type"></param>
    public void 天赋升满级_Dao(TianFu tf, string type,int num)
    {
        role_Data myData = io_.load();
        if (bm.字符串字典返回(myData.天赋[type], tf.name).Equals(""))
        {
            myData.天赋[type].Add(tf.name, bm.Xitos(0));
        }
        myData.天赋[type][tf.name] = bm.Xitos(bm.Xstoi(myData.天赋[type][tf.name]) + num);
        if (tf.type.Equals("被动") && !myData.被动技能["职业"].Contains(tf.name))
        {
            myData.被动技能["职业"].Add(tf.name);
        }
        io_.save(myData);
    }


    public int 返回存档该天赋等级(TianFu tf) {
        role_Data myData = io_.load();
        if (myData.天赋[tf.place].ContainsKey(tf.name))
        {
            return bm.Xstoi(myData.天赋[tf.place][tf.name]);
        }
        else {
            return 0;
        }
    }


    public void 消耗技能点(TianFu tf) {
        role_Data myData = io_.load();
        if (tf.comment.Equals("初级"))
        {
            myData.天赋["职业"]["基础技能点"] = bm.Xitos(bm.Xstoi(myData.天赋["职业"]["基础技能点"]) - 1);
        }
        else if(tf.comment.Equals("高级"))
        {
            myData.天赋["职业"]["高级技能点"] = bm.Xitos(bm.Xstoi(myData.天赋["职业"]["高级技能点"]) - 1);
        }
        io_.save(myData);
    }

    public void 消耗任意技能点(TianFu tf,int num)
    {
        role_Data myData = io_.load();
        if (tf.name.IndexOf("修炼") != -1)
        {
            myData.天赋["职业"]["基础技能点"] = bm.Xitos(bm.Xstoi(myData.天赋["职业"]["基础技能点"]) - num);
        }
        else
        {
            myData.天赋["职业"]["高级技能点"] = bm.Xitos(bm.Xstoi(myData.天赋["职业"]["高级技能点"]) - num);
        }
        io_.save(myData);
    }

    public void 天赋加载图标(GameObject Img, TianFu Tf)
    {
        if (Tf.place.Equals("职业"))
        {
            Img.GetComponent<Image>().sprite = Resources.Load<Sprite>("图片/" + Tf.icon);
        }
        else
        {
            //Img.GetComponent<Image>().sprite = bm.GetChildSprite("技能", int.Parse(Tf.icon));
            Img.GetComponent<Image>().sprite = Resources.Load<Sprite>("图片/" + Tf.icon);
        }
    }

    public TianFu 被动增益介绍(TianFu tf,string tfName,string type,float 系数,string 单位,string 属性名) {
        role_Data myData = io_.load();
        string str_等级 = bm.字符串字典返回(myData.天赋[type], tfName);
        if (str_等级.Equals("")) {
            str_等级 = bm.Xitos(0);
        }
        tf.effect = "效果:提升<color=orange>" + (bm.Xstoi(str_等级) * 系数) +"</color>"+ 单位 + 属性名;
        tf.next = "下一级:提升<color=orange>" + ((bm.Xstoi(str_等级) +1) * 系数) + "</color>" + 单位 + 属性名;
        return tf;
    }


    public TianFu 刷新介绍(TianFu tf)
    {

        role_Data myData = io_.load();

        if (tf.name.Equals("强化骨骼") )
        {
           tf= 被动增益介绍(tf,"强化骨骼","天赋",2,"点","攻击力");
        }
        else if (tf.name.Equals("强化皮肤"))
        {
            tf = 被动增益介绍(tf, "强化皮肤", "天赋", 2, "点", "防御力");
        }
        else if (tf.name.Equals("强化血液"))
        {
            tf = 被动增益介绍(tf, "强化血液", "天赋", 30, "点", "血量");
        }
        else if (tf.name.Equals("强化呼吸"))
        {
            tf = 被动增益介绍(tf, "强化呼吸", "天赋", 2, "点", "回血");
        }
        else if (tf.name.Equals("锋锐"))
        {
            tf = 被动增益介绍(tf, "锋锐", "天赋", 3, "点", "固定伤害");
        }
        else if (tf.name.Equals("坚固"))
        {
            tf = 被动增益介绍(tf, "坚固", "天赋", 3, "点", "固定减伤");
        }
        else if (tf.name.Equals("爆裂"))
        {
            tf = 被动增益介绍(tf, "爆裂", "天赋", 0.5f, "%", "暴击伤害");
        }
        else if (tf.name.Equals("饮血"))
        {
            tf = 被动增益介绍(tf, "饮血", "天赋", 0.5f, "点", "固定吸血");
        }
        else if (tf.name.Equals("修炼拳脚"))
        {
            tf = 被动增益介绍(tf, "修炼拳脚", "职业", 4, "点", "攻击力");
        }
        else if (tf.name.Equals("修炼体魄"))
        {
            tf = 被动增益介绍(tf, "修炼体魄", "职业", 2, "点", "防御力");
        }
        else if (tf.name.Equals("修炼血脉"))
        {
            tf = 被动增益介绍(tf, "修炼血脉", "职业", 50, "点", "血量");
        }
        else if (tf.name.Equals("修炼灵识"))
        {
            tf = 被动增益介绍(tf, "修炼灵识", "职业", 5, "点", "回血");
        }
        else if (tf.name.Equals("九牛二虎"))
        {
            tf = 被动增益介绍(tf, "九牛二虎", "天赋", 0.5f, "%", "攻击力加成");
        }
        else if (tf.name.Equals("琉璃金身"))
        {
            tf = 被动增益介绍(tf, "琉璃金身", "天赋", 0.5f, "%", "防御力加成");
        }
        else if (tf.name.Equals("蛮牛之体"))
        {
            tf = 被动增益介绍(tf, "蛮牛之体", "天赋", 0.5f, "%", "血量加成");
        }
        else if (tf.name.Equals("枯木逢春"))
        {
            tf = 被动增益介绍(tf, "枯木逢春", "天赋", 0.5f, "%", "回血加成");
        }
        else if (tf.name.Equals("势不可挡"))
        {
            tf = 被动增益介绍(tf, "势不可挡", "天赋", 0.5f, "%", "伤害加成");
        }
        else if (tf.name.Equals("不动如山"))
        {
            tf = 被动增益介绍(tf, "不动如山", "天赋", 0.5f, "%", "伤害减免");
        }
        else if (tf.name.Equals("蹑影追风"))
        {
            tf = 被动增益介绍(tf, "蹑影追风", "天赋", 0.5f, "%", "攻击速度");
        }
        else if (tf.name.Equals("雷霆万钧"))
        {
            tf = 被动增益介绍(tf, "雷霆万钧", "天赋", 0.3f, "%", "暴击率");
        }
        else if (tf.name.Equals("疾风意境"))
        {
            tf = 被动增益介绍(tf, "疾风意境", "职业", 10, "%", "攻击速度");
        }
        else if (tf.name.Equals("剑意"))
        {
            string str_等级 = bm.字符串字典返回(myData.天赋["职业"], "剑意");
            tf.effect = "效果:攻击时有<color=orange>" + (14+bm.Xstoi(str_等级) * 1) + "</color>%的概率,额外造成<color=orange>" + (50 + bm.Xstoi(str_等级) * 5)+ "</color>%的技能伤害";
            tf.next = "下一级:攻击时有<color=orange>" + (14 + (bm.Xstoi(str_等级) +1) * 1) + "</color>%的概率,额外造成<color=orange>" + (50 + (bm.Xstoi(str_等级) +1) * 5) + "</color>%的技能伤害";
        }
        else if (tf.name.Equals("剑势"))
        {
            string str_等级 = bm.字符串字典返回(myData.天赋["职业"], "剑势");
            tf.effect = "效果:攻击时有" + 33 + "%的概率,获得一层剑势,每层剑势+5%的伤害加成,buff持续3秒.最高可叠加到<color=orange>" + (4 + bm.Xstoi(str_等级) * 1f) + "</color>层";
            tf.next = "下一级:攻击时有" + 33 + "%的概率,获得一层剑势,每层剑势+5%的伤害加成,buff持续3秒.最高可叠加到<color=orange>" + (4 + (bm.Xstoi(str_等级)+1) * 1f) + "</color>层";
        }
        else if (tf.name.Equals("心剑"))
        {
            string str_等级 = bm.字符串字典返回(myData.天赋["职业"], "心剑");
            tf.effect = "效果:攻击时有" + (9 + 1f) + "%的概率,获得+攻击力*<color=orange>" + (30 + bm.Xstoi(str_等级) * 3) + "</color>%点固定伤害的buff,持续3秒,不可叠加";
            tf.next = "下一级:攻击时有" + (9 + 1f) + "%的概率,获得+攻击力*<color=orange>" + (30 + (bm.Xstoi(str_等级) + 1) * 3) + "</color>%点固定伤害的buff,持续3秒,不可叠加";
        }
        else if (tf.name.Equals("无限剑制"))
        {
            string str_等级 = bm.字符串字典返回(myData.天赋["职业"], "无限剑制");
            tf.effect = "效果:每次攻击都会获得+<color=orange>" + (0.5f+ bm.Xstoi(str_等级) * 0.5f) + "</color>%攻击速度buff,持续1秒.可无限叠加";
            tf.next = "下一级:每次攻击都会获得+<color=orange>" + (0.5f+(bm.Xstoi(str_等级)+1) * 0.5f) + "</color>%攻击速度buff,持续1秒.可无限叠加";
        }


        if (返回存档该天赋等级(tf) == 0)
        {
            tf.effect = "效果:未激活";
        }

        if (!tf.MaxLv.Equals("无限")&&返回存档该天赋等级(tf) >= int.Parse(tf.MaxLv))
        {
            tf.next = "已满级";
        }

        return tf;
    }


    public void 被动增益(string 天赋名字, string 天赋类型, string 增益属性,float 增益比例) {
        role_Data myData = io_.load();
        if (myData.天赋[天赋类型].ContainsKey(天赋名字))
        {
            临时天赋属性[增益属性] = bm.Xftos(bm.Xstoi(临时天赋属性[增益属性]) + bm.Xstoi(myData.天赋[天赋类型][天赋名字]) * 增益比例);
        }
        else {
            myData.天赋[天赋类型].Add(天赋名字, bm.Xitos(0));
            io_.save(myData);
        }
        user = null;
    }


    public void 刷新核心技能()
    {
        role_Data myData = io_.load();
        if (myData.职业["职业"].Equals("剑修"))
        {
            bm.字符串字典自适应添加键值对(myData.天赋["职业"], "无限剑制", bm.Xstoi(myData.等级) / 100 + 1 + "");
            myData.被动技能["职业"].Add("无限剑制");
        }//待补充

        io_.save(myData);
    }


    public void 强化骨骼()
    {
        被动增益("强化骨骼", "天赋", "攻击力", 2f);
    }


   
    public void 强化皮肤()
    {
        被动增益("强化皮肤", "天赋", "防御力", 1f);
    }


    public void 强化血液()
    {
        被动增益("强化血液", "天赋", "血量", 25f);
    }


    public void 强化呼吸()
    {
        被动增益("强化呼吸", "天赋", "回血值", 3f);
    }


    public void 修炼拳脚()
    {
        被动增益("修炼拳脚", "职业", "攻击力", 4f);
    }

    public void 修炼体魄()
    {
        被动增益("修炼体魄", "职业", "防御力", 2f);
    }

    public void 修炼血脉()
    {
        被动增益("修炼血脉", "职业", "血量", 50f);
    }

    public void 修炼灵识()
    {
        被动增益("修炼灵识", "职业", "回血值", 5f);
    }


    public void 蛮牛之体()
    {
        被动增益("蛮牛之体", "天赋", "血量加成", 0.5f);
    }
    public void 琉璃金身()
    {
        被动增益("琉璃金身", "天赋", "防御力加成", 0.5f);
    }

    public void 九牛二虎()
    {
        被动增益("九牛二虎", "天赋", "攻击力加成", 0.5f);
    }


    public void 枯木逢春()
    {
        被动增益("枯木逢春", "天赋", "回血值加成", 0.5f);
    }


    public void 势不可挡()
    {
        被动增益("势不可挡", "天赋", "攻击力加成", 0.5f);
    }


    public void 不动如山()
    {
        被动增益("不动如山", "天赋", "伤害减免", 0.5f);
    }

    public void 雷霆万钧()
    {
        被动增益("雷霆万钧", "天赋", "暴击率", 0.3f);
    }

    public void 蹑影追风()
    {
        被动增益("蹑影追风", "天赋", "攻击速度", 0.5f);
    }


    public void 锋锐()
    {
        被动增益("锋锐", "天赋", "固定伤害", 3f);
    }

    public void 坚固()
    {
        被动增益("坚固", "天赋", "固定减伤", 3f);
    }

    public void 饮血()
    {
        被动增益("饮血", "天赋", "固定吸血", 0.5f);
    }

    public void 爆裂()
    {
        被动增益("爆裂", "天赋", "暴伤加成", 0.5f);
    }

    public void 剑快剑()
    {
        被动增益("疾风意境", "职业", "攻击速度", 10f);
    }

    public void 剑意()
    {
        G_Util gut = NameMgr.画布.GetComponent<G_Util>();
        int 天赋等级 = 返回存档该天赋等级(返回天赋("剑意"));
        if (gut.概率(14 + 天赋等级 * 1, 100)) {
            combat cb = DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>();
            int 伤害 = (int)(bm.Xstoi(cb.攻击力) * (0.5f+0.05*天赋等级) );//这里改参数
            gut.被动技能伤害(伤害);
            gut.生成技能框(cb.gameObject,"被动:剑意");
        }
    }

    public void 剑势()
    {
        G_Util gut = NameMgr.画布.GetComponent<G_Util>();
        int 天赋等级 = 返回存档该天赋等级(返回天赋("剑势"));
        if (gut.概率(33, 100))
        {
            combat cb = DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>();
            gut.获得buff(cb, "剑势", new BuffData("剑势", "伤害加成", 5, 1, 4 + 天赋等级, 3f, true));
            //gut.提升属性(cb, "伤害加成", (30 + 天赋等级 * 3), 3);
            //gut.生成技能特效_攻击(cb.gameObject, 3);
            gut.生成技能框(cb.gameObject, "被动:剑势");
        }
    }

    public void 无限剑制()
    {
        G_Util gut = NameMgr.画布.GetComponent<G_Util>();
        int 天赋等级 = 返回存档该天赋等级(返回天赋("无限剑制"));
        combat cb = DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>();
        gut.获得buff(cb, "无限剑制", new BuffData("无限剑制", "攻击速度", 0.5f+天赋等级 * 0.5f, 1, 999,1f, true));
        //gut.提升属性(cb, "攻击速度", (天赋等级 * 0.5f), 1);
        //gut.生成技能框(cb.gameObject, "无限剑制");

    }

    public void 心剑()
    {
        G_Util gut = NameMgr.画布.GetComponent<G_Util>();
        int 天赋等级 = 返回存档该天赋等级(返回天赋("心剑"));
        if (gut.概率(10, 100))
        {
            combat cb = DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>();
            gut.获得buff(cb,"心剑",new BuffData("心剑","固定伤害", 20 + 天赋等级 * 2,1 ,1,3f,false));
            //gut.提升属性(cb, "固定伤害", (20 + 天赋等级 * 2), 3);
            //gut.生成技能特效_攻击(cb.gameObject, 3);
            gut.生成技能框(cb.gameObject, "被动:心剑");
        }
    }





}
