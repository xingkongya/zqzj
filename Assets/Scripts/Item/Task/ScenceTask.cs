using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScenceTask : BaseManager<ScenceTask>
{
    private G_Util gut;
    private PropMgr pm = PropMgr.GetInstance();


    public Dictionary<string, UnityAction> 加载任务事件表() {
        Dictionary<string, UnityAction> 任务事件表 = new Dictionary<string, UnityAction>();
        任务事件表.Add("收集柴火", 收集柴火);
        任务事件表.Add("神奇松果", 神奇松果);
        任务事件表.Add("学习医术", 学习医术);
        任务事件表.Add("任务一:清除食人花", 清除食人花);
        任务事件表.Add("任务二:清剿蟊贼", 清剿蟊贼);
        任务事件表.Add("任务三:清剿大蛇", 清剿大蛇);
        return 任务事件表;
    }

    public void 隐藏任务框() {
        GameObject.Find("场景任务框(Clone)").gameObject.SetActive(false);
    }

    public void 收集柴火() {
        gut = NameMgr.画布.GetComponent<G_Util>();
        int num = PropMgr.GetInstance().返回背包该物品的数量("木材");
        role_Data myData = io.GetInstance().load();
        int 已收集 = 0;
        if (myData.记录.ContainsKey("收集柴火"))
        {
            已收集 = int.Parse(myData.记录["收集柴火"]);
        }
         
        if (num < (50 - 已收集))
        {
            if (pm.失去物品("木材", num).Equals("成功"))
            {
                gut.生成获得框("铜币", num * 100);
                gut.加金钱(new Dictionary<string, int>() { { "铜币", num * 100 } });
                隐藏任务框();
                gut.存档记录值相加("收集柴火", num);
            }
        }
        else
        {
            if (pm.失去物品("木材", 50 - 已收集).Equals("成功"))
            {
                gut.生成获得框("铜币", (50 - 已收集) * 100);
                gut.加金钱(new Dictionary<string, int>() { { "铜币", (50 - 已收集) * 100 } });
                隐藏任务框();
                Dictionary<Prop_bascis, int> 获取物品 = new Dictionary<Prop_bascis, int>();
                获取物品.Add(pm.检索物品("斩铁剑"), 1);
                获取物品.Add(pm.检索物品("百炼甲"), 1);
                pm.获取多个物品并显示特效(获取物品, "掉落");
                gut.存档记录值相加("收集柴火", 50 - 已收集);
            }
        }

    }

    public void 神奇松果() {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("大松果", 1).Equals("成功"))
        {
            gut.生成获得框("灵芝",1);
            pm.获取物品("灵芝",1);
            隐藏任务框();
            GameObject.Find("柳医师").GetComponent<juqing_yunmeng>().柳医师剧情2();         
        }
    }

    public void 清除食人花()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("食人花的花冠", 10).Equals("成功"))
        {
            gut.生成获得框("百年灵芝", 1);
            pm.获取物品("百年灵芝", 1);
            gut.PlayerPrefs储存("任务一",1);
            隐藏任务框();
        }
    }

    public void 清剿蟊贼()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("钱袋子", 3).Equals("成功"))
        {
            gut.生成获得框("五帝铜钱", 1);
            pm.获取物品("五帝铜钱", 1);
            gut.PlayerPrefs储存("任务二", 1);
            隐藏任务框();
        }
    }

    public void 清剿大蛇()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("听海石", 1).Equals("成功"))
        {
            gut.生成获得框("拜师贴", 1);
            pm.获取物品("拜师贴", 1);
            gut.PlayerPrefs储存("任务三", 1);
            隐藏任务框();
            GameObject.Find("秦镇守").GetComponent<jq_linhai>().秦镇守海底剧情();
        }
    }

    public void 学习医术()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        if (pm.失去物品("人参精华", 1).Equals("成功"))
        {
            gut.生成获得框("(绝招)<包扎>",1);
            pm.获取物品("(绝招)<包扎>", 1);
            隐藏任务框();
            GameObject.Find("柳医师").GetComponent<juqing_yunmeng>().柳医师剧情4();
        }
    }
}
