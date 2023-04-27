using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatMgr : BaseManager<ChatMgr>
{
    private basicMgr bm = basicMgr.GetInstance();
    public List<string> 综合信息 = new List<string>();
    public List<string> 战斗信息 = new List<string>();
    public List<string> 系统信息 = new List<string>();
    public Chat chat ;


    public void 战斗播报(string name,int qua, Dictionary<Prop_bascis, int>掉落集合,Dictionary<string,string>经验钱币,string type) {
        string 战斗信息_="";
        chat = NameMgr.chat.GetComponent<Chat>();
        if (type.Equals("死亡"))
            战斗信息_ = "你已被" + "<color=" + bm.返回颜色代码(qua) + ">" + name + "</color>" + "击败!";
        else if (type.Equals("掉落"))
        {
            string 掉落信息 = "";
            foreach (Prop_bascis 物品 in 掉落集合.Keys)
            {
                掉落信息 += "<color=" + bm.返回颜色代码(bm.Xstoi(物品.qua)) + ">" +物品.name + "</color>"+"x"+掉落集合[物品]+".";
            }
            string 经验钱币信息 = "";
            foreach (string 名字 in 经验钱币.Keys)
            {
                if (bm.Xstoi(经验钱币[名字]) != 0) {
                    经验钱币信息 += bm.Xstoi(经验钱币[名字]) + 名字 + ".";
                }
            }

            战斗信息_ = "你打败了" + "<color=" + bm.返回颜色代码(qua) + ">" + name + "</color>" + ",获得了" + 经验钱币信息 + 掉落信息;
        }
        添加综合信息(战斗信息_,chat);
        添加战斗信息(战斗信息_,chat);
    }

    public void 系统播报(Prop_bascis pb,string name,int num,string type) {
        string 系统信息 = "";
        chat = NameMgr.chat.GetComponent<Chat>();
        if (type.Equals("获得道具"))
        {
            系统信息 = "获得道具:" + "<color=" + bm.返回颜色代码(bm.Xstoi(pb.qua)) + ">" + pb.name + "</color>";
        }
        else if (type.Equals("失去金钱")) {
            系统信息 = "失去" + num + name;
        }
        else if (type.Equals("获得金钱"))
        {
            系统信息 = "获得" + num + name;
        }
        else if (type.Equals("升级"))
        {
            系统信息 = "<color=" + bm.返回颜色代码(1) + ">" +"恭喜你升到" + num+"级!"+"</color>";
        }
        添加综合信息(系统信息,chat);
        添加系统信息(系统信息,chat);
    }

    public void 添加综合信息(string str,Chat chat)
    {
        添加信息(综合信息, str);
        chat.刷新聊天框();
    }
    public void 添加战斗信息(string str, Chat chat) {
        添加信息(战斗信息,str);
        chat.刷新聊天框();
    }
    public void 添加系统信息(string str, Chat chat)
    {
        添加信息(系统信息, str);
        chat.刷新聊天框();
    }

    public void 添加信息(List<string> 信息集,string str)
    {
        if (信息集.Count >= 30)
        {
            信息集.RemoveAt(0);
        }   
            
        信息集.Add(str);

    }
}
