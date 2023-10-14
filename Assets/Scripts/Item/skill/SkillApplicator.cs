using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillApplicator : BaseManager<SkillApplicator>
{
    public Dictionary<string, float> CD集合 = new Dictionary<string, float>();
    private io io_ = io.GetInstance();
    private PropMgr pm = PropMgr.GetInstance();
    private SkillEffect se = SkillEffect.GetInstance();
    public Dictionary<string, Action> 技能名称及其效果 = SkillEffect.GetInstance().加载技能效果();
    public Dictionary<string, Action<int>> 道具名称及其效果 = PropEffect.GetInstance().加载道具效果();
    private G_Util gut = NameMgr.画布.GetComponent<G_Util>();
    private basicMgr bm = basicMgr.GetInstance();







    public void 使用绝招()
    {
        role_Data myData = io_.load();
        gut = NameMgr.画布.GetComponent<G_Util>();
        string 绝招名 = myData.技能槽["1"];
        if (!绝招名.Equals(""))
        {
            SkillData sd = pm.检索技能(绝招名);
            if (!CD集合.ContainsKey("绝招"))
            {
                if (sd.get.Equals("攻击") && gut.现有怪物集合.Count == 0)
                    return;
                CD集合.Add("绝招", bm.Xstof(sd.cd));
                GameObject 人物 = DataMgr.GetInstance().本地对象["主角"];
                se.user = 人物;
                技能名称及其效果[绝招名]();
                gut.生成技能框(人物, sd.name);
            }
        }
    }


    public void 触发攻击后被动()
    {
        role_Data myData = io_.load();
        gut = NameMgr.画布.GetComponent<G_Util>();
        List<string> 职业被动 = myData.被动技能["职业"];
        Dictionary<string, Action> 天赋表 = TianFuMgr.GetInstance().加载天赋表();
        foreach (string tfName in 职业被动) {
            天赋表[tfName]();
        }
       
    }


    public void 使用CD道具()
    {
        role_Data myData = io_.load();
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (myData.记录.ContainsKey("CD道具"))
        {
            string 道具名 = myData.记录["CD道具"];
            Prop_bascis pb = pm.检索物品(道具名);
            if (!CD集合.ContainsKey("CD道具"))
            {
                if (pb.icon.Equals("暗器") && gut.现有怪物集合.Count == 0)//场景有怪物才能用暗器
                    return;
                CD集合.Add("CD道具", bm.Xstof(pb.cd));
                道具名称及其效果["CD道具-" + 道具名](1);
                gut.刷新背包CD道具UI(道具名);
            }
        }
    }


    public void 宠物或怪物使用绝招(GameObject user, string 绝招名)
    {
        role_Data myData = io_.load();
        gut = NameMgr.画布.GetComponent<G_Util>();
        combat cb = user.GetComponent<combat>();
        if (!绝招名.Equals(""))
        {
            SkillData sd = pm.检索技能(绝招名);
            if (!cb.技能与CD.ContainsKey(绝招名))
            {
                /* if (sd.get.Equals("攻击") && (cb.仇恨列表.Count == 0||(user.CompareTag("宠物")&&!myData.列表型记录["战斗设置"]["战斗设置"].Contains("宠物主动攻击"))))
                     return;*/
                cb.技能与CD.Add(绝招名, bm.Xstoi(sd.cd));
                se.user = user;
                技能名称及其效果[绝招名]();
                gut.生成技能框(user, sd.name);
            }
        }
    }

}
