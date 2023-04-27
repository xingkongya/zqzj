using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleMgr : BaseManager<RoleMgr>
{
    private long 当前经验;
    private long 所需经验值;
    private io io_ = io.GetInstance();
    private basicMgr bm = basicMgr.GetInstance();
    private PropMgr pm = PropMgr.GetInstance();

    public bool 经验值结算(long 经验值) {
        bool isUP = false;
        role_Data myData = io.GetInstance().load();
        所需经验值 = 经验表(bm.Xstoi( myData.等级));
        当前经验 = long.Parse(bm.Xor( myData.当前经验));
        当前经验 += 经验值;
        if (所需经验值 <=当前经验)
        {
            if (bm.Xstoi(myData.等级) < bm.Xstoi(myData.限制等级))//等级限制锁
            {
                isUP = true;
                while (当前经验 >= 所需经验值&& bm.Xstoi(myData.等级) < bm.Xstoi(myData.限制等级))
                {
                    myData.等级 = bm.Xitos(bm.Xstoi(myData.等级) + 1);
                    当前经验 -= 所需经验值;
                    所需经验值 = 经验表(bm.Xstoi(myData.等级));
                }
            }     
        }
        myData.当前经验 = bm.Xor( 当前经验 + "");
        io.GetInstance().save(myData);
        return isUP;
    }


    public void 经验池结算(long 经验值)
    {
        role_Data myData = io.GetInstance().load();
        if (!myData.记录.ContainsKey("经验池"))
            myData.记录.Add("经验池", 经验值+"");
        else
            myData.记录["经验池"] = long.Parse(myData.记录["经验池"]) + 经验值 + "";
        io_.save(myData);
    }

    public void 金钱结算(Dictionary<string,int> 获得金钱)
    {
        role_Data myData = io.GetInstance().load();
        foreach (string 货币名 in 获得金钱.Keys) {
            if (myData.金钱.ContainsKey(货币名))
            {
                myData.金钱[货币名] = bm.Xitos(bm.Xstoi(myData.金钱[货币名]) + 获得金钱[货币名]);
            }
            else
            {
                Debug.Log("无此货币");
                return;
            }
        }
       
        io.GetInstance().save(myData);
    }

    public void 掉落结算(List<Prop_bascis> 掉落集合) {
        G_Util utg = NameMgr.画布.GetComponent<G_Util>();
        //掉落效果展示
        utg.生成获得提示(掉落集合,"掉落");




        for (int i = 0; i < 掉落集合.Count; i++) {
            string 名字 = 掉落集合[i].name;
            if (名字==null)
                return;
            pm.获取物品(名字,1);
        }
    }

    public long 经验表(int 等级)
    {
        //正则查询错误数据

        //所需经验值=基础经验+十等级进位增长值+总等级增长值
        float 十进制增加比值 = 1.03f;
        long 十进制增加偏移 = (long)Math.Pow(等级 * 1.3f, 3);
        long 所需经验值 = 25 + ((long)Math.Pow(十进制增加比值, Math.Ceiling((Double)等级 / 10)) + 十进制增加偏移) + ((等级 * (等级 - 1)) * 13);
        return 所需经验值;
    }

   public void 打印等级的经验表(int 等级)
    {
        for (int i = 1; i <= 等级; i++)
        {
            Debug.Log(i + "级所需经验: " + 经验表(i));
        }

    }

    public void 记录数据(string key,string value) {
        role_Data myData = io_.load();
        myData.记录.Add(key, value);
        io_.save(myData);
    }

    public void 记录数据_数值增长(string key, string value)
    {
        role_Data myData = io_.load();
        if (!myData.记录.ContainsKey(key))
            myData.记录.Add(key, value);
        else
            myData.记录[key] = int.Parse(myData.记录[key]) + int.Parse(value) + "";
        io_.save(myData);
    }

}
