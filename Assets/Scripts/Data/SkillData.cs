using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillData : AllBasic
{
    public string id;
    public string name;
    public string qua;
    public string xing;
    public string place;
    public string type;
    public string get;
    public string cd;
    public string lessgrade;
    public string comment;
    public string username;
    public string atknum;//攻击目标数量
    public string icon;//图标
    public string effect;//特效

    //空构造
    public SkillData() {

    }


    public SkillData(string id,string name,string qua,string xing,string place,string type,string get,string cd,string comment,string username,string atknum,string icon,string effect) {
        this.id = id;
        this.name = name;
        this.qua = qua;
        this.xing = xing;
        this.place = place;
        this.type = type;
        this.get = get;
        this.cd = cd;   
        this.comment = comment;
        this.username = username;
        this.icon = icon;
        this.effect = effect;
    }
}
