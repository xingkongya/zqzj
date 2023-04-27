using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Prop_bascis : AllBasic
{
    public string id;//唯一id
    public string xing;//星级
    public string qua;//品质
    public string type;//道具类型.(1:材料. 2:道具. 3: 装备)
    public string isbang;//是否绑定
    public string icon;//图标
    public string name;//道具名字
    public string comment;//介绍描述
    public string price;//卖出价格
    public string fun;//效果
    public string cd;//冷却
    public string islock;//锁
    public string num;//数量



    //构造函数

    public Prop_bascis() { 
    
    
    }

    public Prop_bascis(string id,string xing, string qua, string type, string isbang,string icon, string name, string comment, string price,string fun, string cd,string islock,string num)
    {
        this.id = id;
        this.xing = xing;
        this.qua = qua;
        this.type = type;
        this.isbang = isbang;
        this.icon = icon;
        this.name = name;
        this.comment = comment;
        this.price = price;
        this.fun = fun;
        this.cd = cd;
        this.islock = islock;
        this.num = num;
    }


}
