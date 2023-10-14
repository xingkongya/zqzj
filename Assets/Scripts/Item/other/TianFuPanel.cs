using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TianFuPanel : MonoBehaviour
{
    private basicMgr bm;
    public TianFu tf;
    public string TFName;
    private G_Util gut;
    private TianFuMgr tfm;
    private PropMgr pm;

    private void Awake()
    {
        bm = basicMgr.GetInstance();
        gut = NameMgr.画布.GetComponent<G_Util>();
        tfm = TianFuMgr.GetInstance();
        pm = PropMgr.GetInstance();
    }

    private void OnEnable()
    {
   
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void 刷新天赋信息(TianFu tf)
    {
        tf = tfm.刷新介绍(tf);

        Text 名字文本 = gameObject.transform.Find("name").GetComponent<Text>();
        名字文本.text = tf.name;
        名字文本.color = bm.转换颜色(int.Parse(tf.qua));

        Text 当前等级文本 = gameObject.transform.Find("lv").GetComponent<Text>();

        int 当前等级 = tfm.返回存档该天赋等级(tf);
        当前等级文本.text = "+" + 当前等级;

        Text 等级上限 = gameObject.transform.Find("maxlv").GetComponent<Text>();
        if (tf.name.IndexOf("修炼")!=-1&&tf.place.Equals("职业"))
        {
            role_Data myData = NameMgr.IO.load();
            等级上限.text = "(" + 当前等级 + "/"+ bm.Xstoi(myData.等级) / 2 + ")";
        }
        else
        {
            等级上限.text = "(" + 当前等级 + "/" + tf.MaxLv + ")";

        }

        Text 类型文本 = gameObject.transform.Find("place").GetComponent<Text>();
        类型文本.text = tf.place+"  ("+tf.type+")";

        Text 介绍文本 = gameObject.transform.Find("commend").GetComponent<Text>();
        介绍文本.text = tf.comment;

        Text 当前效果文本 = gameObject.transform.Find("effect").GetComponent<Text>();
        当前效果文本.text = tf.effect;

        Text 下级效果文本 = gameObject.transform.Find("next").GetComponent<Text>();
        下级效果文本.text = tf.next;
    }






    public void 天赋升级_天赋()//server-逻辑层
    {
        role_Data myData = NameMgr.IO.load();

        //Dao-数据层
        if (tf.place.Equals("职业"))
        {
            if (tf.comment.Equals("初级")) { //职业天赋的等级上限
                tf.MaxLv = bm.Xstoi(myData.等级) / 2+ "";
            }


            if (!tf.MaxLv.Equals("无限") && tfm.返回存档该天赋等级(tf) >= int.Parse(tf.MaxLv))
            {
                gut.生成警告框("等级达上限!");
                return;
            }


            //升级的是基础技能且技能点足够
            if ((tf.comment.Equals("初级") && myData.天赋["职业"].ContainsKey("基础技能点") && bm.Xstoi(myData.天赋["职业"]["基础技能点"]) >= 1) || (tf.comment.Equals("高级") && myData.天赋["职业"].ContainsKey("高级技能点") && bm.Xstoi(myData.天赋["职业"]["高级技能点"]) >= 1))
            {
                tfm.消耗技能点(tf);
                tfm.天赋升级_Dao(tf, "职业");
            }
            else if (tf.comment.Equals("核心")) {
                gut.生成警告框("无法升级!");
                return;
            }
            else
            {
                gut.生成警告框("技能点不足!");
                return;
            }

            //view-视图层
            GameObject 属性面板 = DataMgr.GetInstance().本地对象["属性面板"];
            属性面板.GetComponent<Role_Panel>().刷新剩余技能点和技能等级();
            gut.面板属性刷新(属性面板);
        }
        else
        {
            string 返回值;
            if (tf.comment.Equals("铜币天赋"))
            {
                返回值 = pm.失去金钱("铜币", 10000);

            }
            else if (tf.comment.Equals("仙晶天赋"))
            {
                返回值 = pm.失去金钱("仙晶", 100);
            }
            else {
                返回值 = pm.失去金钱("黑钻", 10);
            }


            if (返回值.Equals("成功"))
            {
                tfm.天赋升级_Dao(tf, "天赋");
            }
            else
            {
                gut.生成警告框(返回值);
                return;
            }

            //view-视图层(待补充)

        }
        //view-视图层
        刷新天赋信息(tf);
        gut.生成警告框("升级成功!");
        DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>().人物属性刷新();
        GameObject UI = NameMgr.UI;
        gut.刷新战斗力UI(UI);





    }






    public void 天赋升满级_天赋()//server-逻辑层
    {
        role_Data myData = NameMgr.IO.load();

        //Dao-数据层
        if (tf.place.Equals("职业"))
        {
            if (tf.comment.Equals("初级"))
            { //职业天赋的等级上限
                tf.MaxLv = bm.Xstoi(myData.等级) / 2 + "";
            }

            if (!tf.MaxLv.Equals("无限") && tfm.返回存档该天赋等级(tf) >= int.Parse(tf.MaxLv))
            {
                gut.生成警告框("等级达上限!");
                return;
            }


            int 消耗点数;
            //升级的是基础技能且技能点足够
            if (tf.comment.Equals("初级") && myData.天赋["职业"].ContainsKey("基础技能点") && bm.Xstoi(myData.天赋["职业"]["基础技能点"]) >= 1)
            {
                if (int.Parse( tf.MaxLv) - tfm.返回存档该天赋等级(tf) <= bm.Xstoi(myData.天赋["职业"]["基础技能点"])) {
                    消耗点数 = int.Parse( tf.MaxLv) - tfm.返回存档该天赋等级(tf);
                }
                else {
                    消耗点数 = bm.Xstoi(myData.天赋["职业"]["基础技能点"]);
                }
            }
            else if (tf.comment.Equals("高级") && myData.天赋["职业"].ContainsKey("高级技能点") && bm.Xstoi(myData.天赋["职业"]["高级技能点"]) >= 1) {
                if (int.Parse(tf.MaxLv) - tfm.返回存档该天赋等级(tf) <= bm.Xstoi(myData.天赋["职业"]["高级技能点"]))
                {
                    消耗点数 = int.Parse(tf.MaxLv) - tfm.返回存档该天赋等级(tf);
                }
                else
                {
                    消耗点数 = bm.Xstoi(myData.天赋["职业"]["高级技能点"]);
                }
            }
            else
            {
                gut.生成警告框("技能点不足!");
                return;
            }

            tfm.消耗任意技能点(tf,消耗点数);
            tfm.天赋升满级_Dao(tf, "职业",消耗点数);

            //view-视图层
            GameObject 属性面板 = DataMgr.GetInstance().本地对象["属性面板"];
            属性面板.GetComponent<Role_Panel>().刷新剩余技能点和技能等级();
            gut.面板属性刷新(属性面板);
        }
        else
        {
            string 返回值="金钱不足!";
            int 消耗点数 = 0 ;
            if (tf.comment.Equals("铜币天赋")&& bm.Xstoi(myData.金钱["铜币"])>=10000)
            {
               消耗点数 = bm.Xstoi(myData.金钱["铜币"]) / 10000;
                if (!tf.MaxLv.Equals("无限")&& 消耗点数 > int.Parse(tf.MaxLv)- bm.Xstoi( myData.天赋["天赋"][tf.name])) {
                    消耗点数 = int.Parse(tf.MaxLv) - bm.Xstoi(myData.天赋["天赋"][tf.name]);
                }
               返回值 = pm.失去金钱("铜币", 消耗点数*10000);

            }
            else if (tf.comment.Equals("仙晶天赋") && bm.Xstoi(myData.金钱["仙晶"])>=100)
            {
                消耗点数 = bm.Xstoi(myData.金钱["仙晶"]) / 100;
                if (!tf.MaxLv.Equals("无限") && 消耗点数 > int.Parse(tf.MaxLv) - bm.Xstoi(myData.天赋["天赋"][tf.name]))
                {
                    消耗点数 = int.Parse(tf.MaxLv) - bm.Xstoi(myData.天赋["天赋"][tf.name]);
                }
                返回值 = pm.失去金钱("仙晶", 消耗点数 * 100);
            }
            /*else 
            {
                消耗点数 = bm.Xstoi(myData.金钱["黑钻"]) / 10;
                返回值 = pm.失去金钱("黑钻", 消耗点数 * 10);
            }*/


            if (返回值.Equals("成功"))
            {
                tfm.天赋升满级_Dao(tf, "天赋",消耗点数);
            }
            else
            {
                gut.生成警告框(返回值);
                return;
            }

            //view-视图层(待补充)

        }
        //view-视图层
        刷新天赋信息(tf);
        gut.生成警告框("升级成功!");






    }
}
