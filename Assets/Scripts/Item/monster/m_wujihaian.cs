using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m_wujihaian : MonoBehaviour, I_monster
{

    //自定义
    private UT_monster utm;
    private G_Util utg;

    private void Awake() {
        utm = gameObject.GetComponent<UT_monster>();
        utg = NameMgr.画布.GetComponent<G_Util>();
        utm.当前等级 = 25;

    }

    private void Start()
    {
        utg.移动与坐标.Add("左", "小树林");
        utg.移动与坐标.Add("上", "碎石滩");
        utg.刷新移动与坐标();
        utg.生成场景怪物信息方法(this);

        // utm.技能与粒度.Clear();
    }

    public bool 怪物1赋值(combat cb)
    {
        utm.怪物名字 = "虾兵";
        utm.怪物品质 = 0;
        utm.是否主动攻击 = false;
        utm.最低等级 = 20;
        utm.最高等级 = 25;
        utm.基础攻击力 = 40;
        utm.基础防御力 = 0;
        utm.基础血量 = 220;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 8;
        utm.攻击力资质 = 0.4f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.45f;
        utm.成长 = 1f;
        utm.攻击速度 = 1.5f;
        utm.基础经验值 = 120;
        utm.经验值系数 = 3;
        utm.铜币 = (int)(5 * utm.成长);
        utm.添加基础掉落(cb,1);
        return true;
    }

    public bool 怪物2赋值(combat cb)
    {
        utm.怪物名字 = "蟹将";
        utm.怪物品质 = 0;
        utm.是否主动攻击 = false;
        utm.最低等级 = 20;
        utm.最高等级 = 25;
        utm.基础攻击力 = 38;
        utm.基础防御力 = 0;
        utm.基础血量 = 260;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 12;
        utm.攻击力资质 = 0.3f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.5f;
        utm.成长 = 1f;
        utm.攻击速度 = 1.6f;
        utm.基础经验值 = 126;
        utm.经验值系数 = 3;
        utm.铜币 = (int)(5 * utm.成长);
        utm.添加基础掉落(cb,1);
        return true;
    }

    public bool 怪物3赋值(combat cb)
    {
        utm.怪物名字 = "鱼人";
        utm.怪物品质 = 2;
        utm.是否主动攻击 = false;
        utm.最低等级 = 20;
        utm.最高等级 = 25;
        utm.基础攻击力 = 65;
        utm.基础防御力 = 20;
        utm.基础血量 = 420;
        utm.基础暴击率 = 5;
        utm.基础回血值 = 18;
        utm.攻击力资质 = 0.7f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.7f;
        utm.成长 = 1f;
        utm.攻击速度 = 1.6f;
        utm.基础经验值 = 280;
        utm.经验值系数 = 5;
        utm.铜币 = (int)(20 * utm.成长);
        utm.添加掉落(utg.概率(1, 10), cb.掉落集合, "鱼人心",1);
        utm.添加基础掉落(cb, 3);
        utm.掉落信息.Add("鱼人心");
        return true;
    }

    public bool 怪物4赋值(combat cb)
    {
        utm.怪物名字 = "鲛人";
        utm.怪物品质 = 2;
        utm.是否主动攻击 = false;
        utm.最低等级 = 20;
        utm.最高等级 = 25;
        utm.基础攻击力 = 60;
        utm.基础防御力 = 22;
        utm.基础血量 = 430;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 10;
        utm.攻击力资质 = 0.7f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.8f;
        utm.成长 = 1f;
        utm.攻击速度 = 1.4f;
        utm.基础经验值 = 280;
        utm.经验值系数 = 5;
        utm.铜币 = (int)(20 * utm.成长);
        utm.添加掉落(utg.概率(1, 10), cb.掉落集合, "鲛人泪",1);
        utm.添加基础掉落(cb, 3);
        utm.掉落信息.Add("鲛人泪");
        return true;
    }

    public bool 怪物5赋值(combat cb)
    {
        utm.怪物名字 = "碧水蟒";
        utm.怪物品质 = 3;
        utm.是否主动攻击 = true;
        utm.最低等级 = 25;
        utm.最高等级 = 25;
        utm.基础攻击力 = 90;
        utm.基础防御力 = 25;
        utm.基础血量 = 900;
        utm.基础暴击率 = 10;
        utm.基础回血值 = 50;
        utm.攻击力资质 = 0.9f;
        utm.防御力资质 = 0.6f;
        utm.血量资质 = 1.1f;
        utm.成长 = 1f;
        utm.攻击速度 = 1.8f;
        utm.基础经验值 = 1200;
        utm.经验值系数 = 20;
        utm.铜币 = (int)(50 * utm.成长);
        utm.添加掉落(utg.概率(1, 8), cb.掉落集合, "青磷石",1);
        utm.添加基础掉落(cb, 4);
        utm.掉落信息.Add("青磷石");
        return true;
    }

    public bool 怪物6赋值(combat cb)
    {
        throw new System.NotImplementedException();
    }

    public bool 怪物7赋值(combat cb)
    {
        throw new System.NotImplementedException();
    }

    public bool 怪物8赋值(combat cb)
    {
        throw new System.NotImplementedException();
    }

    public bool 怪物9赋值(combat cb)
    {
        throw new System.NotImplementedException();
    }

    public bool 建筑赋值(combat cb)
    {
        throw new System.NotImplementedException();
    }
}
