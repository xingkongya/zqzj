using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class m_luanzanggang : MonoBehaviour, I_monster
{

    //自定义
    private UT_monster utm;
    private G_Util utg;
    private GameObject 祭坛;
    private PropMgr pm;


    private void Awake()
    {
        utm = gameObject.GetComponent<UT_monster>();
        utg = NameMgr.画布.GetComponent<G_Util>();
        pm = PropMgr.GetInstance();
        祭坛 = GameObject.Find("祭坛");
        utm.当前等级 = 55;

    }



    private void Start()
    {
        utg.移动与坐标.Add("上", "临海西郊");
        utg.刷新移动与坐标();
        utg.生成场景怪物信息方法(this);

        // 技能与粒度.Clear();
    }

    public void 祭坛对话索引()
    {
        祭坛选择项();
    }

    private void 祭坛选择项()
    {
        Dictionary<string, UnityAction> 选项信息 = new Dictionary<string, UnityAction>();
        选项信息.Add("进入尸鬼秘境", 进入秘境);
        int num = pm.返回背包该物品的数量("古铜钥匙");
        string 结果 = "<color=red>不满足</color>";
        if (num >= 1) { 
        结果= "<color=green>满足</color>";
        }
        utg.生成对话框("秘境:尸鬼秘境\n秘境等级:50级.\n怪物成长:5.\n进入条件:古铜钥匙("+num+"/1)  "+结果+"\n提示:古铜钥匙可由该地图铜甲尸获得.", 0, 0.0f, "祭坛");
        utg.生成选项框(选项信息, 祭坛);
    }

    private void 进入秘境() {
        string 返回 = pm.失去物品("古铜钥匙", 1);
        if (返回.Equals("成功"))
        {
            utg.关闭杂项();
            utg.跳转场景("尸王秘境");
            utg.关闭对话框();
        }
        else {
            utg.生成警告框(返回);
        }
    }

    public int 怪物1赋值(combat cb)
    {
        utm.怪物名字 = "幽魂";
        utm.怪物品质 = 0;
        utm.是否主动攻击 = false;
        utm.最低等级 = 50;
        utm.最高等级 = 55;
        utm.基础攻击力 = 145;
        utm.基础防御力 = 0;
        utm.基础血量 = 800;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 45;
        utm.攻击力资质 = 0.6f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.8f;
        utm.成长 = 2f;
        utm.攻击速度 = 1.1f;
        utm.基础经验值 = 2600;
        utm.经验值系数 = 3;
        utm.铜币 = (int)(5 * utm.成长);
        utm.添加基础掉落(cb,utm.当前等级,1);
         return utm.返回战斗力(utm.最高等级);
    }

    public int 怪物2赋值(combat cb)
    {
        utm.怪物名字 = "行尸";
        utm.怪物品质 = 0;
        utm.是否主动攻击 = false;
        utm.最低等级 = 50;
        utm.最高等级 = 55;
        utm.基础攻击力 = 135;
        utm.基础防御力 = 0;
        utm.基础血量 = 950;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 50;
        utm.攻击力资质 = 0.55f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.85f;
        utm.成长 = 2f;
        utm.攻击速度 = 1.35f;
        utm.基础经验值 = 2600;
        utm.经验值系数 = 3;
        utm.铜币 = (int)(5 * utm.成长);
        utm.添加基础掉落(cb,utm.当前等级,1);
         return utm.返回战斗力(utm.最高等级);
    }

    public int 怪物3赋值(combat cb)
    {
        utm.怪物名字 = "魑魅";
        utm.怪物品质 = 1;
        utm.是否主动攻击 = false;
        utm.最低等级 = 50;
        utm.最高等级 = 55;
        utm.基础攻击力 = 160;
        utm.基础防御力 = 0;
        utm.基础血量 = 1460;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 0;
        utm.攻击力资质 = 0.55f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 1.1f;
        utm.成长 = 2f;
        utm.攻击速度 = 0.9f;
        utm.基础经验值 = 3800;
        utm.经验值系数 = 3;
        utm.铜币 = (int)(10 * utm.成长);
        utm.添加基础掉落(cb,utm.当前等级, 2);
         return utm.返回战斗力(utm.最高等级);
    }

    public int 怪物4赋值(combat cb)
    {
        utm.怪物名字 = "魍魉";
        utm.怪物品质 = 1;
        utm.是否主动攻击 = false;
        utm.最低等级 = 50;
        utm.最高等级 = 55;
        utm.基础攻击力 = 145;
        utm.基础防御力 = 10;
        utm.基础血量 = 1420;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 0;
        utm.攻击力资质 = 0.8f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.9f;
        utm.成长 = 2f;
        utm.攻击速度 = 0.9f;
        utm.基础经验值 = 3800;
        utm.经验值系数 = 3;
        utm.铜币 = (int)(10 * utm.成长);
        utm.添加基础掉落(cb,utm.当前等级, 2);
         return utm.返回战斗力(utm.最高等级);
    }

    public int 怪物5赋值(combat cb)
    {
        utm.怪物名字 = "铜甲尸";
        utm.怪物品质 = 3;
        utm.是否主动攻击 = false;
        utm.最低等级 = 55;
        utm.最高等级 = 55;
        utm.基础攻击力 = 165;
        utm.基础防御力 = 80;
        utm.基础血量 = 4000;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 0;
        utm.攻击力资质 = 0.85f;
        utm.防御力资质 = 0.3f;
        utm.血量资质 = 1.0f;
        utm.成长 = 2f;
        utm.攻击速度 = 1.4f;
        utm.基础经验值 = 12800;
        utm.经验值系数 = 20;
        utm.铜币 = (int)(50 * utm.成长);
        cb.技能与粒度.Clear();//赋值前先初始化
        if (!cb.技能与粒度.ContainsKey("嗜血"))
            cb.技能与粒度.Add("嗜血", 0.4f);
        if (!cb.技能与粒度.ContainsKey("獠牙"))
            cb.技能与粒度.Add("獠牙", 0.5f);
        utm.添加基础掉落(cb,utm.当前等级, 4);
        utm.添加掉落(true, cb.掉落集合, "古铜钥匙", 1);
        utm.添加掉落(utg.概率(1, 10), cb.掉落集合, "5仙晶卡", 1);
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
