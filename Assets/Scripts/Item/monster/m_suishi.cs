using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class m_suishi : MonoBehaviour, I_monster
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


    private void Start()
    {
        utg.移动与坐标.Add("下", "无尽海岸");
        utg.移动与坐标.Add("左", "噩梦之森");
        utg.刷新移动与坐标();
        utg.生成场景怪物信息方法(this);

        // 技能与粒度.Clear();
    }


    public bool 怪物1赋值(combat cb)
    {
        utm.怪物名字 = "蚺";
        utm.怪物品质 = 4;
        utm.是否主动攻击 = false;
        utm.最低等级 = 30;
        utm.最高等级 = 30;
        utm.基础攻击力 = 50;
        utm.基础防御力 = 50;
        utm.基础血量 = 5000;
        utm.基础暴击率 = 8;
        utm.基础回血值 = 100;
        utm.攻击力资质 = 1.02f;
        utm.防御力资质 = 0.86f;
        utm.血量资质 = 1.6f;
        utm.成长 = 1.0f;
        utm.攻击速度 = 1.1f;
        utm.基础经验值 = 6000;
        utm.经验值系数 = 50;
        utm.铜币 = (int)(100 * utm.成长);
        if (!cb.技能与粒度.ContainsKey("翻江倒海"))
            cb.技能与粒度.Add("翻江倒海", 0.4f);
        if (!cb.技能与粒度.ContainsKey("水甲术"))
            cb.技能与粒度.Add("水甲术", 0.15f);
        utm.添加基础掉落(cb,5);
        utm.添加掉落(utg.概率(1, 5), cb.掉落集合, "蛟鳞剑",1);
        utm.添加掉落(utg.概率(1, 5), cb.掉落集合, "蛟鳞甲",1);
        utm.添加掉落(utg.概率(1, 5), cb.掉落集合, "蛟蛇角",1);
        utm.添加掉落(utg.概率(1, 5), cb.掉落集合, "蛟蛇尾",1);
        utm.添加掉落(utg.概率(1, 6), cb.掉落集合, "化蛟珠",1);
        utm.添加掉落(true, cb.掉落集合, "听海石",1);
        utm.掉落信息.Add("化蛟珠");
        utm.掉落信息.Add("蛟鳞剑");
        utm.掉落信息.Add("蛟鳞甲");
        utm.掉落信息.Add("蛟蛇角");
        utm.掉落信息.Add("蛟蛇尾");
        utm.掉落信息.Add("听海石");

        return true;
    }

    public bool 怪物2赋值(combat cb)
    {
        return false;
    }

    public bool 怪物3赋值(combat cb)
    {
        return false;
    }

    public bool 怪物4赋值(combat cb)
    {
        return false;
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
