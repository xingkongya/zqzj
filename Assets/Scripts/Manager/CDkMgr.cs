using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDkMgr : BaseManager<CDkMgr>
{
    private Dictionary<string,Action> L_CDK;
    private Dictionary<Prop_bascis, int> 物品列表;
    private PropMgr pm = PropMgr.GetInstance();
    private RoleMgr rm = RoleMgr.GetInstance();

    /// <summary>
    /// 先写死,待补充网络模块
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, Action> 返回兑换码列表() {
        L_CDK = new Dictionary<string, Action>();
        L_CDK.Add("VIP666", VIP666);//礼包形式
        L_CDK.Add("VIP777", VIP777);//多物品发放形式
        L_CDK.Add("VIP888", VIP888);//多物品发放形式
        L_CDK.Add("TEST01", TEST01);//多物品发放形式
        L_CDK.Add("TEST30", TEST30);//多物品发放形式
        L_CDK.Add("TEST66", TEST66);//多物品发放形式
        L_CDK.Add("TEST77", TEST77);//多物品发放形式
        L_CDK.Add("TEST88", TEST88);//多物品发放形式
        L_CDK.Add("TEST99", TEST99);//多物品发放形式
        L_CDK.Add("TEST199", TEST199);//多物品发放形式
        L_CDK.Add("TEST288", TEST288);//多物品发放形式
        L_CDK.Add("TEST98", TEST98);//多物品发放形式
        L_CDK.Add("TEST300", TEST300);//多物品发放形式
        L_CDK.Add("TEST1000", TEST1000);//多物品发放形式
        L_CDK.Add("TEST2000", TEST2000);//多物品发放形式
        L_CDK.Add("happy2023", happy2023);//多物品发放形式

        return L_CDK;
    }
    private void TEST01()
    {
        物品列表 = new Dictionary<Prop_bascis, int>();
        物品列表.Add(pm.检索物品("通天塔礼包"), 1000);
        物品列表.Add(pm.检索物品("归元露"), 100);
        物品列表.Add(pm.检索物品("听海石"), 1000);
        pm.获取多个物品并显示特效(物品列表, "掉落");
        rm.记录数据("CDK_TEST01", "1");
    }

    private void TEST30()
    {
        物品列表 = new Dictionary<Prop_bascis, int>();
        物品列表.Add(pm.检索物品("≮黄金利爪≯"), 1);
        物品列表.Add(pm.检索物品("≮黄金羽衣≯"), 1);
        物品列表.Add(pm.检索物品("≮黄金羽翼≯"), 1);
        物品列表.Add(pm.检索物品("≮金色琉璃≯"), 1);
        物品列表.Add(pm.检索物品("≮黄金羽冠≯"), 1);
        pm.获取多个物品并显示特效(物品列表, "掉落");
        rm.记录数据("CDK_TEST01", "1");
    }


    private void TEST300()
    {
        物品列表 = new Dictionary<Prop_bascis, int>();
        物品列表.Add(pm.检索物品("技能书<焚诀>"), 1);
        pm.获取多个物品并显示特效(物品列表, "掉落");
        rm.记录数据("CDK_TEST300", "1");
    }


    private void TEST66()
    {
        物品列表 = new Dictionary<Prop_bascis, int>();
        物品列表.Add(pm.检索物品("血菩提"), 66);
        pm.获取多个物品并显示特效(物品列表, "掉落");
        rm.记录数据("CDK_TEST66", "1");
    }
    private void TEST77()
    {
        物品列表 = new Dictionary<Prop_bascis, int>();
        物品列表.Add(pm.检索物品("下品涅槃果"), 77);
        pm.获取多个物品并显示特效(物品列表, "掉落");
        rm.记录数据("CDK_TEST77", "1");
    }

    private void TEST88()
    {
        物品列表 = new Dictionary<Prop_bascis, int>();
        物品列表.Add(pm.检索物品("灰色精华"), 100);
        物品列表.Add(pm.检索物品("蓝色精华"), 500);
        物品列表.Add(pm.检索物品("神兽精华"), 100);
        物品列表.Add(pm.检索物品("紫色精华"), 1000);
        物品列表.Add(pm.检索物品("仙兽气息"), 100);
        pm.获取多个物品并显示特效(物品列表, "掉落");
        rm.记录数据("CDK_TEST88", "1");
    }

    private void TEST99() {
        物品列表 = new Dictionary<Prop_bascis, int>();
        物品列表.Add(pm.检索物品("造化金丹"), 99);
        pm.获取多个物品并显示特效(物品列表,"掉落");
        rm.记录数据("CDK_TEST99", "1");
    }

    private void TEST199()
    {
        物品列表 = new Dictionary<Prop_bascis, int>();
        物品列表.Add(pm.检索物品("鲲鹏之卵"), 199);
        pm.获取多个物品并显示特效(物品列表, "掉落");
        rm.记录数据("CDK_TEST199", "1");
    }

    private void TEST288()
    {
        物品列表 = new Dictionary<Prop_bascis, int>();
        物品列表.Add(pm.检索物品("坤坤之卵"), 1);
        pm.获取多个物品并显示特效(物品列表, "掉落");
        rm.记录数据("CDK_TEST288", "1");
    }

    private void TEST1000()
    {
        物品列表 = new Dictionary<Prop_bascis, int>();
        物品列表.Add(pm.检索物品("≮黄中李≯"), 1000);
        pm.获取多个物品并显示特效(物品列表, "掉落");
        rm.记录数据("CDK_TEST1000", "1");
    }

    private void TEST2000()
    {
        物品列表 = new Dictionary<Prop_bascis, int>();
        物品列表.Add(pm.检索物品("≮九千年壬水蟠桃≯"), 2000);
        pm.获取多个物品并显示特效(物品列表, "掉落");
        rm.记录数据("CDK_TEST2000", "1");
    }

    private void happy2023()
    {
        物品列表 = new Dictionary<Prop_bascis, int>();
        物品列表.Add(pm.检索物品("2023礼包"), 1);
        物品列表.Add(pm.检索物品("技能书<夺命三仙剑(伪)>"), 1);
        物品列表.Add(pm.检索物品("技能书<大荒囚天拳(伪)>"), 1);
        pm.获取多个物品并显示特效(物品列表, "掉落");
        rm.记录数据("CDK_happy2023", "1");
    }

    private void TEST98()
    {
        物品列表 = new Dictionary<Prop_bascis, int>();
        物品列表.Add(pm.检索物品("真龙精血"), 99);
        pm.获取多个物品并显示特效(物品列表, "掉落");
        rm.记录数据("CDK_TEST98", "1");
    }

    private void VIP666()
    {
        物品列表 = new Dictionary<Prop_bascis, int>();
        物品列表.Add(pm.检索物品("Vip礼包1"), 1);
        物品列表.Add(pm.检索物品("1000仙晶卡"), 666);
        pm.获取多个物品并显示特效(物品列表, "掉落");
        rm.记录数据("CDK_VIP666", "1");
    }

    private void VIP888()
    {
        物品列表 = new Dictionary<Prop_bascis, int>();
        物品列表.Add(pm.检索物品("灰色精华"), 1);
        物品列表.Add(pm.检索物品("绿色精华"), 1);
        物品列表.Add(pm.检索物品("蓝色精华"), 1);
        物品列表.Add(pm.检索物品("紫色精华"), 1);
        物品列表.Add(pm.检索物品("仙兽精华"), 1);
        pm.获取多个物品并显示特效(物品列表, "掉落");
        rm.记录数据("CDK_VIP888", "1");
    }

    private void VIP777()
    {
        物品列表 = new Dictionary<Prop_bascis, int>();
        物品列表.Add(pm.检索物品("蓝色精华"), 777);   
        pm.获取多个物品并显示特效(物品列表, "掉落");
        rm.记录数据("CDK_VIP777", "1");
    }
}
