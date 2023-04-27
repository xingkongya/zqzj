using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class m_shiwangmijing : MonoBehaviour, I_monster
{


    //自定义
    private UT_monster utm;
    private G_Util utg;

    private void Awake()
    {
        utm = gameObject.GetComponent<UT_monster>();
        utg = NameMgr.画布.GetComponent<G_Util>();
        utm.当前等级 = 50;

    }


    private void Start()
    {

        utg.移动与坐标.Add("上", "尸王秘境");
        utg.移动与坐标.Add("左", "尸王秘境");
        utg.移动与坐标.Add("下", "尸王秘境");
        utg.移动与坐标.Add("右", "尸王秘境");

        utg.刷新移动与坐标();
        utg.生成场景怪物信息方法(this);
        // 技能与粒度.Clear();
    }

    public bool 怪物1赋值(combat cb)
    {
        utm.怪物名字 = "行尸";
        utm.怪物品质 = 0;
        utm.是否主动攻击 = false;
        utm.最低等级 = 50;
        utm.最高等级 = 50;
        utm.基础攻击力 = 75;
        utm.基础防御力 = 0;
        utm.基础血量 = 360;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 0;
        utm.攻击力资质 = 0.6f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.9f;
        utm.成长 = 5f;
        utm.攻击速度 = 1.4f;
        utm.基础经验值 = 2600;
        utm.经验值系数 = 3;
        utm.铜币 = (int)(5 * utm.成长);
        if (!cb.技能与粒度.ContainsKey("獠牙"))
            cb.技能与粒度.Add("獠牙", 0.4f);
        utm.添加基础掉落(cb,1);
        return true;
    }

    public bool 怪物2赋值(combat cb)
    {
        utm.怪物名字 = "尸妖";
        utm.怪物品质 = 1;
        utm.是否主动攻击 = false;
        utm.最低等级 = 50;
        utm.最高等级 = 50;
        utm.基础攻击力 = 85;
        utm.基础防御力 = 0;
        utm.基础血量 = 360;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 0;
        utm.攻击力资质 = 0.6f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.9f;
        utm.成长 = 5f;
        utm.攻击速度 = 1.4f;
        utm.基础经验值 = 2600;
        utm.经验值系数 = 3;
        utm.铜币 = (int)(5 * utm.成长);
        if (!cb.技能与粒度.ContainsKey("獠牙"))
            cb.技能与粒度.Add("獠牙", 0.4f);
        utm.添加基础掉落(cb,1);
        return true;
    }

    public bool 怪物3赋值(combat cb)
    {
        utm.怪物名字 = "骨观";
        utm.怪物品质 = 2;
        utm.是否主动攻击 = false;
        utm.最低等级 = 50;
        utm.最高等级 = 50;
        utm.基础攻击力 = 65;
        utm.基础防御力 = 35;
        utm.基础血量 = 1000;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 80;
        utm.攻击力资质 = 0.85f;
        utm.防御力资质 = 0.55f;
        utm.血量资质 = 1.1f;
        utm.成长 = 5f;
        utm.攻击速度 = 1.5f;
        utm.基础经验值 = 2800;
        utm.经验值系数 = 10;
        utm.铜币 = (int)(20 * utm.成长);
        if (!cb.技能与粒度.ContainsKey("吞噬"))
            cb.技能与粒度.Add("吞噬", 0.4f);
        if (!cb.技能与粒度.ContainsKey("嗜血"))
            cb.技能与粒度.Add("嗜血", 0.5f);

        utm.添加基础掉落(cb,1);
        return true;
    }

    public bool 怪物4赋值(combat cb)
    {
        utm.怪物名字 = "✦银甲尸✧";
        utm.怪物品质 = 3;
        utm.是否主动攻击 = false;
        utm.最低等级 = 50;
        utm.最高等级 = 50;
        utm.基础攻击力 = 120;
        utm.基础防御力 = 260;
        utm.基础血量 = 3500;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 100;
        utm.攻击力资质 = 0.9f;
        utm.防御力资质 = 0.5f;
        utm.血量资质 = 1.2f;
        utm.成长 = 5f;
        utm.攻击速度 = 1.2f;
        utm.基础经验值 = 28000;
        utm.经验值系数 = 30;
        utm.铜币 = (int)(50 * utm.成长);

        if (!cb.技能与粒度.ContainsKey("嗜血"))
            cb.技能与粒度.Add("嗜血", 0.4f);
        if (!cb.技能与粒度.ContainsKey("獠牙"))
            cb.技能与粒度.Add("獠牙", 0.4f);
        if (!cb.技能与粒度.ContainsKey("吞噬"))
            cb.技能与粒度.Add("吞噬", 0.3f);

        utm.添加基础掉落(cb,1);
        return true;
    }

    public bool 怪物5赋值(combat cb)
    {
        utm.怪物名字 = "尸王";
        utm.怪物品质 = 4;
        utm.是否主动攻击 = false;
        utm.最低等级 = 50;
        utm.最高等级 = 50;
        utm.基础攻击力 = 300;
        utm.基础防御力 = 200;
        utm.基础血量 = 8500;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 300;
        utm.攻击力资质 = 1.1f;
        utm.防御力资质 = 0.8f;
        utm.血量资质 = 1.5f;
        utm.成长 = 5f;
        utm.攻击速度 = 1.5f;
        utm.基础经验值 = 88000;
        utm.经验值系数 = 50;
        utm.铜币 = (int)(50 * utm.成长);

        if (!cb.技能与粒度.ContainsKey("吞噬"))
            cb.技能与粒度.Add("吞噬", 0.2f);
        if (!cb.技能与粒度.ContainsKey("≮不老长春诀≯"))
            cb.技能与粒度.Add("≮不老长春诀≯", 0.1f);
        if (!cb.技能与粒度.ContainsKey("嗜血"))
            cb.技能与粒度.Add("嗜血", 0.2f);
        if (!cb.技能与粒度.ContainsKey("磐石"))
            cb.技能与粒度.Add("磐石", 0.2f);

        utm.添加基础掉落(cb,1);
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
