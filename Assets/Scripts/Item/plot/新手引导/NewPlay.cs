using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NewPlay : BaseManager<NewPlay>
{
    private G_Util gut = NameMgr.画布.GetComponent<G_Util>();
    private EventCenter em = EventCenter.GetInstance();
    private PropMgr pm = PropMgr.GetInstance();



    public void 关闭引导()
    {
        gut.实例化对象池["引导"].gameObject.SetActive(false);
    }

    public void 关闭提示()
    {
        gut.实例化对象池["提示"].gameObject.SetActive(false);
    }

    public void 生成提示(string 名字, Sprite 立绘, string 内容, bool isWait, UnityAction 事件)
    {
        //如果iswait是true,等待对话打印结束执行
        if (isWait)
        {
            em.AddEventListener("对话结束生成引导", 事件);
        }
        if (gut.实例化对象池.ContainsKey("提示"))
        {
            GameObject 提示 = gut.实例化对象池["提示"];
            提示.SetActive(true);
            提示.transform.Find("角色立绘").GetComponent<Image>().sprite = 立绘;
            提示.transform.Find("角色立绘/名字/Text").GetComponent<Text>().text = 名字;
            提示.transform.Find("描述").GetComponent<Text_DaYin>().str = 内容;
            提示.transform.Find("描述").GetComponent<Text_DaYin>().playText();
        }
        else
        {
            gut.生成提示(名字, 立绘, 内容);
        }
    }

    /// <summary>
    /// 参数事件一般都是下一步的引导
    /// </summary>
    /// <param name="坐标"></param>
    /// <param name="事件"></param>
    /// <param name="isWait"></param>
    public void 生成引导(Vector2 坐标)
    {
        if (gut.实例化对象池.ContainsKey("引导"))
        {
            GameObject 引导 = gut.实例化对象池["引导"];
            引导.SetActive(true);
            引导.transform.localPosition = 坐标;
        }
        else
        {
            gut.生成引导(坐标);
        }
    }


    public void 空方法()
    {

    }

    public void 初次进入游戏()
    {
        string 内容 = "点击NPC-母亲,开启剧情";
        Sprite 立绘 = Resources.Load<Sprite>("怪物/老鼠");
        生成提示("作者", 立绘, 内容, true, 提示引导01);

    }


    private void 提示001()
    {
        Sprite 立绘 = Resources.Load<Sprite>("怪物/老鼠");
        string 内容 = "对话没结束之前是无法关闭对话框的";
        生成提示("作者", 立绘, 内容, false, 空方法);
    }

    private void 提示引导01()
    {
        Sprite 立绘 = Resources.Load<Sprite>("怪物/老鼠");
        if (GameObject.Find("母亲"))
        {
            jq_jia jj = GameObject.Find("母亲").GetComponent<jq_jia>();
            em.AddEventListener("引导", jj.母亲对话索引);
            em.AddEventListener("引导", 提示001);
            生成引导(new Vector2(0, 200));//当前引导
                                      //点击后
            em.AddEventListener("对话结束", 引导001);//点击后要生成的引导
        }
    }

    public void 关闭对话框()
    {
        Text_DaYin tdy = GameObject.Find("对话框(Clone)").GetComponent<Text_DaYin>();
        tdy.关闭对话框();
    }


    private void 引导001()
    {

        em.AddEventListener("引导", 关闭对话框);
        em.AddEventListener("引导", 提示002);
        生成引导(new Vector2(0, 100));
    }

    private void 提示引导02()
    {
        em.AddEventListener("引导", gut.生成背包);
        em.AddEventListener("引导", 提示003);
        生成引导(new Vector2(410, 662));
    }

    private void 提示002()
    {
        Sprite 立绘 = Resources.Load<Sprite>("怪物/老鼠");
        string 内容 = "馒头可以理解成血药,我们把它装备上吧.先点击背包";
        生成提示("作者", 立绘, 内容, true, 提示引导02);
    }


    private void 提示003()
    {
        Sprite 立绘 = Resources.Load<Sprite>("怪物/老鼠");
        string 内容 = "选中血药,然后点击使用";
        生成提示("作者", 立绘, 内容, true, 等待调用引导003);
    }

    public void 等待调用引导003()
    {
        gut.启动延时方法(引导003, 0.5f);
    }
    private void 引导003()
    {
        点击背包项("馒头");
        em.AddEventListener("生成引导", 引导004);
        生成引导(new Vector2(0, 350));
    }
    private void 提示004()
    {
        Sprite 立绘 = Resources.Load<Sprite>("怪物/老鼠");
        string 内容 = "血药会在战斗中恢复一定的生命值,可以手动使用也可以设置自动使用";
        生成提示("作者", 立绘, 内容, true, 引导005);
    }

    private void 引导004()
    {
        em.AddEventListener("引导", 提示004);
        em.AddEventListener("引导", gut.使用道具);
        生成引导(new Vector2(-219, -219));
    }

    private void 提示005()
    {
        Sprite 立绘 = Resources.Load<Sprite>("怪物/老鼠");
        string 内容 = "让我们开启自动使用吧.生命值低于70%时将自动使用道具,首先点击战斗设置按钮";
        生成提示("作者", 立绘, 内容, true, 引导006);
    }

    private void 引导005()
    {
        if (GameObject.Find("背包(Clone)"))
        {
            Bag bag = GameObject.Find("背包(Clone)").GetComponent<Bag>();
            em.AddEventListener("引导", bag.关闭背包);
        }
        em.AddEventListener("引导", 提示005);
        生成引导(new Vector2(220, 492));
    }

    private void 提示006()
    {
        Sprite 立绘 = Resources.Load<Sprite>("怪物/老鼠");
        string 内容 = "勾选-自动使用道具";
        生成提示("作者", 立绘, 内容, true, 引导007);
    }

    private void 引导006()
    {
        em.AddEventListener("引导", 打开战斗设置界面);
        em.AddEventListener("引导", 提示006);
        生成引导(new Vector2(400, -285));
    }

    private void 打开战斗设置界面()
    {
        GameObject.Find("combat_bg/2级画布").transform.Find("战斗设置面板").gameObject.SetActive(true);
    }

    private void 引导007()
    {
        em.AddEventListener("引导", 设置战斗设置界面);
        em.AddEventListener("生成引导", 引导008);
        生成引导(new Vector2(400, -265));
    }
    private void 设置战斗设置界面()
    {
        GameObject button = GameObject.Find("combat_bg/2级画布/战斗设置面板/自动使用道具/Button");
        button.transform.Find("打勾").gameObject.SetActive(true);
        button.GetComponent<SellSetting>().战斗设置();
    }
    private void 引导008()
    {
        em.AddEventListener("引导", 关闭战斗设置界面);
        em.AddEventListener("引导", 提示007);
        生成引导(new Vector2(416, 44));
    }

    private void 关闭战斗设置界面()
    {
        GameObject.Find("combat_bg/2级画布").transform.Find("战斗设置面板").gameObject.SetActive(false);
    }

    private void 提示007()
    {
        Sprite 立绘 = Resources.Load<Sprite>("怪物/老鼠");
        string 内容 = "继续对话NPC-母亲";
        生成提示("作者", 立绘, 内容, true, 引导009);
    }

    private void 引导009()
    {
        if (GameObject.Find("母亲"))
        {
            jq_jia jj = GameObject.Find("母亲").GetComponent<jq_jia>();
            em.AddEventListener("引导", jj.母亲对话索引);
            em.AddEventListener("对话结束", 引导010);
            生成引导(new Vector2(0, 200));//当前引导
        }
    }

    private void 引导010()
    {
        em.AddEventListener("引导", 关闭对话框);
        em.AddEventListener("引导", 提示008);
        生成引导(new Vector2(0, 100));//当前引导
    }

    private void 提示008()
    {
        Sprite 立绘 = Resources.Load<Sprite>("怪物/老鼠");
        string 内容 = "出发!前往桃源村...";
        生成提示("作者", 立绘, 内容, true, 引导011);
    }

    public void 跳转场景_桃源村()
    {
        gut.跳转场景("桃源村");
    }
    private void 引导011()
    {
        em.AddEventListener("引导", 跳转场景_桃源村);
        em.AddEventListener("引导", 提示009);
        生成引导(new Vector2(0, 463));//当前引导
    }
    private void 提示009()
    {
        Sprite 立绘 = Resources.Load<Sprite>("怪物/老鼠");
        string 内容 = "介绍一下各种NCP:\n牛铁匠---可以打造装备.\n村长---进行任务或对话.\n王二---学习技能.";
        生成提示("作者", 立绘, 内容, true, 引导012);
    }

    private void 引导012()
    {
        em.AddEventListener("引导", 提示0010);
        生成引导(new Vector2(326, -600));//当前引导
    }
    private void 提示0010()
    {
        Sprite 立绘 = Resources.Load<Sprite>("怪物/老鼠");
        string 内容 = "村口西有医师可以免费补血,被怪打残血了一定要找他们";
        生成提示("作者", 立绘, 内容, true, 引导013);
    }
    private void 引导013()
    {
        em.AddEventListener("引导", 提示0011);
        生成引导(new Vector2(326, -600));//当前引导
    }
    private void 提示0011()
    {
        Sprite 立绘 = Resources.Load<Sprite>("怪物/老鼠");
        string 内容 = "先点击村长吧...";
        生成提示("作者", 立绘, 内容, true, 引导014);
    }
    private void 引导014()
    {
        jq_kaoshan jq = GameObject.Find("村长").GetComponent<jq_kaoshan>();
        em.AddEventListener("引导", 提示0012);
        em.AddEventListener("引导", jq.村长对话索引);
        生成引导(new Vector2(0, 360));//当前引导
    }
    private void 提示0012()
    {
        Sprite 立绘 = Resources.Load<Sprite>("怪物/老鼠");
        string 内容 = "点击-村长的帮助-选项...";
        生成提示("作者", 立绘, 内容, true, 引导015);//为true则为下一引导
    }
    private void 引导015()
    {
        jq_kaoshan jq = GameObject.Find("村长").GetComponent<jq_kaoshan>();
        em.AddEventListener("引导", jq.村长剧情4);
        em.AddEventListener("对话结束", 引导015额外);
        生成引导(new Vector2(0, 360));//当前引导
    }
    private void 引导015额外()
    {
        em.AddEventListener("引导", 关闭对话框);
        em.AddEventListener("引导", 提示0013);
        生成引导(new Vector2(0, 100));//当前引导
    }
    private void 提示0013()
    {
        Sprite 立绘 = Resources.Load<Sprite>("怪物/老鼠");
        string 内容 = "哇!拿到装备了,但是这个装备5级才能穿哦,加油升级!\n作者友情赠送你一个平安符,4个木材...快去铁匠打造一级全套装备吧.";
        em.AddEventListener("对话结束生成引导", 引导奖励1);
        生成提示("作者", 立绘, 内容, true, 引导016);//为true则为下一引导
    }

    private void 引导奖励1()
    {

        Dictionary<Prop_bascis, int> 物品列表 = new Dictionary<Prop_bascis, int>();
        物品列表.Add(pm.检索物品("平安符"), 1);
        物品列表.Add(pm.检索物品("木材"), 4);
        pm.获取多个物品并显示特效(物品列表, "掉落");
    }

    private void 引导奖励2()
    {

        Dictionary<Prop_bascis, int> 物品列表 = new Dictionary<Prop_bascis, int>();
        物品列表.Add(pm.检索物品("Vip礼包1"), 1);
        pm.获取多个物品并显示特效(物品列表, "掉落");
    }

    private void 引导016()
    {
        jq_kaoshan jq = GameObject.Find("牛铁匠").GetComponent<jq_kaoshan>();
        em.AddEventListener("引导", jq.牛铁匠对话索引);
        em.AddEventListener("生成引导", 引导017);
        生成引导(new Vector2(-300, 360));//当前引导
    }
    private void 引导017()
    {
        jq_kaoshan jq = GameObject.Find("牛铁匠").GetComponent<jq_kaoshan>();
        em.AddEventListener("引导", jq.生成打造界面);
        em.AddEventListener("引导", 提示0014额外01);
        生成引导(new Vector2(-300, 415));//当前引导
    }
    private void 点击打造装备(string name)
    {

        if (GameObject.Find("打造_" + name) && GameObject.Find("道具界面(Clone)"))
        {
            Ini_Building IB_装备 = GameObject.Find("道具界面(Clone)").GetComponent<Ini_Building>();
            ChangeTag CT_装备 = GameObject.Find("打造_" + name).GetComponent<ChangeTag>();
            em.AddEventListener("引导", CT_装备.OnClick);
            em.AddEventListener("引导", IB_装备.打造装备);
        }

    }

    private void 关闭打造页面()
    {
        GameObject.Find("道具界面(Clone)").gameObject.SetActive(false);
    }

    private void 引导018()
    {
        点击打造装备("木剑");
        em.AddEventListener("生成引导", 引导019);
        生成引导(new Vector2(240, 320));//当前引导
    }

    private void 提示0014额外01()
    {
        Sprite 立绘 = Resources.Load<Sprite>("怪物/老鼠");
        string 内容 = "点击打造按钮";
        生成提示("作者", 立绘, 内容, true, 引导018);//为true则为下一引导
    }
    private void 引导019()
    {
        点击打造装备("破布衣");
        em.AddEventListener("引导", 提示0014额外02);
        生成引导(new Vector2(240, 160));//当前引导
    }

    private void 引导019_20()
    {
        点击打造装备("破布帽");
        em.AddEventListener("生成引导", 引导020);
        生成引导(new Vector2(240, 20));//当前引导
    }

    private void 提示0014额外02()
    {
        Sprite 立绘 = Resources.Load<Sprite>("怪物/老鼠");
        string 内容 = "继续打造...";
        生成提示("作者", 立绘, 内容, true, 引导019_20);//为true则为下一引导
    }

    private void 提示0014额外03()
    {
        Sprite 立绘 = Resources.Load<Sprite>("怪物/老鼠");
        string 内容 = "好!打造完成了";
        生成提示("作者", 立绘, 内容, true, 引导021);//为true则为下一引导
    }

    private void 引导020()
    {
        点击打造装备("破布鞋");
        em.AddEventListener("引导", 提示0014额外03);
        生成引导(new Vector2(240, -150));//当前引导
    }
    private void 引导021()
    {
        em.AddEventListener("引导", 关闭打造页面);
        em.AddEventListener("引导", 提示0014);
        生成引导(new Vector2(333, 493));//当前引导
    }
    private void 提示0014()
    {
        Sprite 立绘 = Resources.Load<Sprite>("怪物/老鼠");
        string 内容 = "穿上这样一套装备,岂不随便在低级区掌控雷电!!\n点击背包,赶快穿上吧";
        生成提示("作者", 立绘, 内容, true, 引导022);//为true则为下一引导
    }
    private void 引导022()
    {
        em.AddEventListener("引导", gut.生成背包);
        em.AddEventListener("引导", 提示0015);
        生成引导(new Vector2(410, 662));
    }
    private void 提示0015()
    {
        Sprite 立绘 = Resources.Load<Sprite>("怪物/老鼠");
        string 内容 = "点击装备页.";
        生成提示("作者", 立绘, 内容, true, 引导023);//为true则为下一引导
    }
    private void 引导023()
    {
        if (GameObject.Find("背包(Clone)"))
        {
            Bag bag = GameObject.Find("背包(Clone)").GetComponent<Bag>();
            em.AddEventListener("引导", bag.点击装备页);
        }
        em.AddEventListener("引导", 提示0016);
        生成引导(new Vector2(-20, 500));
    }
    private void 提示0016()
    {
        Sprite 立绘 = Resources.Load<Sprite>("怪物/老鼠");
        string 内容 = "为了方便,这里我就一次性穿上所有的一级装备了";
        生成提示("作者", 立绘, 内容, true, 引导024);//为true则为下一引导
    }

    private void 点击背包项(string name)
    {
        if (GameObject.Find(name))
        {
            ChangeTag CT_物品 = GameObject.Find(name).GetComponent<ChangeTag>();
            Bag_PropOption BPO_物品 = GameObject.Find(name).GetComponent<Bag_PropOption>();
            em.AddEventListener("引导", CT_物品.OnClick);
            em.AddEventListener("引导", BPO_物品.点击物品项);
        }
    }
    private void 引导024()
    {
        点击背包项("平安符");
        em.AddEventListener("生成引导", 引导025);
        生成引导(new Vector2(0, 265));
    }
    private void 引导025()
    {
        em.AddEventListener("引导", gut.使用道具);
        点击背包项("木剑");
        em.AddEventListener("引导", gut.使用道具);
        点击背包项("破布衣");
        em.AddEventListener("引导", gut.使用道具);
        点击背包项("破布帽");
        em.AddEventListener("引导", gut.使用道具);
        点击背包项("破布鞋");
        em.AddEventListener("引导", gut.使用道具);
        em.AddEventListener("引导", 提示0017);
        生成引导(new Vector2(-219, -219));
    }
    private void 提示0017()
    {
        Sprite 立绘 = Resources.Load<Sprite>("怪物/老鼠");
        string 内容 = "好了,新手引导结束了,开启你的冒险吧";
        生成提示("作者", 立绘, 内容, true, 引导026);//为true则为下一引导
    }
    private void 引导026()
    {

        if (GameObject.Find("背包(Clone)"))
        {
            Bag bag = GameObject.Find("背包(Clone)").GetComponent<Bag>();
            em.AddEventListener("引导", bag.关闭背包);
        }
        em.AddEventListener("引导", 关闭提示);
        //em.AddEventListener("引导", 引导奖励2);
        生成引导(new Vector2(220, 492));

    }



}
