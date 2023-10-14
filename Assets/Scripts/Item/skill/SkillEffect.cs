using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect : BaseManager<SkillEffect>
{
    private basicMgr bm = basicMgr.GetInstance();
    private io io_ = io.GetInstance();
    private MonsterMgr mmgr = MonsterMgr.GetInstance();
    private G_Util gut = NameMgr.画布.GetComponent<G_Util>();
    private GameObject 父物体=new GameObject();
    public GameObject user;


    public Dictionary<string, Action> 加载技能效果()
    {
        Dictionary<string, Action> 技能名字和其效果 = new Dictionary<string, Action>();
        技能名字和其效果.Add("", 空方法);
        技能名字和其效果.Add("初级锻体术", 初级锻体术);
        技能名字和其效果.Add("初级御力诀", 初级御力诀);
        技能名字和其效果.Add("初级轻身术", 初级轻身术);
        技能名字和其效果.Add("中级锻体术", 中级锻体术);
        技能名字和其效果.Add("中级御力诀", 中级御力诀);
        技能名字和其效果.Add("中级轻身术", 中级轻身术);
        技能名字和其效果.Add("高级锻体术", 高级锻体术);
        技能名字和其效果.Add("高级御力诀", 高级御力诀);
        技能名字和其效果.Add("高级轻身术", 高级轻身术);
        技能名字和其效果.Add("桃之夭夭(初级)", 初级桃之夭夭);
        技能名字和其效果.Add("养气诀", 养气诀);
        技能名字和其效果.Add("清心诀", 清心诀);
        技能名字和其效果.Add("归元诀", 归元诀);
        技能名字和其效果.Add("九阴真经", 九阴真经);
        技能名字和其效果.Add("九阳真经", 九阳真经); 
        技能名字和其效果.Add("一气先天诀", 一气先天诀);
        技能名字和其效果.Add("≮焚诀≯", 焚诀);
        技能名字和其效果.Add("包扎", 包扎);
        技能名字和其效果.Add("百步穿杨", 百步穿杨);
        技能名字和其效果.Add("金色闪光", 金色闪光);
        技能名字和其效果.Add("翻江倒海", 翻江倒海);
        技能名字和其效果.Add("排山倒海", 排山倒海);
        技能名字和其效果.Add("荒天拳", 荒天拳);
        技能名字和其效果.Add("陨仙剑", 陨仙剑);
        技能名字和其效果.Add("水甲术", 水甲术);
        技能名字和其效果.Add("嗜血", 嗜血);
        技能名字和其效果.Add("攻击姿态", 攻击姿态);
        技能名字和其效果.Add("防御姿态", 防御姿态);
        技能名字和其效果.Add("急速飞行", 急速飞行);
        技能名字和其效果.Add("獠牙", 獠牙);
        技能名字和其效果.Add("撞击", 撞击);
        技能名字和其效果.Add("吞噬之触", 吞噬之触);
        技能名字和其效果.Add("拍打", 拍打);
        技能名字和其效果.Add("铁砂掌", 铁砂掌);
        技能名字和其效果.Add("鹰爪功", 鹰爪功);
        技能名字和其效果.Add("热血沸腾", 热血沸腾);
        技能名字和其效果.Add("狂战", 狂战);
        技能名字和其效果.Add("强击", 强击);
        技能名字和其效果.Add("鸡你太美", 鸡你太美);
        技能名字和其效果.Add("律师函警告", 律师函警告);
        技能名字和其效果.Add("看我打篮球", 看我打篮球);
        技能名字和其效果.Add("磐石", 磐石);
        技能名字和其效果.Add("磐石身", 磐石身);
        技能名字和其效果.Add("治愈之水", 治愈之水);
        技能名字和其效果.Add("≮化龙≯", 化龙);


        return 技能名字和其效果;

    }

    public void 空方法() { 
    }


    public void 初级锻体术() {
        DataMgr dm = DataMgr.GetInstance();
        role_Data myData = io_.load();
        dm.技能属性["血量"] =bm.Xitos( bm.Xstoi(dm.技能属性["血量"]) + bm.Xstoi(myData.等级) * 15);
        dm.技能属性["回血值"] = bm.Xitos((int)(bm.Xstoi(dm.技能属性["回血值"]) + bm.Xstoi(myData.等级) * 1.8f));
        user = null;
    }

    public void 初级御力诀()
    {
        DataMgr dm = DataMgr.GetInstance();
        role_Data myData = io_.load();
        dm.技能属性["攻击力"] = bm.Xitos((int)(bm.Xstoi(dm.技能属性["攻击力"]) + bm.Xstoi(myData.等级) * 0.8f));
        dm.技能属性["防御力"] = bm.Xitos((int)(bm.Xstoi(dm.技能属性["防御力"]) + bm.Xstoi(myData.等级) * 0.4f));
        user = null;
    }

    public void 初级轻身术()
    {
        DataMgr dm = DataMgr.GetInstance();
        dm.技能属性["攻击速度"] = bm.Xitos(bm.Xstoi(dm.技能属性["攻击速度"]) + 10);
        user = null;
    }

    public void 初级桃之夭夭() {
        DataMgr dm = DataMgr.GetInstance();
        dm.技能属性["攻击速度"] = bm.Xitos(bm.Xstoi(dm.技能属性["攻击速度"]) + 25);
        user = null;

    }

    public void 中级锻体术()
    {
        DataMgr dm = DataMgr.GetInstance();
        role_Data myData = io_.load();
        dm.技能属性["血量"] = bm.Xitos(bm.Xstoi(dm.技能属性["血量"]) + bm.Xstoi(myData.等级) * 18);
        dm.技能属性["回血值"] = bm.Xitos((int)(bm.Xstoi(dm.技能属性["回血值"]) + bm.Xstoi(myData.等级) * 2.0f));
        user = null;
    }

    public void 中级御力诀()
    {
        DataMgr dm = DataMgr.GetInstance();
        role_Data myData = io_.load();
        dm.技能属性["攻击力"] = bm.Xitos((int)(bm.Xstoi(dm.技能属性["攻击力"]) + bm.Xstoi(myData.等级) * 1.2f));
        dm.技能属性["防御力"] = bm.Xitos((int)(bm.Xstoi(dm.技能属性["防御力"]) + bm.Xstoi(myData.等级) * 0.6f));
        user = null;
    }

    public void 中级轻身术()
    {
        DataMgr dm = DataMgr.GetInstance();
        dm.技能属性["攻击速度"] = bm.Xitos(bm.Xstoi(dm.技能属性["攻击速度"]) + 15);
        user = null;
    }

    public void 高级锻体术()
    {
        DataMgr dm = DataMgr.GetInstance();
        role_Data myData = io_.load();
        dm.技能属性["血量"] = bm.Xitos(bm.Xstoi(dm.技能属性["血量"]) + bm.Xstoi(myData.等级) * 20);
        dm.技能属性["回血值"] = bm.Xitos((int)(bm.Xstoi(dm.技能属性["回血值"]) + bm.Xstoi(myData.等级) * 2.4f));
        user = null;
    }

    public void 高级御力诀()
    {
        DataMgr dm = DataMgr.GetInstance();
        role_Data myData = io_.load();
        dm.技能属性["攻击力"] = bm.Xitos((int)(bm.Xstoi(dm.技能属性["攻击力"]) + bm.Xstoi(myData.等级) * 1.5f));
        dm.技能属性["防御力"] = bm.Xitos((int)(bm.Xstoi(dm.技能属性["防御力"]) + bm.Xstoi(myData.等级) * 0.8f));
        user = null;
    }

    public void 高级轻身术()
    {
        DataMgr dm = DataMgr.GetInstance();
        dm.技能属性["攻击速度"] = bm.Xitos(bm.Xstoi(dm.技能属性["攻击速度"]) + 20);
        user = null;
    }

    public void 磐石身()
    {
        DataMgr dm = DataMgr.GetInstance();
        dm.技能属性["防御力"] = bm.Xitos(bm.Xstoi(dm.技能属性["防御力"]) + 100);
        user = null;
    }

    public void 养气诀()
    {
        role_Data myData = io_.load();
        myData.限制等级 = bm.Xor("150");
        io_.save(myData);
        user = null;
    }

    public void 清心诀()
    {
        role_Data myData = io_.load();
        myData.限制等级 = bm.Xor("200");
        io_.save(myData);
        user = null;
    }

    public void 归元诀()
    {
        role_Data myData = io_.load();
        myData.限制等级 = bm.Xor("300");
        io_.save(myData);
        user = null;
    }

    public void 九阴真经()
    {
        role_Data myData = io_.load();
        myData.限制等级 = bm.Xor("200");
        io_.save(myData);
        user = null;
    }

    public void 九阳真经()
    {
        role_Data myData = io_.load();
        myData.限制等级 = bm.Xor("200");
        io_.save(myData);
        user = null;
    }

    public void 一气先天诀()
    {
        role_Data myData = io_.load();
        myData.限制等级 = bm.Xor("300");
        io_.save(myData);
        user = null;
    }

    public void 焚诀()
    {
        role_Data myData = io_.load();
        myData.限制等级 = bm.Xor("500");
        io_.save(myData);
        user = null;
    }

    //主动
    public void 包扎()
    {
        G_Util gut = NameMgr.画布.GetComponent<G_Util>();
        if (user != null)
        {
            combat cb = user.GetComponent<combat>();
            int 伤害 = (int)(50 + bm.Xstoi(cb.战斗攻击力) * 1f);
            gut.恢复血量(cb, 伤害);
        }
       
        user = null;
    }

    //主动
    public void 百步穿杨()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        List<GameObject> 攻击对象 ;
        if (user != null)
        {
            string 敌人;
            if (user.CompareTag("人物") || user.CompareTag("宠物"))
                敌人 = "怪物";
            else
                敌人 = "角色";
            combat cb = user.GetComponent<combat>();
            if (cb.目标名字 != null && cb.目标名字.Equals(""))
                攻击对象 = mmgr.随意攻击一列二个敌人(敌人);
            else
                攻击对象 = mmgr.指定攻击一列二个敌人(GameObject.Find(cb.目标名字));

            for (int i = 0; i < 攻击对象.Count; i++)
            {
                int 伤害 = (int)(bm.Xstoi(cb.战斗攻击力) * 1.5f + 120);
                if (攻击对象[i].activeSelf)
                {
                    combat mcb = 攻击对象[i].GetComponent<combat>();
                    if (mcb != null&&(mcb.gameObject.CompareTag("怪物")|| mcb.gameObject.CompareTag("boss")))
                        mcb.开启战斗();
                    gut.启动延迟伤害(cb, 伤害, 攻击对象[i], 0.1f);

                }

                if (i == 0)
                    父物体 = 攻击对象[i];
                else
                {
                    if (父物体.transform.position.y > 攻击对象[i].transform.position.y)
                        父物体 = 攻击对象[i];
                }

            }
            if (攻击对象.Count > 0) {
                //gut.生成技能流光_攻击竖排敌人(父物体, bm.转换颜色(3), 2);
                gut.生成技能特效_三剑(父物体);
            }
        }

        user = null;
    }


   


    public void 可暴伤加成(combat cb,int 伤害,GameObject 目标对象 ) {

        //暴击
        G_Util gut = NameMgr.画布.GetComponent<G_Util>();
        bool 暴击 = gut.概率((int)bm.Xstof(cb.暴击率), 100);
        string 当前暴伤加成;
        string 类型;
        if (!暴击)//无暴击   
        {
            当前暴伤加成 = bm.Xor(" 1.0");
            类型 = "技能";
        }
        else {
            当前暴伤加成 = cb.暴伤加成;
            类型 = "暴击";
        }
        伤害 = (int)(伤害 * bm.Xstof(当前暴伤加成));
        if (目标对象 != null) {
            combat mcb = 目标对象.GetComponent<combat>();
            if (mcb != null)
            {
                float 攻防比 = bm.Xstof(mcb.战斗防御力) / bm.Xstof(cb.战斗攻击力);
                伤害 = (int)(伤害 * (攻防比 < 0.8f ? (1 - 攻防比) : 0.2f));//技能最低打出攻防比*0.2的伤害

                伤害 = 伤害偏移(伤害);
                if (cb != null)
                {
                    cb.扣血行为(目标对象, 类型, 伤害);
                }
            }
        }
    }

    public int 伤害偏移(int 伤害) {
        
        int 随机比率 =  UnityEngine.Random.Range(900, 1101);
        伤害 = (int)(伤害 * (随机比率 / 1000f));
        return 伤害;
    }



    public void 金色闪光()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        List<GameObject> 攻击对象;
        if (user != null)
        {
            string 敌人;
            if (user.CompareTag("人物") || user.CompareTag("宠物"))
                敌人 = "怪物";
            else
                敌人 = "角色";
            combat cb = user.GetComponent<combat>();
            if (cb.目标名字 != null && cb.目标名字.Equals(""))
                攻击对象 = mmgr.随意攻击一列二个敌人(敌人);
            else
                攻击对象 = mmgr.指定攻击一列二个敌人(GameObject.Find(cb.目标名字));

            int 总伤害 = 0; ;
            for (int i = 0; i < 攻击对象.Count; i++)
            {
                int 伤害 = (int)(bm.Xstoi(cb.战斗攻击力) * 1.6f + 300);
                总伤害 += 伤害;
                if (攻击对象[i].activeSelf)
                {
                    combat mcb = 攻击对象[i].GetComponent<combat>();
                    if (mcb != null && (mcb.gameObject.CompareTag("怪物") || mcb.gameObject.CompareTag("boss")))
                        mcb.开启战斗();
                    gut.启动延迟伤害(cb, 伤害, 攻击对象[i], 0.1f);

                }

                if (i == 0)
                    父物体 = 攻击对象[i];
                else
                {
                    if (父物体.transform.position.y > 攻击对象[i].transform.position.y)
                        父物体 = 攻击对象[i];
                }

            }
            gut.恢复血量(cb, (int)(总伤害 * 0.3f));
            if (攻击对象.Count > 0)
                gut.生成技能流光_攻击竖排敌人(父物体, bm.转换颜色(4), 2);
        }

        user = null;
    }


    public void 翻江倒海()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        List<GameObject> 攻击对象;
        if (user != null)
        {
            string 敌人;
            if (user.CompareTag("人物") || user.CompareTag("宠物"))
                敌人 = "怪物";
            else
                敌人 = "角色";
            combat cb = user.GetComponent<combat>();
            if (cb.目标名字 != null && cb.目标名字.Equals(""))
                攻击对象 = mmgr.随意攻击一列敌人(敌人);
            else
                攻击对象 = mmgr.指定攻击一列敌人(GameObject.Find(cb.目标名字));

            for (int i = 0; i < 攻击对象.Count; i++)
            {
                int 伤害 = (int)(bm.Xstoi(cb.战斗攻击力) * 1.6f + 200);
                if (攻击对象[i].activeSelf)
                {
                    combat mcb = 攻击对象[i].GetComponent<combat>();
                    if (mcb != null&&(攻击对象[i].CompareTag("怪物")|| 攻击对象[i].CompareTag("boss")))
                        mcb.开启战斗();
                    gut.启动延迟伤害(cb, 伤害, 攻击对象[i], 0.1f);

                }

                if (i == 0)
                    父物体 = 攻击对象[i];
                else
                {
                    if (父物体.transform.position.y > 攻击对象[i].transform.position.y)
                        父物体 = 攻击对象[i];
                }

            }

            if(攻击对象.Count>0)
                gut.生成技能特效_水爆(父物体);
        }

        user = null;
    }

    public void 排山倒海()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        List<GameObject> 攻击对象;
        if (user != null)
        {
            string 敌人;
            if (user.CompareTag("人物") || user.CompareTag("宠物"))
                敌人 = "怪物";
            else
                敌人 = "角色";
            combat cb = user.GetComponent<combat>();
            攻击对象 = mmgr.获取所有敌人(敌人);

            for (int i = 0; i < 攻击对象.Count; i++)
            {
                int 伤害 = (int)(bm.Xstoi(cb.战斗攻击力) * 2f + 500);
                if (攻击对象[i].activeSelf)
                {
                    combat mcb = 攻击对象[i].GetComponent<combat>();
                    if (mcb != null && (攻击对象[i].CompareTag("怪物") || 攻击对象[i].CompareTag("boss")))
                        mcb.开启战斗();
                    gut.启动延迟伤害(cb, 伤害, 攻击对象[i], 0.1f);

                }

                if (i == 0)
                    父物体 = 攻击对象[i];
                else
                {
                    if (父物体.transform.position.y > 攻击对象[i].transform.position.y)
                        父物体 = 攻击对象[i];
                }

            }

            if (攻击对象.Count > 0)
                gut.生成技能特效_水爆(父物体);
        }

        user = null;
    }


    public void 鸡你太美()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        List<GameObject> 攻击对象;
        if (user != null)
        {
            string 敌人;
            if (user.CompareTag("人物") || user.CompareTag("宠物"))
                敌人 = "怪物";
            else
                敌人 = "角色";
            combat cb = user.GetComponent<combat>();
            攻击对象 = mmgr.获取所有敌人(敌人);

            for (int i = 0; i < 攻击对象.Count; i++)
            {
                int 伤害 = (int)(bm.Xstoi(cb.战斗攻击力) * 2.0f + 500);
                if (攻击对象[i].activeSelf)
                {
                    combat mcb = 攻击对象[i].GetComponent<combat>();
                    if (mcb != null && (攻击对象[i].CompareTag("怪物") || 攻击对象[i].CompareTag("boss")))
                        mcb.开启战斗();
                    gut.启动延迟伤害(cb, 伤害, 攻击对象[i], 0.1f);

                }

                if (i == 0)
                    父物体 = 攻击对象[i];
                else
                {
                    if (父物体.transform.position.y > 攻击对象[i].transform.position.y)
                        父物体 = 攻击对象[i];
                }

            }

            if (攻击对象.Count > 0)
                gut.生成技能特效_水爆(父物体);
        }

        user = null;
    }

    public void 水甲术()
    {
        G_Util gut = NameMgr.画布.GetComponent<G_Util>();
        if (user != null)
        {
            combat cb = user.GetComponent<combat>();
            gut.提升属性(cb,"防御力",10,10);
            gut.持续恢复血量(cb,bm.Xstoi(cb.等级)*3,5,1);
            gut.生成技能特效_防御(user, 10);
        }

        user = null;
    }


    public void 治愈之水()
    {
        G_Util gut = NameMgr.画布.GetComponent<G_Util>();
        if (user != null)
        {
            combat cb = user.GetComponent<combat>();
            int 伤害 = 100 + bm.Xstoi(cb.等级) * 30;
            gut.恢复血量(cb, 伤害);
            gut.持续恢复血量(cb, bm.Xstoi(cb.等级) * 5, 3, 1);
        }

        user = null;
    }

    public void 热血沸腾()
    {
        G_Util gut = NameMgr.画布.GetComponent<G_Util>();
        if (user != null)
        {
            combat cb = user.GetComponent<combat>();
            gut.提升属性(cb, "攻击速度", 15, 10);
            gut.持续恢复血量(cb, bm.Xstoi(cb.等级) * 3, 5, 1);
            gut.生成技能特效_攻击(user);
        }

        user = null;
    }

    public void 嗜血()
    {
        G_Util gut = NameMgr.画布.GetComponent<G_Util>();
        if (user != null)
        {
            combat cb = user.GetComponent<combat>();
            gut.提升属性(cb, "吸血加成", 8, 10);
            gut.提升属性(cb, "攻击速度", 15, 10);
            gut.生成技能特效_攻击(user);
        }

        user = null;
    }

    public void 急速飞行()
    {
        G_Util gut = NameMgr.画布.GetComponent<G_Util>();
        if (user != null)
        {
            combat cb = user.GetComponent<combat>();
            gut.提升属性(cb, "攻击速度", 35, 6);
            gut.提升属性(cb, "伤害加成", 10, 6);
            gut.生成技能特效_攻击(user);
        }

        user = null;
    }

    public void 狂战()
    {
        G_Util gut = NameMgr.画布.GetComponent<G_Util>();
        if (user != null)
        {
            combat cb = user.GetComponent<combat>();
            gut.提升属性(cb, "攻击速度", 20, 10);
            gut.提升属性(cb, "伤害加成", 5, 10);
            gut.提升属性(cb, "暴击率", 5, 10);
            gut.生成技能特效_攻击(user);
        }

        user = null;
    }

    public void 化龙()
    {
        G_Util gut = NameMgr.画布.GetComponent<G_Util>();
        if (user != null)
        {
            combat cb = user.GetComponent<combat>();
            gut.提升属性(cb, "攻击速度", 40, 20);
            gut.提升属性(cb, "攻击力加成", 15, 20);
            gut.提升属性(cb, "防御力加成", 15, 20);
            gut.生成技能特效_攻击(user);
        }

        user = null;
    }

    public void 看我打篮球()
    {
        G_Util gut = NameMgr.画布.GetComponent<G_Util>();
        if (user != null)
        {
            combat cb = user.GetComponent<combat>();
            gut.提升属性(cb, "攻击速度", 40, 10);
            gut.提升属性(cb, "吸血加成", 15, 10);
            gut.提升属性(cb, "暴击率", 10, 7);
            gut.生成技能特效_攻击(user);
        }

        user = null;
    }


    public void 磐石()
    {
        G_Util gut = NameMgr.画布.GetComponent<G_Util>();
        if (user != null)
        {
            gut.生成技能特效_防御(user, 10);
            combat cb = user.GetComponent<combat>();
            gut.提升属性(cb, "防御力", 80, 10);
        }

        user = null;
    }

    public void 防御姿态()
    {
        G_Util gut = NameMgr.画布.GetComponent<G_Util>();
        if (user != null)
        {
            combat cb = user.GetComponent<combat>();
            gut.持续提升属性(cb, "防御力加成", 4, 10,1);
            gut.生成技能特效_防御(user, 10);
        }

        user = null;
    }

    public void 攻击姿态()
    {
        G_Util gut = NameMgr.画布.GetComponent<G_Util>();
        if (user != null)
        {
            combat cb = user.GetComponent<combat>();
            gut.持续提升属性(cb, "攻击力加成", 4, 10, 1);
            gut.生成技能特效_攻击(user);
        }

        user = null;
    }

    public void 獠牙()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
       GameObject 攻击对象;
        if (user != null)
        {
            string 敌人;
            if (user.CompareTag("人物") || user.CompareTag("宠物"))
                敌人 = "怪物";
            else
                敌人 = "角色";
            combat cb = user.GetComponent<combat>();
            if (cb.目标名字 != null && cb.目标名字.Equals(""))
                攻击对象 = mmgr.随意寻找一个目标(敌人);
            else
                攻击对象 = GameObject.Find(cb.目标名字);

            int 伤害 = (int)(bm.Xstoi(cb.战斗攻击力) * 1.5f + 100);// 这里改参数
            combat mcb = 攻击对象.GetComponent<combat>();
            if (mcb != null && (mcb.gameObject.CompareTag("怪物") || mcb.gameObject.CompareTag("boss")))
                mcb.开启战斗();
            gut.启动延迟伤害(cb, 伤害, 攻击对象, 0.05f);

            gut.生成技能特效_单体(攻击对象);
        }

        user = null;
    }

    public void 撞击()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        GameObject 攻击对象;
        if (user != null)
        {
            string 敌人;
            if (user.CompareTag("人物") || user.CompareTag("宠物"))
                敌人 = "怪物";
            else
                敌人 = "角色";
            combat cb = user.GetComponent<combat>();
            if (cb.目标名字 != null && cb.目标名字.Equals(""))
                攻击对象 = mmgr.随意寻找一个目标(敌人);
            else
                攻击对象 = GameObject.Find(cb.目标名字);

            int 伤害 = (int)(bm.Xstoi(cb.战斗攻击力) * 1.5f + 100);//这里改参数
            combat mcb = 攻击对象.GetComponent<combat>();
            if (mcb != null && (mcb.gameObject.CompareTag("怪物") || mcb.gameObject.CompareTag("boss")))
                mcb.开启战斗();
            gut.启动延迟伤害(cb, 伤害, 攻击对象, 0.05f);

            gut.生成技能特效_单体(攻击对象);
        }

        user = null;
    }


    public void 律师函警告()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        GameObject 攻击对象;
        if (user != null)
        {
            string 敌人;
            if (user.CompareTag("人物") || user.CompareTag("宠物"))
                敌人 = "怪物";
            else
                敌人 = "角色";
            combat cb = user.GetComponent<combat>();
            if (cb.目标名字 != null && cb.目标名字.Equals(""))
                攻击对象 = mmgr.随意寻找一个目标(敌人);
            else
                攻击对象 = GameObject.Find(cb.目标名字);

            int 伤害 = (int)(bm.Xstoi(cb.战斗攻击力) * 2.0f + 100);//这里改参数
            combat mcb = 攻击对象.GetComponent<combat>();
            if (mcb != null && (mcb.gameObject.CompareTag("怪物") || mcb.gameObject.CompareTag("boss")))
                mcb.开启战斗();
            gut.启动延迟伤害(cb, 伤害, 攻击对象, 0.05f);

            gut.生成技能特效_单体(攻击对象);
        }

        user = null;
    }

    public void 拍打()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        GameObject 攻击对象;
        if (user != null)
        {
            string 敌人;
            if (user.CompareTag("人物") || user.CompareTag("宠物"))
                敌人 = "怪物";
            else
                敌人 = "角色";
            combat cb = user.GetComponent<combat>();
            if (cb.目标名字 != null && cb.目标名字.Equals(""))
                攻击对象 = mmgr.随意寻找一个目标(敌人);
            else
                攻击对象 = GameObject.Find(cb.目标名字);

            int 伤害 = (int)(bm.Xstoi(cb.战斗攻击力) * 1.4f + 100);//这里改参数
            combat mcb = 攻击对象.GetComponent<combat>();
            if (mcb != null && (mcb.gameObject.CompareTag("怪物") || mcb.gameObject.CompareTag("boss")))
                mcb.开启战斗();
            gut.启动延迟伤害(cb, 伤害, 攻击对象, 0.05f);

            gut.生成技能特效_单体(攻击对象);
        }

        user = null;
    }

    public void 铁砂掌()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        GameObject 攻击对象;
        if (user != null)
        {
            string 敌人;
            if (user.CompareTag("人物") || user.CompareTag("宠物"))
                敌人 = "怪物";
            else
                敌人 = "角色";
            combat cb = user.GetComponent<combat>();
            if (cb.目标名字 != null && cb.目标名字.Equals(""))
                攻击对象 = mmgr.随意寻找一个目标(敌人);
            else
                攻击对象 = GameObject.Find(cb.目标名字);

            int 伤害 = (int)(bm.Xstoi(cb.战斗攻击力) * 0.8f + 30);//这里改参数
            combat mcb = 攻击对象.GetComponent<combat>();
            if (mcb != null && (mcb.gameObject.CompareTag("怪物") || mcb.gameObject.CompareTag("boss")))
                mcb.开启战斗();
            gut.启动延迟伤害(cb, 伤害, 攻击对象, 0.05f);

            gut.生成技能特效_单体(攻击对象);
        }

        user = null;
    }


    public void 陨仙剑()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        GameObject 攻击对象;
        if (user != null)
        {
            string 敌人;
            if (user.CompareTag("人物") || user.CompareTag("宠物"))
                敌人 = "怪物";
            else
                敌人 = "角色";
            combat cb = user.GetComponent<combat>();
            if (cb.目标名字 != null && cb.目标名字.Equals(""))
                攻击对象 = mmgr.随意寻找一个目标(敌人);
            else
                攻击对象 = GameObject.Find(cb.目标名字);

            int 伤害 = (int)(bm.Xstoi(cb.战斗攻击力) * 3.6f + 1000);//这里改参数
            combat mcb = 攻击对象.GetComponent<combat>();
            if (mcb != null && (mcb.gameObject.CompareTag("怪物") || mcb.gameObject.CompareTag("boss")))
                mcb.开启战斗();
            gut.启动延迟伤害(cb, 伤害, 攻击对象, 0.05f);

            gut.回血(cb, int.Parse(Math.Floor(伤害 * 0.2f) + ""));
            //gut.扣血显示(user, int.Parse(Math.Floor(伤害 * 0.2f) + ""), "回血");不能有,有了触发2次掉落

            gut.生成技能特效_三剑(攻击对象);
        }

        user = null;
    }

    public void 荒天拳()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        List<GameObject> 攻击对象;
        if (user != null)
        {
            string 敌人;
            if (user.CompareTag("人物") || user.CompareTag("宠物"))
                敌人 = "怪物";
            else
                敌人 = "角色";
            combat cb = user.GetComponent<combat>();
            攻击对象 = mmgr.获取所有敌人(敌人);

            for (int i = 0; i < 攻击对象.Count; i++)
            {
                int 伤害 = (int)(bm.Xstoi(cb.战斗攻击力) * 2.5f + 1000);
                if (攻击对象[i].activeSelf)
                {
                    combat mcb = 攻击对象[i].GetComponent<combat>();
                    if (mcb != null && (攻击对象[i].CompareTag("怪物") || 攻击对象[i].CompareTag("boss")))
                        mcb.开启战斗();
                    gut.启动延迟伤害(cb, 伤害, 攻击对象[i], 0.1f);

                }

                if (i == 0)
                    父物体 = 攻击对象[i];
                else
                {
                    if (父物体.transform.position.y > 攻击对象[i].transform.position.y)
                        父物体 = 攻击对象[i];
                }

            }

            if (攻击对象.Count > 0)
                gut.生成技能特效_拳爆(父物体.transform.parent.parent.gameObject);
        }

        user = null;
    }

    public void 鹰爪功()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        GameObject 攻击对象;
        if (user != null)
        {
            string 敌人;
            if (user.CompareTag("人物") || user.CompareTag("宠物"))
                敌人 = "怪物";
            else
                敌人 = "角色";
            combat cb = user.GetComponent<combat>();
            if (cb.目标名字 != null && cb.目标名字.Equals(""))
                攻击对象 = mmgr.随意寻找一个目标(敌人);
            else
                攻击对象 = GameObject.Find(cb.目标名字);

            int 伤害 = (int)(bm.Xstoi(cb.战斗攻击力) * 1.1f + 50);//这里改参数
            combat mcb = 攻击对象.GetComponent<combat>();
            if (mcb != null && (mcb.gameObject.CompareTag("怪物") || mcb.gameObject.CompareTag("boss")))
                mcb.开启战斗();
            gut.启动延迟伤害(cb, 伤害, 攻击对象, 0.05f);

            gut.生成技能特效_单体(攻击对象);
        }

        user = null;
    }

    public void 吞噬之触()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        GameObject 攻击对象;
        if (user != null)
        {
            string 敌人;
            if (user.CompareTag("人物") || user.CompareTag("宠物"))
                敌人 = "怪物";
            else
                敌人 = "角色";
            combat cb = user.GetComponent<combat>();
            if (cb.目标名字 != null && cb.目标名字.Equals(""))
                攻击对象 = mmgr.随意寻找一个目标(敌人);
            else
                攻击对象 = GameObject.Find(cb.目标名字);

            int 伤害 = (int)(bm.Xstoi(cb.战斗攻击力) * 1.7f + 300);//这里改参数
            combat mcb = 攻击对象.GetComponent<combat>();
            if (mcb != null && (mcb.gameObject.CompareTag("怪物") || mcb.gameObject.CompareTag("boss")))
                mcb.开启战斗();
            gut.启动延迟伤害(cb, 伤害, 攻击对象, 0.05f);

            gut.回血(cb, 伤害);
            gut.扣血显示(user, 伤害 , "回血");

            gut.生成技能特效_单体(攻击对象);
        }

        user = null;
    }



    public void 强击()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        GameObject 攻击对象;
                if (user != null)
                {
                    string 敌人;
                    if (user.CompareTag("人物") || user.CompareTag("宠物"))
                        敌人 = "怪物";
                    else
                        敌人 = "角色";
                    combat cb = user.GetComponent<combat>();
                    if (cb.目标名字 != null && cb.目标名字.Equals(""))
                        攻击对象 = mmgr.随意寻找一个目标(敌人);
                    else
                        攻击对象 = GameObject.Find(cb.目标名字);

                    if (攻击对象 != null)
                    {
                        int 伤害 = (int)(bm.Xstoi(cb.战斗攻击力) * 1.8f + 80);//这里改参数
                        combat mcb = 攻击对象.GetComponent<combat>();
                        if (mcb != null && (mcb.gameObject.CompareTag("怪物") || mcb.gameObject.CompareTag("boss")))
                            mcb.开启战斗();
                        gut.启动延迟伤害(cb, 伤害, 攻击对象, 0.05f);
                        gut.生成技能特效_单体(攻击对象);
                    }
                }
        user = null;
    }

}
