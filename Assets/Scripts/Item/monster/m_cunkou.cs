using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m_cunkou : MonoBehaviour, I_monster
{

    //自定义
    private UT_monster utm;
    private G_Util utg;


    private void Awake() {
        utm = gameObject.GetComponent<UT_monster>();
        utg = NameMgr.画布.GetComponent<G_Util>();
        utm.当前等级 = 5;

    }

    private void Start()
    {
        utg.移动与坐标.Add("左", "桃源村");
        utg.移动与坐标.Add("右", "小树林");
        utg.刷新移动与坐标();
        utg.生成场景怪物信息方法(this);


        // utm.技能与粒度.Clear();
    }

    public bool 怪物1赋值(combat cb)
    {
        utm.怪物名字 = "小鸡";
        utm.怪物品质 = 0;
        utm.是否主动攻击 = false;
        utm.最低等级 = 1;
        utm.最高等级 = 5;
        utm.基础攻击力 = 6;
        utm.基础防御力 = 0;
        utm.基础血量 = 18;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 0;
        utm.攻击力资质 = 0.12f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.1f;
        utm.成长 = 1f;
        utm.攻击速度 = 1.5f;
        utm.基础经验值 = 3;
        utm.经验值系数 = 1;
        utm.铜币 = (int)(5 * utm.成长);
        utm.添加基础掉落(cb,1);

        return true;
    }

    public bool 怪物2赋值(combat cb)
    {
        utm.怪物名字 = "兔子";
        utm.怪物品质 = 0;
        utm.是否主动攻击 = false;
        utm.最低等级 = 1;
        utm.最高等级 = 5;
        utm.基础攻击力 = 7;
        utm.基础防御力 = 0;
        utm.基础血量 = 20;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 1;
        utm.攻击力资质 = 0.14f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.12f;
        utm.成长 = 1f;
        utm.攻击速度 = 1.3f;
        utm.基础经验值 = 5;
        utm.经验值系数 = 1;
        utm.铜币 = (int)(5 * utm.成长);
        utm.添加基础掉落(cb,1);
        return true;
    }

    public bool 怪物3赋值(combat cb)
    {
        utm.怪物名字 = "黄鼠狼";
        utm.怪物品质 = 0;
        utm.是否主动攻击 = false;
        utm.最低等级 = 1;
        utm.最高等级 = 5;
        utm.基础攻击力 = 8;
        utm.基础防御力 = 0;
        utm.基础血量 = 24;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 2;
        utm.攻击力资质 = 0.15f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.15f;
        utm.成长 = 1f;
        utm.攻击速度 = 1.4f;
        utm.基础经验值 = 6;
        utm.经验值系数 = 1;
        utm.铜币 = (int)(5 * utm.成长);
        utm.添加基础掉落(cb,1);
        return true;
    }

    public bool 怪物4赋值(combat cb)
    {
        utm.怪物名字 = "疯狗";
        utm.怪物品质 = 1;
        utm.是否主动攻击 = false;
        utm.最低等级 = 3;
        utm.最高等级 = 5;
        utm.基础攻击力 = 10;
        utm.基础防御力 = 1;
        utm.基础血量 = 40;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 5;
        utm.攻击力资质 = 0.32f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.4f;
        utm.成长 = 1f;
        utm.攻击速度 = 1.5f;
        utm.基础经验值 = 30;
        utm.经验值系数 = 4;
        utm.铜币 = (int)(10 * utm.成长);
        utm.添加基础掉落(cb, 2);
        utm.添加掉落(utg.概率(5, 10), cb.掉落集合,  "疯狗牙",1);
        utm.添加掉落(utg.概率(5, 10), cb.掉落集合,  "疯狗皮毛",1);
        utm.掉落信息.Add("疯狗牙");
        utm.掉落信息.Add("疯狗皮毛");
        return true;

    }

    public bool 怪物5赋值(combat cb)
    {
        return false;
    }

    public bool 怪物6赋值(combat cb)
    {
        return false;
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
        utm.怪物名字 = "灌木丛";
        utm.怪物品质 = 0;
        utm.是否主动攻击 = false;
        utm.最低等级 = 1;
        utm.最高等级 = 1;
        utm.基础攻击力 = 0;
        utm.基础防御力 = 0;
        utm.基础血量 = 20;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 0;
        utm.攻击力资质 = 0f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0f;
        utm.成长 = 0;
        utm.攻击速度 = 0f;
        utm.基础经验值 = 1;
        utm.经验值系数 = 1;
        utm.添加掉落(true, cb.掉落集合, "木材", 1);
        utm.掉落信息.Clear();
        utm.掉落信息.Add("木材");
        return true;
    }
}
