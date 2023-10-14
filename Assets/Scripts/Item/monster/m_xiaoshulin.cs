using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class m_xiaoshulin : MonoBehaviour, I_monster
{


    //自定义
    private UT_monster utm;
    private G_Util utg;

    private void Awake()
    {
        utm = gameObject.GetComponent<UT_monster>();
        utg = NameMgr.画布.GetComponent<G_Util>();
        utm.当前等级 = 10;

    }


    private void Start()
    {
        utg.移动与坐标.Add("左", "村口小道");
        utg.移动与坐标.Add("右", "村口东");
        utg.刷新移动与坐标();
        utg.生成场景怪物信息方法(this);
        // 技能与粒度.Clear();
    }

    public int 怪物1赋值(combat cb)
    {
        utm.怪物名字 = "小狐狸";
        utm.怪物品质 = 0;
        utm.是否主动攻击 = false;
        utm.最低等级 = 5;
        utm.最高等级 = 8;
        utm.基础攻击力 = 6;
        utm.基础防御力 = 0;
        utm.基础血量 = 15;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 0;
        utm.攻击力资质 = 0.15f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.2f;
        utm.成长 = 1f;
        utm.攻击速度 = 1.3f;
        utm.基础经验值 = 14;
        utm.经验值系数 = 3;
        utm.铜币 = (int)(5 * utm.成长);
        utm.添加基础掉落(cb,utm.当前等级,1);
         return utm.返回战斗力(utm.最高等级);
    }

    public int 怪物2赋值(combat cb)
    {
        utm.怪物名字 = "野猪";
        utm.怪物品质 = 0;
        utm.是否主动攻击 = false;
        utm.最低等级 = 5;
        utm.最高等级 = 10;
        utm.基础攻击力 = 5;
        utm.基础防御力 = 0;
        utm.基础血量 = 20;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 1;
        utm.攻击力资质 = 0.15f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.25f;
        utm.成长 = 1f;
        utm.攻击速度 = 1.5f;
        utm.基础经验值 = 16;
        utm.经验值系数 = 3;
        utm.铜币 = (int)(5 * utm.成长);
        utm.添加基础掉落(cb,utm.当前等级, 1);
         return utm.返回战斗力(utm.最高等级);
    }

    public int 怪物3赋值(combat cb)
    {
        utm.怪物名字 = "竹叶青";
        utm.怪物品质 = 0;
        utm.是否主动攻击 = false;
        utm.最低等级 = 5;
        utm.最高等级 = 10;
        utm.基础攻击力 = 8;
        utm.基础防御力 = 0;
        utm.基础血量 = 12;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 2;
        utm.攻击力资质 = 0.25f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.15f;
        utm.成长 = 1f;
        utm.攻击速度 = 1.4f;
        utm.基础经验值 = 20;
        utm.经验值系数 = 3;
        utm.铜币 = (int)(5 * utm.成长);
        utm.添加基础掉落(cb,utm.当前等级, 1);
         return utm.返回战斗力(utm.最高等级);
    }

    public int 怪物4赋值(combat cb)
    {
        utm.怪物名字 = "羽鸡";
        utm.怪物品质 = 1;
        utm.是否主动攻击 = false;
        utm.最低等级 = 5;
        utm.最高等级 = 10;
        utm.基础攻击力 = 10;
        utm.基础防御力 = 2;
        utm.基础血量 = 30;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 10;
        utm.攻击力资质 = 0.4f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.35f;
        utm.成长 = 1f;
        utm.攻击速度 = 1.5f;
        utm.基础经验值 = 90;
        utm.经验值系数 = 5;
        utm.铜币 = (int)(20 * utm.成长);
        utm.添加掉落(utg.概率(1, 6), cb.掉落集合, "彩色羽毛",1);
        utm.添加基础掉落(cb,utm.当前等级, 2);
         return utm.返回战斗力(utm.最高等级);
    }

    public int 怪物5赋值(combat cb)
    {
        utm.怪物名字 = "机灵的松鼠☆";
        utm.怪物品质 = 2;
        utm.是否主动攻击 = false;
        utm.最低等级 = 10;
        utm.最高等级 = 10;
        utm.基础攻击力 = 15;
        utm.基础防御力 = 5;
        utm.基础血量 = 55;
        utm.基础暴击率 = 5;
        utm.基础回血值 = 10;
        utm.攻击力资质 = 0.5f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.65f;
        utm.成长 = 1f;
        utm.攻击速度 = 1.2f;
        utm.基础经验值 = 200;
        utm.经验值系数 = 10;
        utm.铜币 = (int)(20 * utm.成长);
        utm.添加基础掉落(cb,utm.当前等级, 3);
        utm.添加掉落(utg.概率(11, 8), cb.掉落集合, "松鼠鞋",1);
        utm.添加掉落(utg.概率(11, 8), cb.掉落集合, "松鼠剑",1);
        utm.添加掉落(utg.概率(11, 8), cb.掉落集合, "松鼠帽",1);
        utm.添加掉落(utg.概率(11, 8), cb.掉落集合, "松鼠衣",1);
        utm.添加掉落(utg.概率(11, 8), cb.掉落集合, "松鼠的榛子",1);
        utm.添加掉落(true, cb.掉落集合, "大松果",1);
        utm.添加掉落(utg.概率(1, 10), cb.掉落集合, "2仙晶卡",1);
         return utm.返回战斗力(utm.最高等级);
    }

    public int 怪物6赋值(combat cb)
    {
        throw new NotImplementedException();
    }

    public int 怪物7赋值(combat cb)
    {
        throw new NotImplementedException();
    }

    public int 怪物8赋值(combat cb)
    {
        throw new NotImplementedException();
    }

    public int 怪物9赋值(combat cb)
    {
        throw new NotImplementedException();
    }

    public bool 建筑赋值(combat cb)
    {
        throw new NotImplementedException();
    }
}
