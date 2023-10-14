using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class m_shanqiutaolin : MonoBehaviour, I_monster
{
    //自定义
    private UT_monster utm;
    private G_Util utg;
    public bool isFlash = false;
    private float timer;//定时器
    private GameObject 特殊;
    private int index=0;

    private void Awake()
    {
        utm = gameObject.GetComponent<UT_monster>();
        utg = NameMgr.画布.GetComponent<G_Util>();
        timer = UnityEngine.Random.Range(1.0f, 3.0f); // 随机秒数
        utm.当前等级 = 20;

    }


    private void Start()
    {
        utg.移动与坐标.Add("上", "十字坡");
        utg.移动与坐标.Add("下", "村口东");
        utg.刷新移动与坐标();
        utg.生成场景怪物信息方法(this);

        // 技能与粒度.Clear();

    }

    void FixedUpdate()
    {
        if (Time.frameCount % 10 == 0)
        { //每10帧进行一次检测
            if (!isFlash)
            {
                if (GameObject.FindGameObjectsWithTag("建筑").Length == 0)
                {
                    GameObject m8 = GameObject.Find("combat_other/monster/m8").gameObject;
                    特殊 = m8.transform.Find("血条8").gameObject;
                    特殊.SetActive(true);
                    isFlash = true;
                    StartCoroutine(延迟性赋值(特殊));
                }
            }
        }


        if (GameObject.Find("血条8"))
        {
            //生成/显示...气泡计时器
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                //判断生成还是销毁气泡
                if (特殊.transform.Find("气泡_位置").childCount != 0 && 特殊.transform.Find("气泡_位置/气泡(Clone)").gameObject.activeSelf)
                {
                    特殊.transform.Find("气泡_位置/气泡(Clone)").gameObject.SetActive(false);
                    timer = UnityEngine.Random.Range(1.0f, 3.0f); // 随机秒数
                }
                else
                {
                    桃仙分身气泡内容(特殊);
                    timer = 2.0f; // 显示2秒
                }

            }
        }



    }


    private void 桃仙分身气泡内容(GameObject 特殊)
    {
        string str;
        string str1 = "这都被你找到了";
        string str2 = "有点东西呀";
        if (index % 2==0)
            str = str1;
        else
            str = str2;
        utg.生成气泡(str, 特殊);
        index++;
    }

    private IEnumerator 延迟性赋值(GameObject 特殊)
    {
        yield return new WaitForSeconds(0.1f);
        combat cb = 特殊.GetComponent<combat>();
        怪物4赋值(cb);
        GameObject cbo = GameObject.Find("combat_other");
        utm.加载赋值后的属性(特殊.GetComponent<combat>(), false);
        cb.怪物初始化行为();
    }

    public int 怪物1赋值(combat cb)
    {
        utm.怪物名字 = "桃花妖";
        utm.怪物品质 = 0;
        utm.是否主动攻击 = false;
        utm.最低等级 = 15;
        utm.最高等级 = 20;
        utm.基础攻击力 = 20;
        utm.基础防御力 = 0;
        utm.基础血量 = 70;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 15;
        utm.攻击力资质 = 0.3f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.35f;
        utm.成长 = 1;
        utm.攻击速度 = 1.3f;
        utm.基础经验值 = 60;
        utm.经验值系数 = 3;
        utm.铜币 = (int)(5 * utm.成长);
        utm.添加基础掉落(cb,utm.当前等级,1);
        utm.添加掉落(utg.概率(1, 20), cb.掉落集合, "桃子",1);
         return utm.返回战斗力(utm.最高等级);
    }

    public int 怪物2赋值(combat cb)
    {
        utm.怪物名字 = "桃精";
        utm.怪物品质 = 0;
        utm.是否主动攻击 = false;
        utm.最低等级 = 15;
        utm.最高等级 = 20;
        utm.基础攻击力 = 18;
        utm.基础防御力 = 0;
        utm.基础血量 = 85;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 18;
        utm.攻击力资质 = 0.35f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.4f;
        utm.成长 = 1;
        utm.攻击速度 = 1.4f;
        utm.基础经验值 = 66;
        utm.经验值系数 = 3;
        utm.铜币 = (int)(5 * utm.成长);
        utm.添加基础掉落(cb,utm.当前等级,1);
        utm.添加掉落(utg.概率(1, 20), cb.掉落集合, "桃子",1);
         return utm.返回战斗力(utm.最高等级);
    }

    public int 怪物3赋值(combat cb)
    {
        utm.怪物名字 = "十年桃妖";
        utm.怪物品质 = 1;
        utm.是否主动攻击 = false;
        utm.最低等级 = 20;
        utm.最高等级 = 20;
        utm.基础攻击力 = 28;
        utm.基础防御力 = 8;
        utm.基础血量 = 180;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 30;
        utm.攻击力资质 = 0.5f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0.6f;
        utm.成长 = 1;
        utm.攻击速度 = 1.7f;
        utm.基础经验值 = 100;
        utm.经验值系数 = 4;
        utm.铜币 = (int)(10 * utm.成长);
        utm.添加基础掉落(cb,utm.当前等级, 2);
        utm.添加掉落(utg.概率(1, 3), cb.掉落集合, "桃妖之心",1);
        utm.添加掉落(utg.概率(1, 3), cb.掉落集合, "桃子",1);
         return utm.返回战斗力(utm.最高等级);
    }

    public int 怪物4赋值(combat cb)
    {
        utm.怪物名字 = "桃仙分身";
        utm.怪物品质 = 3;
        utm.是否主动攻击 = false;
        utm.最低等级 = 30;
        utm.最高等级 = 30;
        utm.基础攻击力 = 60;
        utm.基础防御力 = 35;
        utm.基础血量 = 860;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 300;
        utm.攻击力资质 = 0.9f;
        utm.防御力资质 = 0.9f;
        utm.血量资质 = 0.9f;
        utm.成长 = 1;
        utm.攻击速度 = 1.8f;
        utm.基础经验值 = 1500;
        utm.经验值系数 = 20;
        utm.铜币 = (int)(50 * utm.成长);
        utm.添加基础掉落(cb,utm.当前等级, 4);
        utm.添加掉落(true, cb.掉落集合, "宠爱零食", 1);
         return utm.返回战斗力(utm.最高等级);

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
        utm.怪物名字 = "百年桃树";
        utm.怪物品质 = 0;
        utm.是否主动攻击 = false;
        utm.最低等级 = 20;
        utm.最高等级 = 20;
        utm.基础攻击力 = 0;
        utm.基础防御力 = 0;
        utm.基础血量 = 1000;
        utm.基础暴击率 = 0;
        utm.基础回血值 = 0;
        utm.攻击力资质 = 0f;
        utm.防御力资质 = 0f;
        utm.血量资质 = 0f;
        utm.成长 = 1;
        utm.攻击速度 = 0f;
        utm.基础经验值 = 1;
        utm.经验值系数 = 1;
        utm.添加掉落(true, cb.掉落集合, "百年桃木", 1);
        utm.掉落信息.Clear();
        return true;
    }
}
