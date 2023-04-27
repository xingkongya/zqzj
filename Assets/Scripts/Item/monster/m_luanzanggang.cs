using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class m_luanzanggang : MonoBehaviour, I_monster
{

    //自定义
    private UT_monster utm;
    private G_Util utg;
    private GameObject 祭坛;


    private void Awake()
    {
        utm = gameObject.GetComponent<UT_monster>();
        utg = NameMgr.画布.GetComponent<G_Util>();
        祭坛 = GameObject.Find("祭坛");
        utm.当前等级 = 55;

    }



    private void Start()
    {
        utg.移动与坐标.Add("上", "临海西郊");
        utg.刷新移动与坐标();
        utg.生成场景怪物信息方法(this);

        // 技能与粒度.Clear();
    }

    public void 祭坛对话索引()
    {
        祭坛选择项();
    }

    private void 祭坛选择项()
    {
        Dictionary<string, UnityAction> 选项信息 = new Dictionary<string, UnityAction>();
        选项信息.Add("尸王秘境", 进入秘境);
        utg.生成对话框("尸王秘境:50级副本.\n怪物成长:5.\n进入尸王秘境需要3块神秘石头", 0, 0.05f, "祭坛");
        utg.生成选项框(选项信息, 祭坛);
    }

    private void 进入秘境() {
        utg.关闭杂项();
        utg.跳转场景("尸王秘境");
    }

    public bool 怪物1赋值(combat cb)
    {
        utm.怪物名字 = "幽魂";
        utm.怪物品质 = 0;
        utm.是否主动攻击 = false;
        utm.最低等级 = 50;
        utm.最高等级 = 55;
        utm.基础攻击力 = 130;
        utm.基础防御力 = 0;
        utm.基础血量 = 800;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 45;
        utm.攻击力资质 = 0.6f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.8f;
        utm.成长 = 3f;
        utm.攻击速度 = 1.1f;
        utm.基础经验值 = 2600;
        utm.经验值系数 = 3;
        utm.铜币 = (int)(5 * utm.成长);
        utm.添加基础掉落(cb,1);
        return true;
    }

    public bool 怪物2赋值(combat cb)
    {
        utm.怪物名字 = "行尸";
        utm.怪物品质 = 0;
        utm.是否主动攻击 = false;
        utm.最低等级 = 50;
        utm.最高等级 = 55;
        utm.基础攻击力 = 125;
        utm.基础防御力 = 0;
        utm.基础血量 = 850;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 50;
        utm.攻击力资质 = 0.6f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.9f;
        utm.成长 = 3f;
        utm.攻击速度 = 1.4f;
        utm.基础经验值 = 2600;
        utm.经验值系数 = 3;
        utm.铜币 = (int)(5 * utm.成长);
        utm.添加基础掉落(cb,1);
        return true;
    }

    public bool 怪物3赋值(combat cb)
    {
        utm.怪物名字 = "魑魅";
        utm.怪物品质 = 1;
        utm.是否主动攻击 = false;
        utm.最低等级 = 50;
        utm.最高等级 = 55;
        utm.基础攻击力 = 150;
        utm.基础防御力 = 0;
        utm.基础血量 = 1360;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 0;
        utm.攻击力资质 = 0.55f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 1.1f;
        utm.成长 = 3f;
        utm.攻击速度 = 0.9f;
        utm.基础经验值 = 3800;
        utm.经验值系数 = 3;
        utm.铜币 = (int)(10 * utm.成长);
        utm.添加基础掉落(cb, 2);
        return true;
    }

    public bool 怪物4赋值(combat cb)
    {
        utm.怪物名字 = "魍魉";
        utm.怪物品质 = 1;
        utm.是否主动攻击 = false;
        utm.最低等级 = 50;
        utm.最高等级 = 55;
        utm.基础攻击力 = 150;
        utm.基础防御力 = 0;
        utm.基础血量 = 1460;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 0;
        utm.攻击力资质 = 0.8f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.95f;
        utm.成长 = 3f;
        utm.攻击速度 = 0.9f;
        utm.基础经验值 = 3800;
        utm.经验值系数 = 3;
        utm.铜币 = (int)(10 * utm.成长);
        utm.添加基础掉落(cb, 2);
        return true;
    }

    public bool 怪物5赋值(combat cb)
    {
        utm.怪物名字 = "铜甲尸";
        utm.怪物品质 = 3;
        utm.是否主动攻击 = false;
        utm.最低等级 = 55;
        utm.最高等级 = 55;
        utm.基础攻击力 = 220;
        utm.基础防御力 = 50;
        utm.基础血量 = 4500;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 0;
        utm.攻击力资质 = 0.9f;
        utm.防御力资质 = 0.3f;
        utm.血量资质 = 1.2f;
        utm.成长 = 3f;
        utm.攻击速度 = 1.4f;
        utm.基础经验值 = 12800;
        utm.经验值系数 = 20;
        utm.铜币 = (int)(50 * utm.成长);
        cb.技能与粒度.Clear();//赋值前先初始化
        if (!cb.技能与粒度.ContainsKey("嗜血"))
            cb.技能与粒度.Add("嗜血", 0.4f);
        if (!cb.技能与粒度.ContainsKey("獠牙"))
            cb.技能与粒度.Add("獠牙", 0.5f);
        utm.添加掉落(utg.概率(1, 10), cb.掉落集合, "5仙晶卡",1);
        utm.添加基础掉落(cb, 4);
        utm.掉落信息.Add("5仙晶卡");
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
}
