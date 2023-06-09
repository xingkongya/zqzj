﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class m_huangyecaodi : MonoBehaviour, I_monster
{

    //自定义
    private UT_monster utm;
    private G_Util utg;

    private void Awake()
    {
        utm = gameObject.GetComponent<UT_monster>();
        utg = NameMgr.画布.GetComponent<G_Util>();
        utm.当前等级 = 30;

    }

    private void OnEnable()
    {

    }

    private void Start()
    {
        utg.移动与坐标.Add("右", "黄沙地");
        utg.移动与坐标.Add("下", "山丘桃林");
        utg.刷新移动与坐标();
        utg.生成场景怪物信息方法(this);
        // 技能与粒度.Clear();
    }

    public bool 怪物1赋值(combat cb)
    {
        utm.怪物名字 = "奔狼";
        utm.怪物品质 = 0;
        utm.是否主动攻击 = false;
        utm.最低等级 = 25;
        utm.最高等级 = 30;
        utm.基础攻击力 = 55;
        utm.基础防御力 = 0;
        utm.基础血量 = 320;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 10;
        utm.攻击力资质 = 0.35f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.45f;
        utm.成长 = 1f;
        utm.攻击速度 = 1.2f;
        utm.基础经验值 = 240;
        utm.经验值系数 = 3;
        utm.铜币 = (int)(5 * utm.成长);
        utm.添加基础掉落(cb,1);
        return true;
    }

    public bool 怪物2赋值(combat cb)
    {
        utm.怪物名字 = "暴熊";
        utm.怪物品质 = 0;
        utm.是否主动攻击 = false;
        utm.最低等级 = 25;
        utm.最高等级 = 30;
        utm.基础攻击力 = 50;
        utm.基础防御力 = 0;
        utm.基础血量 = 360;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 15;
        utm.攻击力资质 = 0.4f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.55f;
        utm.成长 = 1f;
        utm.攻击速度 = 1.5f;
        utm.基础经验值 = 250;
        utm.经验值系数 = 3;
        utm.铜币 = (int)(5 * utm.成长);
        utm.添加基础掉落(cb,1);
        return true;
    }

    public bool 怪物3赋值(combat cb)
    {
        utm.怪物名字 = "【猎鹰】";
        utm.怪物品质 = 0;
        utm.是否主动攻击 = false;
        utm.最低等级 = 25;
        utm.最高等级 = 30;
        utm.基础攻击力 = 45;
        utm.基础防御力 = 0;
        utm.基础血量 = 300;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 12;
        utm.攻击力资质 = 0.4f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.4f;
        utm.成长 = 1f;
        utm.攻击速度 = 0.9f;
        utm.基础经验值 = 320;
        utm.经验值系数 = 4;
        utm.铜币 = (int)(5 * utm.成长);
        utm.添加基础掉落(cb,1);
        utm.添加掉落(utg.概率(1, 5), cb.掉落集合, "飞羽",1);
        utm.掉落信息.Add("飞羽");
        return true;
    }

    public bool 怪物4赋值(combat cb)
    {
        utm.怪物名字 = "大地暴熊";
        utm.怪物品质 = 2;
        utm.是否主动攻击 = false;
        utm.最低等级 = 25;
        utm.最高等级 = 30;
        utm.基础攻击力 = 70;
        utm.基础防御力 = 15;
        utm.基础血量 = 750;
        utm.基础暴击率 = 10;
        utm.基础回血值 = 60;
        utm.攻击力资质 = 0.65f;
        utm.防御力资质 = 0.3f;
        utm.血量资质 = 0.8f;
        utm.成长 = 1f;
        utm.攻击速度 = 1.7f;
        utm.基础经验值 = 1500;
        utm.经验值系数 = 20;
        utm.铜币 = (int)(20 * utm.成长);
        utm.添加基础掉落(cb, 4);
        utm.添加掉落(utg.概率(1, 8), cb.掉落集合, "暴熊爪",1);
        utm.添加掉落(utg.概率(1, 8), cb.掉落集合, "暴熊衣",1);
        utm.添加掉落(utg.概率(1, 8), cb.掉落集合, "暴熊颅",1);
        utm.添加掉落(utg.概率(1, 8), cb.掉落集合, "暴熊足",1);
        utm.掉落信息.Add("暴熊爪");
        utm.掉落信息.Add("暴熊衣");
        utm.掉落信息.Add("暴熊颅");
        utm.掉落信息.Add("暴熊足");
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
