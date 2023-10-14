using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EQU_Intensify : MonoBehaviour
{
    public string Key;
    private Equipment 装备;
    private io IO;
    private G_Util gut;
    private basicMgr bm;
    private PropMgr pm;
    public Dictionary<string, string> 强化成功率 = new Dictionary<string, string>() { { "0", "100" }, { "1", "95" }, { "2", "90" }, { "3", "80" }, { "4", "70" }, { "5", "60" }, { "6", "45" }, { "7", "30" }, { "8", "25" }, { "9", "20" }, { "10", "15" }, { "11", "12" }, { "12", "9" }, { "13", "5" }, { "14", "1" } };
    private  Dictionary<string, string> 需求材料 = new Dictionary<string, string>() { { "0", "灰色晶矿" }, { "1", "绿色晶矿" }, { "2", "蓝色晶矿" }, { "3", "紫色晶矿" }, { "4", "橙色晶矿" }, { "5", "红色晶矿" } };

    private void Awake()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        IO = io.GetInstance();
        bm = basicMgr.GetInstance();
        pm = PropMgr.GetInstance();
    }


    public void 刷新装备信息() {
        role_Data myData = IO.load();
        if (myData.装备背包.ContainsKey(Key) || myData.装备槽.ContainsKey(Key))
        {
            if (myData.装备背包.ContainsKey(Key))
            {
                装备 = myData.装备背包[Key];
            }
            else
            {
                装备 = myData.装备槽[Key];
            }


        }
        else
        {
            gut.生成警告框("装备错误!");
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        刷新装备信息();
        刷新装备强化信息();
        刷新文本信息();
    }



    public void 刷新装备强化信息() {
        Text 名字文本 = gameObject.transform.Find("文本/名字").GetComponent<Text>();
        Text 强化文本 = 名字文本.transform.Find("强化等级").GetComponent<Text>();
        名字文本.text = 装备.name;
        强化文本.text = "+" + bm.Xstoi(装备.lv);
        名字文本.color = bm.转换颜色(bm.Xstoi(装备.qua));
        强化文本.color = bm.转换颜色(bm.Xstoi(装备.qua));
    }


    public void 刷新文本信息()
    {
        Text 材料名字文本 = gameObject.transform.Find("文本/消耗/名字").GetComponent<Text>();
        Text 材料数量文本 = gameObject.transform.Find("文本/消耗/数量").GetComponent<Text>();
        Text 成功率文本 = gameObject.transform.Find("文本/成功率/概率").GetComponent<Text>();
        Text 失败惩罚文本 = gameObject.transform.Find("文本/失败惩罚/描述").GetComponent<Text>();
        Text 当前属性文本 = gameObject.transform.Find("文本/当前属性/文本/数值").GetComponent<Text>();
        Text 强化属性文本 = gameObject.transform.Find("文本/强化属性/文本/数值").GetComponent<Text>();
        材料名字文本.text = 需求材料[bm.Xstoi(装备.qua) + ""] + "";
        材料数量文本.text = "<color=red>1</color>/"+pm.返回背包该物品的数量(需求材料[bm.Xstoi(装备.qua) + ""]) + "";
        当前属性文本.text = PropMgr.GetInstance().主属性表[装备.place] + "+"+pm.返回装备主属性(装备);
        强化属性文本.text = PropMgr.GetInstance().主属性表[装备.place] + "+" + pm.返回装备强化主属性(装备);
        if (bm.Xstoi(装备.lv) < int.Parse(PropMgr.GetInstance().颜色等级上限[bm.Xstoi(装备.qua) + ""])) {
            成功率文本.text = 强化成功率[bm.Xstoi(装备.lv) + ""]+"%";
        }
        else {
            成功率文本.text = "强化达上限";
        }
        if (bm.Xstoi(装备.lv) < 5)
        {
            失败惩罚文本.text = "无";
        }
        else if (bm.Xstoi(装备.lv) < 8)
        {
            失败惩罚文本.text = "小概率掉一级";

        }
        else if (bm.Xstoi(装备.lv) < 12)
        {
            失败惩罚文本.text = "概率掉一级";

        }
        else if (bm.Xstoi(装备.lv) < 15)
        {
            失败惩罚文本.text = "掉一级";

        }
    }



    public void 强化按钮() {
        //如果逻辑层返回true,说明强化成功,则刷新视图层
        if (强化_Control()) {
            gut.生成警告框("强化成功!");
            刷新装备信息();
            刷新装备强化信息();
            刷新文本信息();
            if (NameMgr.背包!=null)
                NameMgr.背包.GetComponent<Bag>().初始化背包();
        }
    }

    public void 一键加1按钮()
    {
        //如果逻辑层返回true,说明强化成功,则刷新视图层
        if (一键_Control())
        {
            gut.生成警告框("强化成功!");
        }
        刷新装备信息();
        刷新装备强化信息();
        刷新文本信息();
        if (NameMgr.背包 != null)
            NameMgr.背包.GetComponent<Bag>().初始化背包();

}




    private bool 强化_Control() {
        string 材料名 = 需求材料[bm.Xstoi(装备.qua) + ""];
        if (pm.返回背包该物品的数量(材料名) > 1)
        {
            if (bm.Xstoi(装备.lv)<int.Parse( PropMgr.GetInstance().颜色等级上限[bm.Xstoi(装备.qua)+""])) {
                string 返回结果 = pm.失去物品(材料名, 1);
                if (返回结果.Equals("成功"))
                {
                    int 成功率 = int.Parse(强化成功率[bm.Xstoi(装备.lv) + ""]);
                    if (gut.概率(成功率, 100))
                    {
                        强化_Dao();
                        return true;
                    }
                    else
                    {
                        gut.生成警告框("强化失败!");
                        return false;
                    }
                }
                else
                {
                    gut.生成警告框("材料不足");
                    return false;
                }
            }
            else {
                gut.生成警告框("强化达上限");
                return false;
            }
        }
        else
        {
            gut.生成警告框("材料不足");
            return false;
        }
    }

    private bool 一键_Control()
    {
        int 当前等级 = bm.Xstoi(装备.lv);
        string 材料名 = 需求材料[bm.Xstoi(装备.qua) + ""];
        int num = pm.返回背包该物品的数量(材料名) > 20 ? 20 : pm.返回背包该物品的数量(材料名);
        for (int i=0;i< num;i++)
        {
           
                if (bm.Xstoi(装备.lv) < int.Parse(PropMgr.GetInstance().颜色等级上限[bm.Xstoi(装备.qua) + ""]))
                {
                    string 返回结果 = pm.失去物品(材料名, 1);
                    if (返回结果.Equals("成功"))
                    {
                        int 成功率 = int.Parse(强化成功率[bm.Xstoi(装备.lv) + ""]);
                        if (gut.概率(成功率, 100))
                        {
                            强化_Dao();
                        if (bm.Xstoi(装备.lv)>当前等级) {
                            return true;
                        }
                        }
                        else
                        {
                        强化失败判定_Control();
                            continue;
                        }
                    }
                    else
                    {
                    gut.生成警告框("材料不足");
                    return false;
                    }
                }
                else
                {
                gut.生成警告框("强化达上限");
                return false;
            }

        }
        gut.生成警告框("强化失败!");
        return false;
    }





        private void 强化_Dao() {
        role_Data myData = IO.load();
        装备.lv = bm.Xitos(bm.Xstoi(装备.lv) + 1);
        装备= pm.装备初始化主属性值(装备);
        装备.islock = "1";
        if (myData.装备背包.ContainsKey(Key))
        {    
            myData.装备背包[Key]=装备;
        }
        else
        {
            myData.装备槽[Key] = 装备;
        }
        IO.save(myData);
    }

    private void 强化失败判定_Control()
    {
        if (bm.Xstoi(装备.lv) >= 5 && bm.Xstoi(装备.lv) < 8)
        {
            if (gut.概率(30, 100))
            {
                强化失败_Dao();
            }
        }
        else if (bm.Xstoi(装备.lv) < 12)
        {
            if (gut.概率(70, 100))
            {
                强化失败_Dao();
            }
        }
        else if (bm.Xstoi(装备.lv) < 15) {
            强化失败_Dao();
        }


    }

    private void 强化失败_Dao() {
        role_Data myData = IO.load();
        装备.lv = bm.Xitos(bm.Xstoi(装备.lv) - 1);
        装备 = pm.装备初始化主属性值(装备);
        if (myData.装备背包.ContainsKey(Key))
        {
            myData.装备背包[Key] = 装备;
        }
        else
        {
            myData.装备槽[Key] = 装备;
        }
        IO.save(myData);
    }



    public void 删除自己() {
        Destroy(gameObject);
    }

}
