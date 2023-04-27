using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class m_tongtianta : MonoBehaviour, I_monster
{

    //自定义
    private UT_monster utm;
    private G_Util gut;
    private combat cb;
    public string 层数="1";
    private Dictionary<int, Action> 普通怪物 = new Dictionary<int, Action>();
    private Dictionary<int, Action> 精英怪物 = new Dictionary<int, Action>();
    private Dictionary<int, Action> boss怪物 = new Dictionary<int, Action>();

    private void Awake() {
        utm = gameObject.GetComponent<UT_monster>();
        gut = NameMgr.画布.GetComponent<G_Util>();
        普通怪物.Add(1, 普通怪物赋值1);
        普通怪物.Add(2, 普通怪物赋值2);
        普通怪物.Add(3, 普通怪物赋值3);
        普通怪物.Add(4, 普通怪物赋值4);
        普通怪物.Add(5, 普通怪物赋值5);



        role_Data myData = NameMgr.IO.load();
        if (myData.树形记录["每日记录"].ContainsKey("通天塔层数"))
            层数 = myData.树形记录["每日记录"]["通天塔层数"];
        else
            层数 = "1";
    }

    private void Start()
    {
        gut.刷新移动与坐标();
        gut.生成场景怪物信息方法(this);
        刷新楼层();
    }


    public void 退出通天塔() {
        role_Data myData = NameMgr.IO.load();
        gut.跳转场景(myData.复活城市);
    
    }
    public bool 怪物1赋值(combat cb)
    {

        //掉落赋值
        if (int.Parse(层数) > 90)
            utm.添加通天塔一百层掉落(cb);
        else if (int.Parse(层数) > 60)
            utm.添加通天塔九十层掉落(cb);
        else if (int.Parse(层数) > 30)
            utm.添加通天塔六十层掉落(cb);
        else
            utm.添加通天塔三十层掉落(cb);

        if (int.Parse(层数) % 50 == 0)
        {

        }
        else if (int.Parse(层数) % 10 == 0)
        {

        }
        else {
            System.Random 随机类 = NameMgr.随机类;
            int 随机数 = 随机类.Next(1, 普通怪物.Count+1);
            this.cb = cb;
            普通怪物[随机数]();
        }


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


    public void 普通怪物赋值1() {
        utm.怪物名字 = "≮肥遗≯";
        utm.怪物品质 = 2;
        utm.是否主动攻击 = false;
        utm.最低等级 = 5+int.Parse( 层数)*5;
        utm.最高等级 = 5 + int.Parse(层数) * 5;
        utm.基础攻击力 = 5* 4*int.Parse(层数);
        utm.基础防御力 = 5 *2* int.Parse(层数);
        utm.基础血量 = 5 * 40 * int.Parse(层数);
        utm.基础暴击率 = 10;
        utm.基础回血值 = 5 * 2 * int.Parse(层数);
        utm.攻击力资质 = 0.82f;
        utm.防御力资质 = 0.56f;
        utm.血量资质 = 1.1f;
        utm.成长 = int.Parse(层数);
        utm.攻击速度 = 1.4f;
        utm.基础经验值 = 2000 * int.Parse(层数);
        utm.经验值系数 = (int)( utm.成长* utm.成长 );
        utm.金币 = 1;
        cb.技能与粒度.Clear();//赋值前先初始化
        if (!cb.技能与粒度.ContainsKey("嗜血"))
            cb.技能与粒度.Add("嗜血", 0.3f);
        if (!cb.技能与粒度.ContainsKey("獠牙"))
            cb.技能与粒度.Add("獠牙", 0.2f);

        
    }

    public void 普通怪物赋值2()
    {
        utm.怪物名字 = "≮寓鸟≯";
        utm.怪物品质 = 2;
        utm.是否主动攻击 = false;
        utm.最低等级 = 5 + int.Parse(层数) * 5;
        utm.最高等级 = 5 + int.Parse(层数) * 5;
        utm.基础攻击力 = 5 * 5 * int.Parse(层数);
        utm.基础防御力 = 5 * 1 * int.Parse(层数);
        utm.基础血量 = 5 * 35 * int.Parse(层数);
        utm.基础暴击率 = 15;
        utm.基础回血值 = 5 * 2 * int.Parse(层数);
        utm.攻击力资质 = 0.9f;
        utm.防御力资质 = 0.4f;
        utm.血量资质 = 1.0f;
        utm.成长 = int.Parse(层数);
        utm.攻击速度 = 1.0f;
        utm.基础经验值 = 2000 * int.Parse(层数);
        utm.经验值系数 = (int)(utm.成长* utm.成长);;
        utm.金币 = 1;
        cb.技能与粒度.Clear();//赋值前先初始化
        if (!cb.技能与粒度.ContainsKey("急速飞行"))
            cb.技能与粒度.Add("急速飞行", 0.3f);
        if (!cb.技能与粒度.ContainsKey("撞击"))
            cb.技能与粒度.Add("撞击", 0.2f);
    }


    public void 普通怪物赋值3()
    {
        utm.怪物名字 = "≮闻麟≯";
        utm.怪物品质 = 2;
        utm.是否主动攻击 = false;
        utm.最低等级 = 5 + int.Parse(层数) * 5;
        utm.最高等级 = 5 + int.Parse(层数) * 5;
        utm.基础攻击力 = 5 * 4 * int.Parse(层数);
        utm.基础防御力 = 5 * 2 * int.Parse(层数);
        utm.基础血量 = 5 * 55 * int.Parse(层数);
        utm.基础暴击率 = 5;
        utm.基础回血值 = 5 * 2 * int.Parse(层数);
        utm.攻击力资质 = 0.85f;
        utm.防御力资质 = 0.5f;
        utm.血量资质 = 1.3f;
        utm.成长 = int.Parse(层数);
        utm.攻击速度 = 1.0f;
        utm.基础经验值 = 2000 * int.Parse(层数);
        utm.经验值系数 = (int)( utm.成长* utm.成长);
        utm.金币 = 1;
        cb.技能与粒度.Clear();//赋值前先初始化
        if (!cb.技能与粒度.ContainsKey("防御姿态"))
            cb.技能与粒度.Add("防御姿态", 0.3f);
        if (!cb.技能与粒度.ContainsKey("撞击"))
            cb.技能与粒度.Add("撞击", 0.2f);
    }


    public void 普通怪物赋值4()
    {
        utm.怪物名字 = "≮角马≯";
        utm.怪物品质 = 2;
        utm.是否主动攻击 = false;
        utm.最低等级 = 5 + int.Parse(层数) * 5;
        utm.最高等级 = 5 + int.Parse(层数) * 5;
        utm.基础攻击力 = 5 * 5 * int.Parse(层数);
        utm.基础防御力 = 5 * 2 * int.Parse(层数);
        utm.基础血量 = 5 * 45 * int.Parse(层数);
        utm.基础暴击率 = 10;
        utm.基础回血值 = 5 * 2 * int.Parse(层数);
        utm.攻击力资质 = 0.95f;
        utm.防御力资质 = 0.5f;
        utm.血量资质 = 1.2f;
        utm.成长 = int.Parse(层数);
        utm.攻击速度 = 1.2f;
        utm.基础经验值 = 2000 * int.Parse(层数);
        utm.经验值系数 = (int)( utm.成长* utm.成长);
        utm.金币 = 1;
        cb.技能与粒度.Clear();//赋值前先初始化
        if (!cb.技能与粒度.ContainsKey("拍打"))
            cb.技能与粒度.Add("拍打", 0.2f);
        if (!cb.技能与粒度.ContainsKey("撞击"))
            cb.技能与粒度.Add("撞击", 0.2f);
    }


    public void 普通怪物赋值5()
    {
        utm.怪物名字 = "≮犰狳≯";
        utm.怪物品质 = 2;
        utm.是否主动攻击 = false;
        utm.最低等级 = 5 + int.Parse(层数) * 5;
        utm.最高等级 = 5 + int.Parse(层数) * 5;
        utm.基础攻击力 = 5 * 4 * int.Parse(层数);
        utm.基础防御力 = 5 * 1 * int.Parse(层数);
        utm.基础血量 = 5 * 30 * int.Parse(层数);
        utm.基础暴击率 = 10;
        utm.基础回血值 = 5 * 2 * int.Parse(层数);
        utm.攻击力资质 = 0.85f;
        utm.防御力资质 = 0.45f;
        utm.血量资质 = 1.0f;
        utm.成长 = int.Parse(层数);
        utm.攻击速度 = 1.2f;
        utm.基础经验值 = 2000 * int.Parse(层数);
        utm.经验值系数 = (int)( utm.成长* utm.成长);
        utm.金币 = 1;
        cb.技能与粒度.Clear();//赋值前先初始化
        if (!cb.技能与粒度.ContainsKey("攻击姿态"))
            cb.技能与粒度.Add("攻击姿态", 0.3f);
        if (!cb.技能与粒度.ContainsKey("防御姿态"))
            cb.技能与粒度.Add("防御姿态", 0.3f);
    }



    public void 刷新楼层() {
        Text 楼层文本= gameObject.transform.Find("楼层/Text").GetComponent<Text>();
        楼层文本.text = "第" + 层数 + "层";   
    }

    public void 刷新倒计时(GameObject 倒计时,int timer)
    {
        Text 计时文本 = 倒计时.transform.Find("Text").GetComponent<Text>();
        计时文本.text = timer + "";
    }

    public void 刷新怪物() {
        GameObject 怪物 = gameObject.transform.Find("monster/m2").gameObject.transform.Find("血条2").gameObject;
        combat mcb = 怪物.GetComponent<combat>();
        怪物.SetActive(true);
        mcb.怪物初始化();
    }

    public  IEnumerator  刷新倒计时() {
        GameObject 倒计时 = gameObject.transform.Find("倒计时").gameObject;
        倒计时.SetActive(true);
        int timer = 3;
        while (timer > 0) {
            yield return new WaitForSeconds(1.0f);
            timer--;
            刷新倒计时(倒计时, timer);
        }
        倒计时.SetActive(false);
        刷新楼层();
        刷新怪物();
        刷新倒计时(倒计时, 3);//初始化一下
    }

    public void 进入下一层() {
        if (int.Parse(层数) <= 100) {
            StartCoroutine(刷新倒计时());
        }
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
