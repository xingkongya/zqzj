using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillMgr : BaseManager<SkillMgr>
{
    private io io_ = io.GetInstance();
    private PropMgr pm = PropMgr.GetInstance();
    private SkillEffect se = SkillEffect.GetInstance();
    private basicMgr bm = basicMgr.GetInstance();
    private DataMgr dm = DataMgr.GetInstance();

    public void 加载技能配置()
    {
        dm.技能属性 = bm.返回空的战斗属性();
        role_Data myData = io_.load();
        Dictionary<string, Action> sed = se.加载技能效果();
        for (int i = 0; i < myData.技能槽.Count; i++)
        {
            if (!myData.技能槽[i + ""].Equals(""))
            {
                SkillData sd = 返回技能背包该名称的技能(myData.技能槽[i + ""]);
                if (sd.get.Equals("被动增益"))
                    sed[myData.技能槽[i + ""]]();
            }
        }
    }

    public void 持久化技能槽数据(string index, SkillData 待入槽技能)
    {
        role_Data myData = io_.load();
        待入槽技能.username = myData.名字;
        if (myData.技能槽.ContainsKey(index))
            myData.技能槽[index] = 待入槽技能.name;
        else
            myData.技能槽.Add(index, 待入槽技能.name);
        io_.save(myData);

    }


    public SkillData 返回技能背包该名称的技能(string name)
    {
        role_Data myData = io_.load();
        string place = pm.检索技能(name).place;
        foreach (SkillData sd in myData.技能背包[place])
        {
            if (sd.name.Equals(name))
                return sd;
        }
        return new SkillData();
    }

    public void 持久化技能背包数据(string place, SkillData sd)
    {
        role_Data myData = io_.load();
        if (!myData.技能背包.ContainsKey(place))
            myData.技能背包.Add(place, new List<SkillData>());
        myData.技能背包[place].Add(sd);
        io_.save(myData);

    }

    public void 删除技能背包数据(string place, SkillData sd)
    {
        role_Data myData = io_.load();
        for (int i = 0; i < myData.技能背包[place].Count; i++) {
            if (myData.技能背包[place][i].name.Equals(sd.name))
                myData.技能背包[place].RemoveAt(i);
        }
        io_.save(myData);
    }

    public bool 技能查重(string place, SkillData sd)
    {
        role_Data myData = io_.load();
        if (!myData.技能背包.ContainsKey(place))//检查是否有该类型的主键,没有则创建一个
        {
            myData.技能背包.Add(place, new List<SkillData>());
            io_.save(myData);
        }

        foreach (SkillData the_sd in myData.技能背包[place])
        {
            if (the_sd.name.Equals(sd.name))//在该主键下的列表查看是否包含这个技能
                return true;
        }
        return false;
    }

    public void 加载心法()
    {
        string str = GameObject.FindGameObjectWithTag("选中").transform.Find("name").GetComponent<Text>().text;
        SkillData sd = 返回技能背包该名称的技能(str);
        技能穿戴效果(sd, 0);
        if (!sd.name.Equals(""))
            持久化技能槽数据("0", sd);
    }


    public void 加载绝招()
    {
        G_Util gut = NameMgr.画布.GetComponent<G_Util>();
        string str = GameObject.FindGameObjectWithTag("选中").transform.Find("name").GetComponent<Text>().text;
        SkillData sd = 返回技能背包该名称的技能(str);
        技能穿戴效果(sd, 1);
        if (!sd.name.Equals(""))
        {
            持久化技能槽数据("1", sd);
            gut.初始化绝招图标();
            GameObject.Find("绝招拉条").GetComponent<CDSlider>().初始化拉条数据();
        }
    }

    public void 加载被动一()
    {
        string str = GameObject.FindGameObjectWithTag("选中").transform.Find("name").GetComponent<Text>().text;
        SkillData sd = 返回技能背包该名称的技能(str);
        技能穿戴效果(sd, 2);
        if (!sd.name.Equals(""))
            持久化技能槽数据("2", sd);
    }

    public void 加载被动二()
    {
        string str = GameObject.FindGameObjectWithTag("选中").transform.Find("name").GetComponent<Text>().text;
        SkillData sd = 返回技能背包该名称的技能(str);
        技能穿戴效果(sd, 3);
        if (!sd.name.Equals(""))
            持久化技能槽数据("3", sd);
    }

    public void 加载被动三()
    {
        string str = GameObject.FindGameObjectWithTag("选中").transform.Find("name").GetComponent<Text>().text;
        SkillData sd = 返回技能背包该名称的技能(str);
        技能穿戴效果(sd, 4);
        if (!sd.name.Equals(""))
            持久化技能槽数据("4", sd);
    }

    private void 技能穿戴效果(SkillData sd, int index)
    {
        Text 装备槽;
        if (index == 0)
            装备槽 = GameObject.Find("属性面板(Clone)/bg/中部/技能页/心法/Text").GetComponent<Text>();
        else if (index == 1)
            装备槽 = GameObject.Find("属性面板(Clone)/bg/中部/技能页/绝招/Text").GetComponent<Text>();
        else if (index == 2)
            装备槽 = GameObject.Find("属性面板(Clone)/bg/中部/技能页/武学_1/Text").GetComponent<Text>();
        else if (index == 3)
            装备槽 = GameObject.Find("属性面板(Clone)/bg/中部/技能页/武学_2/Text").GetComponent<Text>();
        else if (index == 4)
            装备槽 = GameObject.Find("属性面板(Clone)/bg/中部/技能页/武学_3/Text").GetComponent<Text>();
        else
            装备槽 = null;
        装备槽.text = sd.name;
        装备槽.color = bm.转换颜色(bm.Xstoi(sd.qua));
    }

    /// <summary>
    /// 在道具效果里调用
    /// </summary>
    public bool 学习技能(string SkillName)
    {
        G_Util gut = NameMgr.画布.GetComponent<G_Util>();
        SkillData sd = pm.检索技能(SkillName);
        role_Data myData = io_.load();
        if (bm.Xstoi(myData.等级) < bm.Xstoi(sd.lessgrade))
        {
            gut.生成警告框(sd.lessgrade + "级才能学习该技能");
            return false;
        }
        if (sd!=null&&!技能查重(sd.place, sd))
        {
            if (sd.place.Equals("心法"))
                持久化技能背包数据("心法", sd);
            else if (sd.place.Equals("绝招"))
                持久化技能背包数据("绝招", sd);
            else if (sd.place.Equals("被动"))
                持久化技能背包数据("被动", sd);
            else
                Debug.Log("sd出错");
        }
        else
        {
            gut.生成警告框("已经学过了");
            return false;
        }
        return true;
    }

    /// <summary>
    /// 学习界面使用
    /// </summary>
    public void 学习技能_无参()
    {
        GameObject Skill = GameObject.FindGameObjectWithTag("选中").gameObject.transform.Find("name").gameObject;
        G_Util gut = NameMgr.画布.GetComponent<G_Util>();
        string SkillName = gut.获取对象Text上的字符串(Skill);
        SkillData sd = pm.检索技能(SkillName);
        if (sd != null && !技能查重(sd.place, sd))
        {
            if (sd.place.Equals("心法"))
                持久化技能背包数据("心法", sd);
            else if (sd.place.Equals("绝招"))
                持久化技能背包数据("绝招", sd);
            else if (sd.place.Equals("被动"))
                持久化技能背包数据("被动", sd);
            else
                Debug.Log("sd出错");
        }
        else
        {
            gut.生成警告框("已经学过了");
        }
    }


    /// <summary>
    /// 学习界面使用
    /// </summary>
    public void 遗忘技能_无参()
    {
        GameObject Skill = GameObject.FindGameObjectWithTag("选中").gameObject.transform.Find("name").gameObject;
        G_Util gut = NameMgr.画布.GetComponent<G_Util>();
        string SkillName = gut.获取对象Text上的字符串(Skill);
        SkillData sd = pm.检索技能(SkillName);

        if (sd.place.Equals("心法"))
            删除技能背包数据("心法", sd);
        else if (sd.place.Equals("绝招"))
            删除技能背包数据("绝招", sd);
        else if (sd.place.Equals("被动"))
            删除技能背包数据("被动", sd);
        else
            Debug.Log("sd出错");
    }


}
