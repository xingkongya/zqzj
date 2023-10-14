using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Prop_bascis
{
    private basicMgr bm = basicMgr.GetInstance();
    public string min_qua; //最低品质
    public string max_qua; //最高品质
    public string min_grade; //最小使用等级
    public string lessgrade; //使用等级
    public string max_grade; //最大使用等级
    /*public string atk;//攻击力
    public string def;//防御力
    public string hp;//生命值
    public string hpr;//回血值
    public string atk_p;//攻击力加成
    public string def_p;//防御力加成
    public string hp_p;//血量加成
    public string hpr_p;//回血值加成
    public string aspd;//攻击速度
    public string ms;//移动速度
    public string cri;//暴击率
    public string cri_d;//暴伤加成
    public string harm;//固定伤害
    public string harm_d;//固定减伤
    public string harm_p;//伤害加成
    public string harm_p_d;//伤害减免
    public string vam;//固定吸血
    public string vam_p;//吸血加成
    public string mon_p;//金钱加成
    public string exp_p;//经验加成*/
    public string head_attribute_num;//首属性值
    public string next_attribute;//次属性
    public string next_num;//次属性值
    public Dictionary<string,string> extar_attribute;//额外属性
    public string place;//装备位置
    public string tao;//套装信息
    public string lv=basicMgr.GetInstance().Xor("0");//等级


    public Equipment() {
       
    }

    /*public Equipment(string id, string qua, string type, string name, string comment, string price, string isbang, string lessgrade,  string xing,string place, string atk, string def, string hp, string hpr,string aspd, string cri,string harm,string harm_p,string vam,string vam_p,string tao,string islock,string num) 
        {
        this.id = id;
        this.type = type;
        this.name = name;
        this.comment = comment;
        this.price =price ;
        this.isbang = isbang;
        this.lessgrade =lessgrade ;
        this.qua = qua;
        this.xing =xing ;
        this.place = place;
        this.atk =atk ;
        this.def =def ;
        this.hp =hp ;
        this.hpr =hpr ;
        this.aspd =aspd ;
        this.cri =cri ;
        this.harm =harm ;
        this.harm_p =harm_p ;
        this.vam =vam ;
        this.vam_p =vam_p ;
        this.tao = tao;
        this.islock = islock;
        this.num = num;
    }*/


    public Equipment(string id, string qua, string type, string name, string comment, string price, string isbang, string lessgrade, string xing, string place,  string tao, string islock, string lv)
    {
        this.id = id;
        this.type = type;
        this.name = name;
        this.comment = comment;
        this.price = price;
        this.isbang = isbang;
        this.lessgrade = lessgrade;
        this.qua = qua;
        this.xing = xing;
        this.place = place;
        this.tao = tao;
        this.islock = islock;
        this.lv = lv;
    }
}
