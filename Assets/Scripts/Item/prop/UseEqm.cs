using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseEqm : MonoBehaviour
{
    
    private io io_;
    private PropMgr pm;
    private combat cb;
    private G_Util utg;
    private basicMgr bm;


    private void Awake()
    {
        io_ = io.GetInstance();
        pm = PropMgr.GetInstance();
        if (!NameMgr.画布.CompareTag("幻界"))
        {
            cb =DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>();
        }
        utg = NameMgr.画布.GetComponent<G_Util>();;
        bm = basicMgr.GetInstance();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


   


    public string 使用_Data() {
        GameObject 选中 = GameObject.FindGameObjectWithTag("选中");
        if (选中 != null)//判断有选中项
        {
            string 道具名字 = 选中.transform.Find("名字").GetComponent<Text>().text;
            role_Data myData = io_.load();
            if (!PropMgr.材料表.ContainsKey(道具名字))//判断使用的是装备
            {
                Equipment 装备 = myData.装备背包[选中.name];

                if (bm.Xstoi(myData.等级) < bm.Xstoi(装备.lessgrade))
                    return "等级不足";

                //数据写入-装备
                if (myData.装备槽[装备.place] != null)//判断原先位置是否有装备,有则取下
                    pm.获取物品(myData.装备槽[装备.place]);
                myData = io_.load();//更新数据
                myData.装备槽[装备.place] = 装备;
                io_.save(myData);
                pm.失去装备(选中.name);

                Bag bag = gameObject.GetComponent<Bag>();
                bag.初始化背包();
                if (!NameMgr.画布.CompareTag("幻界"))
                    cb.人物属性刷新();
                return "使用成功";
            }
            else if (PropMgr.材料表.ContainsKey(道具名字))
            //数据写入-材料
            {
                if (myData.材料背包.ContainsKey(道具名字) && bm.Xstoi(myData.材料背包[道具名字].num) > 0)
                {
                    /*  string 返回状态 = utg.使用道具(道具名字);
                     if (!返回状态.Equals("使用成功"))
                         return 返回状态;
                     else
                         return "使用成功";*/
                    Prop_bascis pb = pm.检索物品(道具名字);
                    if (pb.icon.Equals("精华") || pb.icon.Equals("晶矿"))
                    {
                        utg.生成合成界面(pb.name);
                        return "null";
                    }
                    else if (pb.icon.Equals("收集品")) {
                        return "材料无法使用";
                    }
                    if (!(pb.icon.Equals("血药") || pb.icon.Equals("暗器") || pb.icon.Equals("技能书") || pb.icon.Equals("钥匙") || pb.icon.Equals("宠物蛋")))
                    {
                        utg.生成拉条(道具名字,"" ,0, bm.Xstoi(myData.材料背包[道具名字].num), "铜币", "使用道具");
                        return "null";
                    }
                    else
                    {
                       string 返回值= utg.使用道具(道具名字, 1);
                        return 返回值;
                    }
                }
                else
                {
                    return "没有该道具";
                }

            }
            else
            {
                return "未知道具";
            }
        }
        else
        {
            return "未选中";
        }
    
    }

}
