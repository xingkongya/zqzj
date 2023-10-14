using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m_yunmengkou : MonoBehaviour, I_monster
{

    //自定义
    private UT_monster utm;
    private G_Util utg;

    private void Awake() {
        utm = gameObject.GetComponent<UT_monster>();
        utg = NameMgr.画布.GetComponent<G_Util>();
        utm.当前等级 = 15;

    }

    private void Start()
    {
        utg.移动与坐标.Add("左", "小树林");
        //utg.移动与坐标.Add("左", "十里坡");
        utg.移动与坐标.Add("上", "山丘桃林");
        utg.刷新移动与坐标();
        utg.生成场景怪物信息方法(this);

        // utm.技能与粒度.Clear();
    }

    public int 怪物1赋值(combat cb)
    {
        utm.怪物名字 = "烈雀";
        utm.怪物品质 = 0;
        utm.是否主动攻击 = false;
        utm.最低等级 = 10;
        utm.最高等级 = 15;
        utm.基础攻击力 = 12;
        utm.基础防御力 = 0;
        utm.基础血量 = 30;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 8;
        utm.攻击力资质 = 0.2f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.2f;
        utm.成长 = 1;
        utm.攻击速度 = 1.4f;
        utm.基础经验值 = 28;
        utm.经验值系数 = 3;
        utm.铜币 = (int)(5 * utm.成长);
        utm.添加基础掉落(cb,utm.当前等级,1);
         return utm.返回战斗力(utm.最高等级);
    }

    public int 怪物2赋值(combat cb)
    {
        utm.怪物名字 = "花貂";
        utm.怪物品质 = 0;
        utm.是否主动攻击 = false;
        utm.最低等级 = 10;
        utm.最高等级 = 15;
        utm.基础攻击力 = 10;
        utm.基础防御力 = 0;
        utm.基础血量 = 40;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 12;
        utm.攻击力资质 = 0.25f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.2f;
        utm.成长 = 1;
        utm.攻击速度 = 1.5f;
        utm.基础经验值 = 30;
        utm.经验值系数 = 3;
        utm.铜币 = (int)(5 * utm.成长);
        utm.添加基础掉落(cb,utm.当前等级,1);
         return utm.返回战斗力(utm.最高等级);
    }

    public int 怪物3赋值(combat cb)
    {
        utm.怪物名字 = "大刺猬";
        utm.怪物品质 = 0;
        utm.是否主动攻击 = false;
        utm.最低等级 = 10;
        utm.最高等级 = 15;
        utm.基础攻击力 = 11;
        utm.基础防御力 = 0;
        utm.基础血量 = 35;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 3;
        utm.攻击力资质 = 0.2f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.25f;
        utm.成长 = 1;
        utm.攻击速度 = 1.5f;
        utm.基础经验值 = 32;
        utm.经验值系数 = 3;
        utm.铜币 = (int)(5 * utm.成长);
        utm.添加基础掉落(cb,utm.当前等级,1);
         return utm.返回战斗力(utm.最高等级);
    }

    public int 怪物4赋值(combat cb)
    {
        utm.怪物名字 = "人参娃娃";
        utm.怪物品质 = 3;
        utm.是否主动攻击 = false;
        utm.最低等级 = 15;
        utm.最高等级 = 15;
        utm.基础攻击力 = 30;
        utm.基础防御力 = 20;
        utm.基础血量 = 260;
        utm.基础暴击率 = 10;
        utm.基础回血值 = 15;
        utm.攻击力资质 = 0.5f;
        utm.防御力资质 = 0.6f;
        utm.血量资质 = 1.1f;
        utm.成长 = 1.0f;
        utm.攻击速度 = 1.6f;
        utm.基础经验值 = 600;
        utm.经验值系数 = 20;
        utm.铜币 = (int)(50 * utm.成长);
        if (!cb.技能与粒度.ContainsKey("包扎"))
            cb.技能与粒度.Add("包扎", 1.0f);
        utm.添加基础掉落(cb,utm.当前等级, 4);
        utm.添加掉落(true, cb.掉落集合,  "人参精华",1);
        utm.添加掉落(true, cb.掉落集合, "人参的守护",1);
         return utm.返回战斗力(utm.最高等级);
    }

    public int 怪物5赋值(combat cb)
    {
        return 0;
    }

    public int 怪物6赋值(combat cb)
    {
        throw new System.NotImplementedException();
    }

    public int 怪物7赋值(combat cb)
    {
        throw new System.NotImplementedException();
    }

    public int 怪物8赋值(combat cb)
    {
        throw new System.NotImplementedException();
    }

    public int 怪物9赋值(combat cb)
    {
        throw new System.NotImplementedException();
    }

    public bool 建筑赋值(combat cb)
    {
        throw new System.NotImplementedException();
    }
}
