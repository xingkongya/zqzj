﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class m_linhaiguandao : MonoBehaviour, I_monster
{

    //自定义
    private UT_monster utm;
    private G_Util utg;


    private void Awake()
    {
        utm = gameObject.GetComponent<UT_monster>();
        utg = NameMgr.画布.GetComponent<G_Util>();
        utm.当前等级 = 45;

    }

    private void Start()
    {
        utg.移动与坐标.Add("上", "临海城");
        utg.移动与坐标.Add("下", "噩梦之森");
        utg.刷新移动与坐标();
        utg.生成场景怪物信息方法(this);

        // 技能与粒度.Clear();
    }

    public bool 怪物1赋值(combat cb)
    {
        utm.怪物名字 = "机关守卫";
        utm.怪物品质 = 0;
        utm.是否主动攻击 = false;
        utm.最低等级 = 40;
        utm.最高等级 = 45;
        utm.基础攻击力 = 100;
        utm.基础防御力 = 0;
        utm.基础血量 = 620;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 25;
        utm.攻击力资质 = 0.45f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.7f;
        utm.成长 = 1.0f;
        utm.攻击速度 = 1.2f;
        utm.基础经验值 = 1080;
        utm.经验值系数 = 3;
        utm.铜币 = (int)(5 * utm.成长);
        utm.添加基础掉落(cb,1);
        return true;
    }

    public bool 怪物2赋值(combat cb)
    {
        utm.怪物名字 = "游缴";
        utm.怪物品质 = 0;
        utm.是否主动攻击 = false;
        utm.最低等级 = 40;
        utm.最高等级 = 45;
        utm.基础攻击力 = 95;
        utm.基础防御力 = 0;
        utm.基础血量 = 660;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 30;
        utm.攻击力资质 = 0.4f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.75f;
        utm.成长 = 1.0f;
        utm.攻击速度 = 1f;
        utm.基础经验值 = 1100;
        utm.经验值系数 = 3;
        utm.铜币 = (int)(5 * utm.成长);
        utm.添加基础掉落(cb,1);
        return true;
    }

    public bool 怪物3赋值(combat cb)
    {
        utm.怪物名字 = "蟊贼";
        utm.怪物品质 = 1;
        utm.是否主动攻击 = false;
        utm.最低等级 = 40;
        utm.最高等级 = 45;
        utm.基础攻击力 = 90;
        utm.基础防御力 = 0;
        utm.基础血量 = 1180;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 80;
        utm.攻击力资质 = 0.5f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.9f;
        utm.成长 = 1.0f;
        utm.攻击速度 = 0.8f;
        utm.基础经验值 = 1800;
        utm.铜币 = (int)(10 * utm.成长);
        utm.添加基础掉落(cb, 2);
        utm.添加掉落(true, cb.掉落集合, "钱袋子",1);
        utm.掉落信息.Add("钱袋子");
        return true;
    }

    public bool 怪物4赋值(combat cb)
    {
        utm.怪物名字 = "【铁甲卫】";
        utm.怪物品质 = 3;
        utm.是否主动攻击 = false;
        utm.最低等级 = 45;
        utm.最高等级 = 45;
        utm.基础攻击力 = 120;
        utm.基础防御力 = 150;
        utm.基础血量 = 2800;
        utm.基础暴击率 = 10;
        utm.基础回血值 = 100;
        utm.攻击力资质 = 0.7f;
        utm.防御力资质 = 0.8f;
        utm.血量资质 = 1.2f;
        utm.成长 = 1.0f;
        utm.攻击速度 = 2.0f;
        utm.基础经验值 = 4800;
        utm.经验值系数 = 25;
        utm.铜币 = (int)(50 * utm.成长);
        utm.添加掉落(utg.概率(1, 5), cb.掉落集合, "百战战戟",1);
        utm.添加掉落(utg.概率(1, 5), cb.掉落集合, "百战铁甲",1);
        utm.添加掉落(utg.概率(1, 10), cb.掉落集合, "5仙晶卡",1);
        utm.添加基础掉落(cb, 4);
        utm.掉落信息.Add("5仙晶卡");
        utm.掉落信息.Add("百战战戟");
        utm.掉落信息.Add("百战铁甲");
        return true;
    }

    public bool 怪物5赋值(combat cb)
    {
        return false;
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
