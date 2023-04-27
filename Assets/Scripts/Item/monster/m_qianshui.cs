using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class m_qianshui : MonoBehaviour, I_monster
{


    //自定义
    private UT_monster utm;
    private G_Util utg;
    private EventCenter ec;

    private void Awake()
    {
        ec = EventCenter.GetInstance();
        utm = gameObject.GetComponent<UT_monster>();
        utg = NameMgr.画布.GetComponent<G_Util>();
        utm.当前等级 = 50;

    }

    private void OnEnable()
    {
        ec.AddEventListener<combat>("怪物失败", 地图显示检索);
    }


    private void 地图显示检索(combat cb) {
        G_Util gut = NameMgr.画布.GetComponent<G_Util>();
        Debug.Log(gut.现有怪物集合.Count);
        if (gut.现有怪物集合.Count==0) {
            副本地图显示();
        }
    }


    private void Start()
    {
        utg.移动与坐标.Clear();
        utg.刷新移动与坐标();

    }




    void 副本地图显示() {

        utg.副本步数++;
        if (utg.副本步数 == 1)
        {
            utg.移动与坐标.Add("下", "潜水");
            utg.移动与坐标.Add("上", "碎石滩");
        }
        else
        {
            if (utg.概率(1, 5))
                utg.移动与坐标.Add("上", "潜水");
            if (utg.概率(1, 3))
                utg.移动与坐标.Add("左", "潜水");
            if (utg.概率(1, 3))
                utg.移动与坐标.Add("右", "潜水");
            if (utg.概率(1, 100))
            {
                utg.移动与坐标.Add("上", "潜水");
                utg.移动与坐标.Add("下", "海底裂缝");
            }
            else
                utg.移动与坐标.Add("下", "潜水");
        }
        utg.刷新移动与坐标();
        utg.生成场景怪物信息方法(this);
        // 技能与粒度.Clear();
    }

    public bool 怪物1赋值(combat cb)
    {
        utm.怪物名字 = "海龟";
        utm.怪物品质 = 0;
        utm.是否主动攻击 = false;
        utm.最低等级 = 30;
        utm.最高等级 = 35;
        utm.基础攻击力 = 45;
        utm.基础防御力 = 0;
        utm.基础血量 = 230;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 30;
        utm.攻击力资质 = 0.5f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.65f;
        utm.成长 = 1f;
        utm.攻击速度 = 1.4f;
        utm.基础经验值 = 550;
        utm.经验值系数 = 3;
        utm.铜币 = (int)(5 * utm.成长);
        utm.添加基础掉落(cb,1);
        utm.添加掉落(utg.概率(1, 100), cb.掉落集合, "蛟血",1);
        utm.添加掉落(utg.概率(1, 800), cb.掉落集合, "神秘石头", 1);
        utm.掉落信息.Add("蛟血");
        return true;
    }

    public bool 怪物2赋值(combat cb)
    {
        utm.怪物名字 = "珊瑚虫";
        utm.怪物品质 = 0;
        utm.是否主动攻击 = false;
        utm.最低等级 = 35;
        utm.最高等级 = 40;
        utm.基础攻击力 = 60;
        utm.基础防御力 = 0;
        utm.基础血量 = 200;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 40;
        utm.攻击力资质 = 0.6f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.55f;
        utm.成长 = 1f;
        utm.攻击速度 = 0.9f;
        utm.基础经验值 = 1020;
        utm.经验值系数 = 3;
        utm.铜币 = (int)(5 * utm.成长);
        utm.添加基础掉落(cb,1);
        utm.添加掉落(utg.概率(1, 800), cb.掉落集合, "神秘石头", 1);
        utm.添加掉落(utg.概率(1, 80), cb.掉落集合, "蛟血",1);
        utm.掉落信息.Add("蛟血");
        utm.掉落信息.Add("神秘石头");
        return true;
    }

    public bool 怪物3赋值(combat cb)
    {
        utm.怪物名字 = "✦血珊瑚✧";
        utm.怪物品质 = 2;
        utm.是否主动攻击 = false;
        utm.最低等级 = 35;
        utm.最高等级 = 40;
        utm.基础攻击力 = 65;
        utm.基础防御力 = 35;
        utm.基础血量 = 1000;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 80;
        utm.攻击力资质 = 0.85f;
        utm.防御力资质 = 0.55f;
        utm.血量资质 = 1.1f;
        utm.成长 = 2f;
        utm.攻击速度 = 1.5f;
        utm.基础经验值 = 2800;
        utm.经验值系数 = 10;
        utm.铜币 = (int)(20 * utm.成长);

        if (!cb.技能与粒度.ContainsKey("吞噬"))
            cb.技能与粒度.Add("吞噬", 0.4f);
        if (!cb.技能与粒度.ContainsKey("磐石"))
            cb.技能与粒度.Add("磐石", 0.15f);

        utm.添加基础掉落(cb,1);
        utm.添加掉落(utg.概率(1, 100), cb.掉落集合, "神秘石头", 1);
        utm.添加掉落(true, cb.掉落集合, "蛟血",1);
        utm.添加掉落(utg.概率(1, 100), cb.掉落集合, "技能书<吞噬>",1);
        utm.掉落信息.Add("技能书<吞噬>");
        utm.添加掉落(utg.概率(1, 100), cb.掉落集合, "技能书<磐石>",1);
        utm.掉落信息.Add("技能书<磐石>");
        utm.掉落信息.Add("蛟血");
        utm.掉落信息.Add("神秘石头");
        return true;
    }

    public bool 怪物4赋值(combat cb)
    {
        utm.怪物名字 = "龙鱼";
        utm.怪物品质 = 3;
        utm.是否主动攻击 = false;
        utm.最低等级 = 45;
        utm.最高等级 = 45;
        utm.基础攻击力 = 120;
        utm.基础防御力 = 60;
        utm.基础血量 = 3500;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 100;
        utm.攻击力资质 = 0.9f;
        utm.防御力资质 = 0.5f;
        utm.血量资质 = 1.2f;
        utm.成长 = 2f;
        utm.攻击速度 = 1.2f;
        utm.基础经验值 = 18000;
        utm.经验值系数 = 30;
        utm.铜币 = (int)(50 * utm.成长);

        if (!cb.技能与粒度.ContainsKey("吞噬"))
            cb.技能与粒度.Add("吞噬", 0.4f);
        if (!cb.技能与粒度.ContainsKey("≮化龙≯"))
            cb.技能与粒度.Add("≮化龙≯", 0.08f);

        utm.添加基础掉落(cb,1);
        utm.添加掉落(utg.概率(1, 20), cb.掉落集合, "神秘石头", 1);
        utm.添加掉落(utg.概率(1, 100), cb.掉落集合, "技能书<吞噬>",1);
        utm.掉落信息.Add("技能书<吞噬>");
        utm.掉落信息.Add("神秘石头");
        return true;
    }

    public bool 怪物5赋值(combat cb)
    {
        utm.怪物名字 = "≮龙鱼王≯";
        utm.怪物品质 = 3;
        utm.是否主动攻击 = false;
        utm.最低等级 = 50;
        utm.最高等级 = 50;
        utm.基础攻击力 = 150;
        utm.基础防御力 = 100;
        utm.基础血量 = 5500;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 150;
        utm.攻击力资质 = 1f;
        utm.防御力资质 = 0.6f;
        utm.血量资质 = 1.4f;
        utm.成长 = 2f;
        utm.攻击速度 = 1.2f;
        utm.基础经验值 = 38000;
        utm.经验值系数 = 30;
        utm.铜币 = (int)(50 * utm.成长);

        if (!cb.技能与粒度.ContainsKey("吞噬"))
            cb.技能与粒度.Add("吞噬", 0.4f);
        if (!cb.技能与粒度.ContainsKey("≮化龙≯"))
            cb.技能与粒度.Add("≮化龙≯", 0.3f);
        if (!cb.技能与粒度.ContainsKey("治愈之水"))
            cb.技能与粒度.Add("治愈之水", 0.3f);

        utm.添加基础掉落(cb,1);
        utm.添加掉落(utg.概率(1, 5), cb.掉落集合, "神秘石头", 1);
        utm.添加掉落(utg.概率(1, 500), cb.掉落集合, "技能书<治愈之水>",1);
        utm.掉落信息.Add("技能书<治愈之水>");
        utm.添加掉落(utg.概率(1, 100), cb.掉落集合, "技能书<吞噬>",1);
        utm.掉落信息.Add("技能书<吞噬>");
        utm.添加掉落(utg.概率(1, 500), cb.掉落集合, "沧龙珠",1);
        utm.掉落信息.Add("沧龙珠");
        utm.掉落信息.Add("神秘石头");
        return true;
    }

    public bool 怪物6赋值(combat cb)
    {
        throw new NotImplementedException();
    }

    public bool 怪物7赋值(combat cb)
    {
        throw new NotImplementedException();
    }

    public bool 怪物8赋值(combat cb)
    {
        throw new NotImplementedException();
    }

    public bool 怪物9赋值(combat cb)
    {
        throw new NotImplementedException();
    }

    public bool 建筑赋值(combat cb)
    {
        throw new NotImplementedException();
    }

    private void OnDisable()
    {
        ec.RemoveEventListener<combat>("怪物失败", 地图显示检索);
    }
}
