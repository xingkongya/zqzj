using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellSetting : MonoBehaviour
{
    private io io_;
    private G_Util gut;


    private void Awake()
    {
        io_ = io.GetInstance();
        gut = NameMgr.画布.GetComponent<G_Util>();
    }

    public void 点击设置() {
        role_Data myData = io_.load();
        string name = gameObject.transform.parent.name;
        if (!myData.列表型记录.ContainsKey("出售设置"))
            myData.列表型记录.Add("出售设置", new Dictionary<string, List<string>>() { { "星级", new List<string>() }, { "颜色", new List<string>() } });
        Dictionary<string, List<string>> 出售设置 = myData.列表型记录["出售设置"];
        if (出售设置["星级"].Contains(name))
            出售设置["星级"].Remove(name);
        else if (出售设置["颜色"].Contains(name))
            出售设置["颜色"].Remove(name);
        else
        {
            if (name.Equals("一星") || name.Equals("二星") || name.Equals("三星") || name.Equals("四星") || name.Equals("五星"))
                出售设置["星级"].Add(name);
            else
                出售设置["颜色"].Add(name);
        }

        myData.列表型记录["出售设置"] = 出售设置;
        io_.save(myData);
    }


    public void 战斗设置() {
        combat rcb = DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>();
        role_Data myData = io_.load();
        string name = gameObject.transform.parent.name;
        if (!myData.列表型记录.ContainsKey("战斗设置"))
            myData.列表型记录.Add("战斗设置", new Dictionary<string, List<string>>() { { "战斗设置", new List<string>() } });
        Dictionary<string, List<string>> 战斗设置 = myData.列表型记录["战斗设置"];
        if (战斗设置["战斗设置"].Contains(name))
            战斗设置["战斗设置"].Remove(name);
        else
        {
            战斗设置["战斗设置"].Add(name);
            if (name.Equals("宠物主动攻击"))
            {
                GameObject 宠物 = GameObject.FindGameObjectWithTag("宠物");
                if (宠物 != null)
                {
                    combat petcb = 宠物.GetComponent<combat>();
                    gut.自动攻击(petcb);
                }
            }
        }
        myData.列表型记录["战斗设置"] = 战斗设置;
        io_.save(myData);
        rcb.myData = myData;
    }
}
