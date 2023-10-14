using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeCheng : MonoBehaviour
{
    public string Key;
    private Prop_bascis 物品;
    private io IO;
    private G_Util gut;
    private basicMgr bm;
    private PropMgr pm;
    private Slider 拉条;


    private void Awake()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        IO = io.GetInstance();
        bm = basicMgr.GetInstance();
        pm = PropMgr.GetInstance();
        拉条 = gameObject.transform.Find("button/Slider").GetComponent<Slider>();
    }


    public void 刷新拉条最大值()
    {
        int MaxNum = pm.返回背包该物品的数量(pm.物品合成表[物品.name]["合成需求"]) / int.Parse(pm.物品合成表[物品.name]["需求数量"]);
        int MaxEls;
        if (pm.物品合成表[物品.name]["额外需求"].Equals("铜币"))
            MaxEls = int.Parse(pm.返回金钱数量(pm.物品合成表[物品.name]["额外需求"])) / int.Parse(pm.物品合成表[物品.name]["额外数量"]);
        else
            MaxEls = pm.返回背包该物品的数量(pm.物品合成表[物品.name]["额外需求"]) / int.Parse(pm.物品合成表[物品.name]["额外数量"]);

        if (MaxEls > MaxNum)
        {
            拉条.maxValue = MaxNum;
        }
        else {
            拉条.maxValue = MaxEls;
        }

        if (拉条.maxValue != 0) {
            //初始化value值为1
            拉条.value = 1;
        }
    }


    public void 刷新合成信息()
    {
        role_Data myData = IO.load();
        if (myData.材料背包.ContainsKey(Key) )
        {
            物品 = myData.材料背包[Key];

            Text 名字文本 = gameObject.transform.Find("文本/名字").GetComponent<Text>();
            Text 数量文本 = 名字文本.transform.Find("数量").GetComponent<Text>();
            名字文本.text = 物品.name;
            数量文本.text = "X" + bm.Xstoi(物品.num);
            名字文本.color = bm.转换颜色(bm.Xstoi(物品.qua));
            数量文本.color = bm.转换颜色(bm.Xstoi(物品.qua));
        }
        else
        {
            gut.生成警告框("物品错误!");
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        刷新合成信息();
        刷新拉条最大值();
        刷新合成需求信息();
    }



    public void 刷新合成需求信息()
    {
        if (拉条 != null && 拉条.gameObject.activeSelf) {
            Text 消耗文本 = gameObject.transform.Find("文本/消耗/内容").GetComponent<Text>();
            Text 额外消耗文本 = gameObject.transform.Find("文本/额外消耗/内容").GetComponent<Text>();
            Text 获得文本 = gameObject.transform.Find("文本/获得/内容").GetComponent<Text>();
            int num = (int)拉条.value;

            消耗文本.text = pm.物品合成表[物品.name]["合成需求"] + "(" + int.Parse(pm.物品合成表[物品.name]["需求数量"]) * num + "/" + pm.返回背包该物品的数量(pm.物品合成表[物品.name]["合成需求"]) + ")";
            if (pm.物品合成表[物品.name]["额外需求"].Equals("铜币"))
            {
                额外消耗文本.text = pm.物品合成表[物品.name]["额外需求"] + "(" + int.Parse(pm.物品合成表[物品.name]["额外数量"]) * num + "/" + pm.返回金钱数量(pm.物品合成表[物品.name]["额外需求"]) + ")";
            }
            else
            {
                额外消耗文本.text = pm.物品合成表[物品.name]["额外需求"] + "(" + int.Parse(pm.物品合成表[物品.name]["额外数量"]) * num + "/" + pm.返回背包该物品的数量(pm.物品合成表[物品.name]["额外需求"]) + ")";
            }
            获得文本.text = pm.物品合成表[物品.name]["合成获得"] + "X" + num;
        }
    }




    public void 合成按钮()
    {
        //如果逻辑层返回true,说明强化成功,则刷新视图层
        if (合成_Control())
        {
            gut.生成警告框("合成成功!");
            刷新合成信息();
            刷新拉条最大值();
            刷新合成需求信息();
            if (NameMgr.背包 != null)
                NameMgr.背包.GetComponent<Bag>().初始化背包();
        }
    }



    public void 加一() {
        if (拉条.value < 拉条.maxValue) {
            拉条.value += 1;
        }
    }

    public void 减一()
    {
        if (拉条.value > 0)
        {
            拉条.value -= 1;
        }
    }


    public void 拉满() {
        if (拉条.value != 拉条.maxValue)
        {
            拉条.value = 拉条.maxValue;
        }
    }



    private bool 合成_Control()
    {
        int num = (int)拉条.value;
        string 返回值 = pm.失去物品(pm.物品合成表[物品.name]["合成需求"], int.Parse(pm.物品合成表[物品.name]["需求数量"]) * num);
        if (返回值.Equals("成功"))
        {
            string 返回值_额外;
            if (pm.物品合成表[物品.name]["额外需求"].Equals("铜币"))
            {
                 返回值_额外 = pm.失去金钱(pm.物品合成表[物品.name]["额外需求"],int.Parse(pm.物品合成表[物品.name]["额外数量"])*num);
            }
            else
            {
                 返回值_额外 = pm.失去物品(pm.物品合成表[物品.name]["额外需求"], int.Parse(pm.物品合成表[物品.name]["额外数量"]) * num);
            }

            if (!返回值_额外.Equals("成功"))
            {
                pm.获取物品(pm.物品合成表[物品.name]["合成需求"], int.Parse(pm.物品合成表[物品.name]["需求数量"]) * num);
                gut.生成警告框("合成失败!");
                return false;
            }

            pm.获取物品(pm.物品合成表[物品.name]["合成获得"], num);
            return true;
        }
        else {
            gut.生成警告框("合成失败!");
            return false;
        }
    }

   






   
   


    public void 删除自己()
    {
        Destroy(gameObject);
    }
}
