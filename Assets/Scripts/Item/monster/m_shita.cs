using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class m_shita : MonoBehaviour, I_monster
{

    //自定义
    private UT_monster utm;
    private G_Util utg;


    private void Awake()
    {
        utm = gameObject.GetComponent<UT_monster>();
        utg = NameMgr.画布.GetComponent<G_Util>();
        utm.当前等级 = 40;

    }


    private void OnEnable()
    {

    }

    private void Start()
    {
        utg.移动与坐标.Add("右", "噩梦之森");
        utg.刷新移动与坐标();
        utg.生成场景怪物信息方法(this);

        // 技能与粒度.Clear();
    }

    public int 怪物1赋值(combat cb)
    {
        utm.怪物名字 = "黄沙妖精";
        utm.怪物品质 = 0;
        utm.是否主动攻击 = false;
        utm.最低等级 = 35;
        utm.最高等级 = 40;
        utm.基础攻击力 = 75;
        utm.基础防御力 = 0;
        utm.基础血量 = 340;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 12;
        utm.攻击力资质 = 0.4f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.55f;
        utm.成长 = 1f;
        utm.攻击速度 = 1.0f;
        utm.基础经验值 = 600;
        utm.经验值系数 = 3;
        utm.铜币 = (int)(5 * utm.成长);
        utm.添加基础掉落(cb,utm.当前等级,1);
         return utm.返回战斗力(utm.最高等级);
    }

    public int 怪物2赋值(combat cb)
    {
        utm.怪物名字 = "石头人";
        utm.怪物品质 = 0;
        utm.是否主动攻击 = false;
        utm.最低等级 = 35;
        utm.最高等级 = 40;
        utm.基础攻击力 = 65;
        utm.基础防御力 = 15;
        utm.基础血量 = 420;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 20;
        utm.攻击力资质 = 0.4f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.6f;
        utm.成长 = 1f;
        utm.攻击速度 = 1.4f;
        utm.基础经验值 = 620;
        utm.经验值系数 = 3;
        utm.铜币 = (int)(5 * utm.成长);
        utm.添加基础掉落(cb,utm.当前等级,1);
         return utm.返回战斗力(utm.最高等级);
    }

    public int 怪物3赋值(combat cb)
    {
        return 0;
    }

    public int 怪物4赋值(combat cb)
    {
        return 0;
    }

    public int 怪物5赋值(combat cb)
    {
        return 0;
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
