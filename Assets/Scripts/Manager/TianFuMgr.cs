using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TianFuMgr : BaseManager<TianFuMgr>
{
    DataMgr dm = DataMgr.GetInstance();
    basicMgr bm = basicMgr.GetInstance();
    io io_ = io.GetInstance();
    PropMgr pm = PropMgr.GetInstance();
    public GameObject user;

    public Dictionary<string, Action> 加载天赋效果()
    {
        Dictionary<string, Action> 天赋效果表 = new Dictionary<string, Action>();
        天赋效果表.Add("强化骨骼", 强化骨骼);
        天赋效果表.Add("强化皮膜", 强化皮膜);
        天赋效果表.Add("强化呼吸", 强化呼吸);
        天赋效果表.Add("强化血液", 强化血液);
        天赋效果表.Add("修炼拳脚", 修炼拳脚);
        天赋效果表.Add("修炼体魄", 修炼体魄);
        天赋效果表.Add("修炼血脉", 修炼血脉);
        天赋效果表.Add("修炼神识", 修炼神识);
        天赋效果表.Add("黄巾力士", 黄巾力士);
        天赋效果表.Add("无损之躯", 无损之躯);
        天赋效果表.Add("破灭之力", 破灭之力);
        天赋效果表.Add("不动如山", 不动如山);
        天赋效果表.Add("锋锐", 锋锐);
        天赋效果表.Add("坚固", 坚固);
        天赋效果表.Add("饮血", 饮血);

        return 天赋效果表;
    }


    public TianFu 返回天赋(string TfName)
    {
        if (PropMgr.天赋表.ContainsKey(TfName))
        {
            return PropMgr.天赋表[TfName];
        }
        else
        {
            Debug.Log("错误,没有该天赋");
            return null;
        }
    }

    public void 天赋加载图标(GameObject Img, TianFu Tf)
    {
        Img.GetComponent<Image>().sprite = bm.GetChildSprite("技能", int.Parse(Tf.icon));
    }

    public TianFu 刷新介绍(TianFu tf)
    {

        role_Data myData = io_.load();
        if (tf.name.Equals("强化骨骼"))
        {
            tf.effect = "效果:提升" + (bm.Xstoi(myData.天赋["天赋"]["强化骨骼"]) * 1) + "点攻击力";
            tf.next = "下一级:提升" + ((bm.Xstoi(myData.天赋["天赋"]["强化骨骼"]) + 1) * 1) + "点攻击力";
        }
        else if (tf.name.Equals("强化皮膜"))
        {
            tf.effect = "效果:提升" + (bm.Xstoi(myData.天赋["天赋"]["强化皮膜"]) * 1) + "点防御力";
            tf.next = "下一级:提升" + ((bm.Xstoi(myData.天赋["天赋"]["强化皮膜"]) + 1) * 1) + "点防御力";
        }
        else if (tf.name.Equals("强化血液"))
        {
            tf.effect = "效果:提升" + (bm.Xstoi(myData.天赋["天赋"]["强化肌肉"]) * 10) + "点血量";
            tf.next = "下一级:提升" + ((bm.Xstoi(myData.天赋["天赋"]["强化肌肉"]) + 1) * 10) + "点血量";
        }
        else if (tf.name.Equals("强化呼吸"))
        {
            tf.effect = "效果:提升" + (bm.Xstoi(myData.天赋["天赋"]["强化血液"]) * 5) + "点回血";
            tf.next = "下一级:提升" + ((bm.Xstoi(myData.天赋["天赋"]["强化血液"]) + 1) * 5) + "点回血";
        }
        else if (tf.name.Equals("黄巾力士"))
        {
            tf.effect = "效果:提升" + (bm.Xstoi(myData.天赋["天赋"]["黄巾力士"]) * 2) + "%血量加成";
            tf.next = "下一级:提升" + ((bm.Xstoi(myData.天赋["天赋"]["黄巾力士"]) + 1) * 2) + "%血量加成";
        }
        else if (tf.name.Equals("无损之躯"))
        {
            tf.effect = "效果:提升" + (bm.Xstoi(myData.天赋["天赋"]["无损之躯"]) * 2) + "%回血加成";
            tf.next = "下一级:提升" + ((bm.Xstoi(myData.天赋["天赋"]["无损之躯"]) + 1) * 2) + "%回血加成";
        }
        else if (tf.name.Equals("破灭之力"))
        {
            tf.effect = "效果:提升" + (bm.Xstoi(myData.天赋["天赋"]["破灭之力"]) * 1) + "%伤害加成";
            tf.next = "下一级:提升" + ((bm.Xstoi(myData.天赋["天赋"]["破灭之力"]) + 1) * 1) + "%伤害加成";
        }
        else if (tf.name.Equals("不动如山"))
        {
            tf.effect = "效果:提升" + (bm.Xstoi(myData.天赋["天赋"]["不动如山"]) * 1) + "%伤害减免";
            tf.next = "下一级:提升" + ((bm.Xstoi(myData.天赋["天赋"]["不动如山"]) + 1) * 1) + "%伤害减免";
        }

        return tf;
    }


    public void 被动增益(string 天赋名字, string 天赋类型, string 增益属性,float 增益比例) {
        role_Data myData = io_.load();
        dm.天赋属性[增益属性] = bm.Xftos(bm.Xstoi(dm.技能属性[增益属性]) + bm.Xstoi(myData.天赋[天赋类型][天赋名字]) * 增益比例);
        user = null;
    }


    public void 强化骨骼()
    {
        被动增益("强化骨骼", "天赋", "攻击力", 2f);
    }


   
    public void 强化皮膜()
    {
        被动增益("强化皮膜", "天赋", "防御力", 1f);
    }


    public void 强化血液()
    {
        被动增益("强化骨骼", "天赋", "血量", 20f);
    }


    public void 强化呼吸()
    {
        被动增益("强化呼吸", "天赋", "回血", 3f);
    }


    public void 修炼拳脚()
    {
        被动增益("修炼拳脚", "职业", "攻击力", 5f);
    }

    public void 修炼体魄()
    {
        被动增益("修炼体魄", "职业", "防御力", 3f);
    }

    public void 修炼血脉()
    {
        被动增益("修炼血脉", "职业", "血量", 60f);
    }

    public void 修炼神识()
    {
        被动增益("修炼神识", "职业", "回血", 10f);
    }


    public void 黄巾力士()
    {
        role_Data myData = io_.load();
        dm.天赋属性["血量加成"] = bm.Xitos(bm.Xstoi(dm.技能属性["血量加成"]) + bm.Xstoi(myData.天赋["天赋"]["黄巾力士"]) * 2);
        user = null;
    }


    public void 无损之躯()
    {
        role_Data myData = io_.load();
        dm.天赋属性["回血加成"] = bm.Xitos(bm.Xstoi(dm.技能属性["回血加成"]) + bm.Xstoi(myData.天赋["天赋"]["无损之躯"]) * 2);
        user = null;
    }


    public void 破灭之力()
    {
        role_Data myData = io_.load();
        dm.天赋属性["伤害加成"] = bm.Xitos(bm.Xstoi(dm.技能属性["伤害加成"]) + bm.Xstoi(myData.天赋["天赋"]["破灭之力"]) * 1);
        user = null;
    }


    public void 不动如山()
    {
        role_Data myData = io_.load();
        dm.天赋属性["伤害减免"] = bm.Xitos(bm.Xstoi(dm.技能属性["伤害减免"]) + bm.Xstoi(myData.天赋["天赋"]["不动如山"]) * 1);
        user = null;
    }


    public void 锋锐()
    {
        role_Data myData = io_.load();
        dm.天赋属性["固定伤害"] = bm.Xitos(bm.Xstoi(dm.技能属性["固定伤害"]) + bm.Xstoi(myData.天赋["天赋"]["锋锐"]) * 2 );
        user = null;
    }

    public void 坚固()
    {
        role_Data myData = io_.load();
        dm.天赋属性["固定减伤"] = bm.Xitos(bm.Xstoi(dm.技能属性["固定减伤"]) + bm.Xstoi(myData.天赋["天赋"]["坚固"]) * 1 );
        user = null;
    }

    public void 饮血()
    {
        role_Data myData = io_.load();
        dm.天赋属性["固定吸血"] = bm.Xitos(bm.Xstoi(dm.技能属性["固定吸血"]) + (int)(bm.Xstoi(myData.天赋["天赋"]["吸血"]) * 0.5f ));
        user = null;
    }

    public void 剑_快剑()
    {
        role_Data myData = io_.load();
        int 三目 = (int)((bm.Xstoi(myData.天赋["职业"]["剑_快剑"]) * 0.5f ) >= 100 ? 100 : (bm.Xstoi(myData.天赋["职业"]["剑_快剑"]) * 0.5f ));
        dm.天赋属性["攻击速度"] = bm.Xitos(bm.Xstoi(dm.技能属性["攻击速度"]) + 三目);
        user = null;
    }


}
