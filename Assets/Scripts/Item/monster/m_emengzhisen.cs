using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class m_emengzhisen : MonoBehaviour, I_monster
{

    //自定义
    private UT_monster utm;
    private G_Util utg;

    private void Awake()
    {
        utm = gameObject.GetComponent<UT_monster>();
        utg = NameMgr.画布.GetComponent<G_Util>();
        utm.当前等级 = 35;

    }

    private void Start()
    {
        utg.移动与坐标.Add("左", "黄沙地");
        utg.移动与坐标.Add("右", "荒原");
        utg.移动与坐标.Add("上", "临海官道");
        utg.刷新移动与坐标();
        utg.生成场景怪物信息方法(this);

        // 技能与粒度.Clear();
    }


    public int 怪物1赋值(combat cb)
    {
        utm.怪物名字 = "食人花";
        utm.怪物品质 = 0;
        utm.是否主动攻击 = false;
        utm.最低等级 = 30;
        utm.最高等级 = 35;
        utm.基础攻击力 = 58;
        utm.基础防御力 = 0;
        utm.基础血量 = 220;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 20;
        utm.攻击力资质 = 0.4f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.4f;
        utm.成长 = 1f;
        utm.攻击速度 = 1.3f;
        utm.基础经验值 = 350;
        utm.经验值系数 = 3;
        utm.铜币 = (int)(5 * utm.成长);
        utm.添加基础掉落(cb,utm.当前等级,1);
        utm.添加掉落(utg.概率(3, 4), cb.掉落集合, "食人花的花冠", 1);
         return utm.返回战斗力(utm.最高等级);
    }

    public int 怪物2赋值(combat cb)
    {
        utm.怪物名字 = "【捕蝇草】";
        utm.怪物品质 = 0;
        utm.是否主动攻击 = false;
        utm.最低等级 = 30;
        utm.最高等级 = 35;
        utm.基础攻击力 = 52;
        utm.基础防御力 = 20;
        utm.基础血量 = 280;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 20;
        utm.攻击力资质 = 0.45f;
        utm.防御力资质 = 0.2f;
        utm.血量资质 = 0.45f;
        utm.成长 = 1f;
        utm.攻击速度 = 1.5f;
        utm.基础经验值 = 400;
        utm.经验值系数 = 4;
        utm.铜币 = (int)(5 * utm.成长);
        utm.添加基础掉落(cb,utm.当前等级,1);
         return utm.返回战斗力(utm.最高等级);
    }

    public int 怪物3赋值(combat cb)
    {
        utm.怪物名字 = "麻花藤";
        utm.怪物品质 = 1;
        utm.是否主动攻击 = false;
        utm.最低等级 = 30;
        utm.最高等级 = 35;
        utm.基础攻击力 = 63;
        utm.基础防御力 = 18;
        utm.基础血量 = 400;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 30;
        utm.攻击力资质 = 0.55f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.55f;
        utm.成长 = 1f;
        utm.攻击速度 = 1.3f;
        utm.基础经验值 = 500;
        utm.经验值系数 = 5;
        utm.铜币 = (int)(10 * utm.成长);
        utm.添加基础掉落(cb,utm.当前等级, 2);
         return utm.返回战斗力(utm.最高等级);
    }

    public int 怪物4赋值(combat cb)
    {
        utm.怪物名字 = "鹿灵";
        utm.怪物品质 = 2;
        utm.是否主动攻击 = false;
        utm.最低等级 = 35;
        utm.最高等级 = 35;
        utm.基础攻击力 = 70;
        utm.基础防御力 = 35;
        utm.基础血量 = 660;
        utm.基础暴击率 = 5;
        utm.基础回血值 = 80;
        utm.攻击力资质 = 0.7f;
        utm.防御力资质 = 0.3f;
        utm.血量资质 = 0.8f;
        utm.成长 = 1f;
        utm.攻击速度 = 1.2f;
        utm.基础经验值 = 1200;
        utm.经验值系数 = 10;
        utm.铜币 = (int)(20 * utm.成长);
        utm.添加基础掉落(cb,utm.当前等级, 3);
        utm.添加掉落(true, cb.掉落集合, "祝福果子",1);
        utm.添加掉落(utg.概率(1, 10), cb.掉落集合, "2仙晶卡",1);
        utm.添加掉落(utg.概率(1, 3), cb.掉落集合, "祝福果子",1);
        utm.添加掉落(utg.概率(1, 5), cb.掉落集合, "祝福果子",1);
        utm.添加掉落(utg.概率(1, 7), cb.掉落集合, "祝福果子",1);
        utm.添加掉落(utg.概率(1, 9), cb.掉落集合, "祝福果子",1);
         return utm.返回战斗力(utm.最高等级);
    }

    public int 怪物5赋值(combat cb)
    {
        /*怪物名字 = "梦魇";
        怪物品质 = 4;
        是否主动攻击 = false;
        最低等级 = 50;
        最高等级 = 50;
        基础攻击力 = 280;
        基础防御力 = 90;
        基础血量 = 6000;
        基础暴击率 = 10;
        基础回血值 = 120;
        攻击力资质 = 1.9f;
        防御力资质 = 1.2f;
        血量资质 = 2.1f;
        成长 = 2f;
        攻击速度 = 2.0f;
        基础经验值 = 12600;
        经验值系数 = 50;
        铜币 = (int)(1000 * 成长);
        utm.添加基础掉落(10, "下品涅槃果", cb);
        utm.添加掉落(true, cb.掉落集合, "神性结晶-梦魇");
        utm.添加掉落(utg.概率(1, 10), cb.掉落集合, "10仙晶卡");
        掉落.Add("神性结晶-梦魇");
        掉落.Add("10仙晶卡");*/
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
