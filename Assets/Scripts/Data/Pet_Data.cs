using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet_Data : AllBasic
{
    private basicMgr bm = basicMgr.GetInstance();
    public string id;
    public string name;
    public string xing;
    public string qua;
    public string comment;
    public string grade;
    public string cc;
    public string max_cc;
    public string icon;
    public string state;
    public string next;
    public string ini_atk;
    public string ini_def;
    public string ini_hp;
    public string ini_hpr;
    public string ini_aspd;
    public string ini_cri;
    public string ram_atk;
    public string ram_def;
    public string ram_hp;
    public string ram_hpr;
    public string qua_atk;
    public string qua_def;
    public string qua_hp;
    public string qua_hpr;
    public string qua_cri;
    public string harm;
    public string harm_p;
    public string vam;
    public string vam_p;
    public string islock;
    public string skilllist_str;
    public List<string> skilllist = new List<string>();
    public List<string> 锁 = new List<string>();

    public Pet_Data() {

    }

    public Pet_Data(string id, string name, string xing, string qua, string comment, string grade, string cc, string max_cc, string icon, string state, string ini_atk, string ini_def, string ini_hp, string ini_hpr, string ini_aspd, string ini_cri, string ram_atk, string ram_def, string ram_hp, string ram_hpr, string qua_atk, string qua_def, string qua_hp, string qua_hpr, string qua_cri, string harm, string harm_p, string vam, string vam_p, string islock, string skilllist_str)
    {
        this.id = id;
        this.name = name;
        this.xing = xing;
        this.qua = qua;
        this.comment = comment;
        this.grade = grade;
        this.cc = cc;
        this.max_cc = max_cc;
        this.state = state;
        this.icon = icon;
        this.ini_atk = ini_atk;
        this.ini_def = ini_def;
        this.ini_hp = ini_hp;
        this.ini_hpr = ini_hpr;
        this.ini_aspd = ini_aspd;
        this.ini_cri = ini_cri;
        this.ram_atk = ram_atk;
        this.ram_def = ram_def;
        this.ram_hp = ram_hp;
        this.ram_hpr = ram_hpr;
        this.qua_atk = qua_atk;
        this.qua_def = qua_def;
        this.qua_hp = qua_hp;
        this.qua_hpr = qua_hpr;
        this.qua_cri = qua_cri;
        this.harm = harm;
        this.harm_p = harm_p;
        this.vam = vam;
        this.vam_p = vam_p;
        this.islock = islock;
        this.skilllist_str = skilllist_str;
    }




}
