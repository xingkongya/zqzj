using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MonsterMgr : BaseManager<MonsterMgr>
{
    private GameObject[] Monsters;
    private List<string> 敌人名字集合;


    /// <summary>
    /// 获取场上所有怪物列表
    /// </summary>
    /// <returns></returns>
    public List<GameObject> GetAllMonsters()
    {
        List<GameObject> L_Monsters = new List<GameObject>();
        Monsters = GameObject.FindGameObjectsWithTag("怪物");
        foreach (GameObject 怪物 in Monsters)
        {
            L_Monsters.Add(怪物);
        }
        return L_Monsters;
    }

    /// <summary>
    /// 获取目标父物体编号
    /// </summary>
    /// <returns></returns>
    private int 获取下标(GameObject 目标)
    {
        int MonsterIndex;
        if (目标 != null)
        {
            string ParentName = 目标.transform.parent.name;
            if (ParentName.Contains("m"))
                MonsterIndex = int.Parse(ParentName.Replace("m", ""));//怪物
            else
                MonsterIndex = int.Parse(ParentName.Replace("r", ""));//主角方
            return MonsterIndex;
        }
        else
            return -1;

    }

    /// <summary>
    /// 添加下标减三的目标
    /// </summary>
    /// <returns></returns>
    private List<GameObject> 添加下标减三的目标(GameObject 目标, List<GameObject> L_Monsters)
    {
        GameObject OtherParent;
        int MonsterIndex = 获取下标(目标);
        if (目标.transform.parent.name.Contains("m"))
            OtherParent = GameObject.Find("combat_other/monster/m" + (MonsterIndex - 3));
        else
            OtherParent = GameObject.Find("combat(Clone)/role/r" + (MonsterIndex - 3));
        GameObject 待添加 = OtherParent.transform.GetChild(0).gameObject;
        L_Monsters.Add(待添加);
        return L_Monsters;
    }


    /// <summary>
    /// 添加下标加三的目标
    /// </summary>
    /// <returns></returns>
    private List<GameObject> 添加下标加三的目标(GameObject 目标, List<GameObject> L_Monsters)
    {
        GameObject OtherParent;
        int MonsterIndex = 获取下标(目标);
        if (目标 == null)
            return new List<GameObject>();
        if (目标.transform.parent.name.Contains("m"))
            OtherParent = GameObject.Find("combat_other/monster/m" + (MonsterIndex + 3));
        else
            OtherParent = GameObject.Find("combat(Clone)/role/r" + (MonsterIndex + 3));
        //Debug.Log(OtherParent.name);
        if (OtherParent.transform.childCount > 0) {
            GameObject 待添加 = OtherParent.transform.GetChild(0).gameObject;
            L_Monsters.Add(待添加);
        }
        return L_Monsters;
    }


    /// <summary>
    /// 添加一列目标
    /// </summary>
    /// <returns></returns>
    private List<GameObject> 添加一列目标(GameObject 目标, List<GameObject> L_Monsters)
    {
        GameObject OtherParent;
        GameObject 待添加;
        int MonsterIndex = 获取下标(目标);
        L_Monsters.Add(目标);
        if (MonsterIndex <= 3)
        {
            L_Monsters = 添加下标加三的目标(目标, L_Monsters);
            if (目标.name.Contains("m"))
                OtherParent = GameObject.Find("combat_other/monster/m" + (MonsterIndex + 6));
            else
                OtherParent = GameObject.Find("combat(Clone)/role/r" + (MonsterIndex + 6));
        }
        else if (MonsterIndex > 3 && MonsterIndex <= 6)
        {
            L_Monsters = 添加下标减三的目标(目标, L_Monsters);
            if (目标.name.Contains("m"))
                OtherParent = GameObject.Find("combat_other/monster/m" + (MonsterIndex + 3));
            else
                OtherParent = GameObject.Find("combat(Clone)/role/r" + (MonsterIndex + 3));
        }
        else if (MonsterIndex==-1)
        {
            return new List<GameObject>();
        }
        else
        {
            L_Monsters = 添加下标减三的目标(目标, L_Monsters);
            if (目标.name.Contains("m"))
                OtherParent = GameObject.Find("combat_other/monster/m" + (MonsterIndex -3));
            else
                OtherParent = GameObject.Find("combat(Clone)/role/r" + (MonsterIndex -3));
        }
        if (OtherParent != null)
        {
            待添加 = OtherParent.transform.GetChild(0).gameObject;
            L_Monsters.Add(待添加);
        }

        return L_Monsters;
    }


    /// <summary>
    /// 指定攻击一列敌人
    /// </summary>
    /// <returns></returns>
    public List<GameObject> 指定攻击一列敌人(GameObject 正在攻击目标)
    {
        List<GameObject> L_Monsters = new List<GameObject>();
        L_Monsters=添加一列目标(正在攻击目标, L_Monsters);
        return L_Monsters;
    }

    /// <summary>
    /// 攻击一列敌人返回的列表
    /// </summary>
    /// <returns></returns>
    public List<GameObject> 指定攻击一列二个敌人(GameObject 正在攻击目标)
    {
        List<GameObject> L_Monsters = new List<GameObject>();
        L_Monsters.Add(正在攻击目标);
        int MonsterIndex = 获取下标(正在攻击目标);
        if (MonsterIndex <= 3)//攻击目标位置在第一排
            L_Monsters = 添加下标加三的目标(正在攻击目标, L_Monsters);
        else if (MonsterIndex > 3 && MonsterIndex <= 6)//攻击目标位置在第二排
        {
            GameObject isNull = GameObject.Find("m8");
            if ((isNull != null && !isNull.CompareTag("怪物")) || isNull == null)//没有第三排
                L_Monsters = 添加下标减三的目标(正在攻击目标, L_Monsters);
            else//有第三排
            {
                System.Random rd = new System.Random();
                int RandomInt = rd.Next(0, 2);//生成0到1的随机数
                if (RandomInt == 0)//等于0,向下打
                    L_Monsters = 添加下标加三的目标(正在攻击目标, L_Monsters);
                else//等于1,向上打
                    L_Monsters = 添加下标减三的目标(正在攻击目标, L_Monsters);
            }
        }
        else if (MonsterIndex == -1) {
            return new List<GameObject>();
        }
        else//攻击目标位置在第三排
            L_Monsters = 添加下标减三的目标(正在攻击目标, L_Monsters);

        return L_Monsters;
    }

    public GameObject 随意寻找一个目标(string 敌人) {
        G_Util gut = NameMgr.画布.GetComponent<G_Util>();
        Dictionary<string, GameObject> 敌人集合;
        if (敌人.Equals("怪物"))
            敌人集合 = gut.现有怪物集合;
        else
            敌人集合 = gut.现有角色集合;
        if (敌人集合.Count != 0)
        {
            敌人名字集合 = new List<string>();
            foreach (string name in 敌人集合.Keys) {
                敌人名字集合.Add(name);
            }

            System.Random 随机类 = new System.Random();
            int 随机数 = 随机类.Next(0, gut.现有怪物集合.Count);
            GameObject ms = 敌人集合[敌人名字集合[随机数]];
            return ms;
        }
        return null;
    }

    /// <summary>
    /// 攻击一列敌人返回的列表
    /// </summary>
    /// <returns></returns>
    public List<GameObject> 随意攻击一列二个敌人(string 敌人)
    {
        List<GameObject> L_Monsters;
        GameObject 敌人_ = 随意寻找一个目标( 敌人);
        L_Monsters= 指定攻击一列二个敌人(敌人_);
        return L_Monsters;
    }


    public List<GameObject> 随意攻击一列敌人(string 敌人)
    {
        List<GameObject> L_Monsters;
        GameObject 敌人_ = 随意寻找一个目标( 敌人);
        L_Monsters = 指定攻击一列敌人(敌人_);
        return L_Monsters;
    }

    public List<GameObject> 获取所有敌人(string 敌人)
    {
        List<GameObject> L_Monsters=new List<GameObject>();
        G_Util gut = NameMgr.画布.GetComponent<G_Util>();
        Dictionary<string, GameObject> 敌人集合;
        if (敌人.Equals("怪物"))
            敌人集合 = gut.现有怪物集合;
        else
            敌人集合 = gut.现有角色集合;
        foreach (string name in 敌人集合.Keys) {
            L_Monsters.Add(敌人集合[name]);
        }
        return L_Monsters;
    }

}
