using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuangHuan : MonoBehaviour
{
    public G_Util gut;
    public string 光环名字;
    private PropMgr pm;
    private basicMgr bm;

    private void Awake()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        pm = PropMgr.GetInstance();
        bm = basicMgr.GetInstance();
    }


    private void OnEnable()
    {
         //信息刷新
        光环信息刷新();
    }
    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private int 获取存档光环等级(string name) {
        //初始化
        role_Data myData = io.GetInstance().load();
        if (myData.拓展.ContainsKey("光环模块"))
        {
            if (name.Equals("修炼光环"))
                return bm.Xstoi(myData.拓展["光环模块"]["修炼光环"]);
            else if (name.Equals("无敌光环"))
                return bm.Xstoi(myData.拓展["光环模块"]["无敌光环"]);
            else if (name.Equals("奇遇光环"))
                return bm.Xstoi(myData.拓展["光环模块"]["奇遇光环"]);
            else if (name.Equals("幸运光环"))
                return bm.Xstoi(myData.拓展["光环模块"]["幸运光环"]);
            else
                return 0;
        }
        else
        {
            myData.拓展.Add("光环模块", new Dictionary<string, string>() { { "修炼光环", bm.Xitos(0) }, { "无敌光环", bm.Xitos(0) }, { "奇遇光环", bm.Xitos(0) }, { "幸运光环", bm.Xitos(0) } });
            io.GetInstance().save(myData);
            return 0;
        }


    }


    public void 光环信息刷新() {
        int 修炼光环等级 = 获取存档光环等级("修炼光环");
        int 无敌光环等级 = 获取存档光环等级("无敌光环");
        int 奇遇光环等级 = 获取存档光环等级("奇遇光环");
        int 幸运光环等级 = 获取存档光环等级("幸运光环");

        //name
        gameObject.transform.Find("修炼光环/name").GetComponent<Text>().text = "修炼光环 Lv" + 修炼光环等级;
        gameObject.transform.Find("无敌光环/name").GetComponent<Text>().text = "无敌光环 Lv" + 无敌光环等级;
        gameObject.transform.Find("奇遇光环/name").GetComponent<Text>().text = "奇遇光环 Lv" + 奇遇光环等级;
        gameObject.transform.Find("幸运光环/name").GetComponent<Text>().text = "幸运光环 Lv" + 幸运光环等级;
        //效果
        gameObject.transform.Find("修炼光环/效果").GetComponent<Text>().text = "效果:经验+" + (30 * 修炼光环等级) + "%";
        gameObject.transform.Find("无敌光环/效果").GetComponent<Text>().text = "效果:攻防血+" + (10 * 无敌光环等级) + "%";
        gameObject.transform.Find("奇遇光环/效果").GetComponent<Text>().text = "效果:遇到稀有怪概率+" + (10 * 奇遇光环等级) + "%";
        gameObject.transform.Find("幸运光环/效果").GetComponent<Text>().text = "效果:爆率+" + (10 * 幸运光环等级) + "%";
       
    }

    public void 升级信息刷新() {
        if (光环名字 == null || 光环名字.Equals("")) {
            return;
        }
        if (光环名字.Equals("修炼光环")) {
            打开升级界面_修炼();
        }else if (光环名字.Equals("无敌光环"))
        {
            打开升级界面_无敌();
        }else if (光环名字.Equals("奇遇光环"))
        {
            打开升级界面_奇遇();
        }else if (光环名字.Equals("幸运光环"))
        {
            打开升级界面_幸运();
        }
    }

    public void 打开升级界面_修炼()
    {

        int 修炼光环等级 = 获取存档光环等级("修炼光环");
        GameObject 升级界面 = gameObject.transform.Find("升级界面").gameObject;
        升级界面.SetActive(true);
        升级界面.transform.Find("光环名字/name").GetComponent<Text>().text = "修炼光环 Lv" + 修炼光环等级;
        升级界面.transform.Find("光环名字/当前效果").GetComponent<Text>().text = "效果:经验+" + (30 * 修炼光环等级) + "%";
        if (修炼光环等级 >= 10)
        {
            升级界面.transform.Find("光环名字/下一级效果").GetComponent<Text>().text = "已满级";
        }
        else
        {
            升级界面.transform.Find("光环名字/下一级效果").GetComponent<Text>().text = "下一级:经验+" + (30 * (修炼光环等级 + 1)) + "%";
        }
        光环名字 = "修炼光环";
        显示升级消耗(升级界面,光环名字);
    }

    public void 打开升级界面_无敌()
    {
        int 无敌光环等级 = 获取存档光环等级("无敌光环");
        GameObject 升级界面 = gameObject.transform.Find("升级界面").gameObject;
        升级界面.SetActive(true);
        升级界面.transform.Find("光环名字/name").GetComponent<Text>().text = "无敌光环 Lv" + 无敌光环等级;
        升级界面.transform.Find("光环名字/当前效果").GetComponent<Text>().text = "效果:攻防血+" + (10 * 无敌光环等级) + "%";
        if (无敌光环等级 >= 10)
        {
            升级界面.transform.Find("光环名字/下一级效果").GetComponent<Text>().text = "已满级";
        }
        else
        {
            升级界面.transform.Find("光环名字/下一级效果").GetComponent<Text>().text = "下一级:攻防血+" + (10 * (无敌光环等级 + 1)) + "%";
        }
        光环名字 = "无敌光环";
        显示升级消耗(升级界面, 光环名字);

    }

    public void 打开升级界面_奇遇()
    {
        int 奇遇光环等级 = 获取存档光环等级("奇遇光环");
        GameObject 升级界面 = gameObject.transform.Find("升级界面").gameObject;
        升级界面.SetActive(true);
        升级界面.transform.Find("光环名字/name").GetComponent<Text>().text = "奇遇光环 Lv" + 奇遇光环等级;
        升级界面.transform.Find("光环名字/当前效果").GetComponent<Text>().text = "效果:遇到稀有怪物概率+" + (10 * 奇遇光环等级) + "%";
        if (奇遇光环等级 >= 10)
        {
            升级界面.transform.Find("光环名字/下一级效果").GetComponent<Text>().text = "已满级";
        }
        else
        {
            升级界面.transform.Find("光环名字/下一级效果").GetComponent<Text>().text = "下一级:遇到稀有怪物概率+" + (10 * (奇遇光环等级 + 1)) + "%";
        }
        光环名字 = "奇遇光环";
        显示升级消耗(升级界面, 光环名字);

    }

    public void 打开升级界面_幸运()
    {
        int 幸运光环等级 = 获取存档光环等级("幸运光环");
        GameObject 升级界面 = gameObject.transform.Find("升级界面").gameObject;
        升级界面.SetActive(true);
        升级界面.transform.Find("光环名字/name").GetComponent<Text>().text = "幸运光环 Lv" + 幸运光环等级;
        升级界面.transform.Find("光环名字/当前效果").GetComponent<Text>().text = "效果:爆率+" + (10 * 幸运光环等级) + "%";
        if (幸运光环等级 >= 10)
        {
            升级界面.transform.Find("光环名字/下一级效果").GetComponent<Text>().text = "已满级";
        }
        else
        {
            升级界面.transform.Find("光环名字/下一级效果").GetComponent<Text>().text = "下一级:爆率+" + (10 * (幸运光环等级 + 1)) + "%";
        }
        光环名字 = "幸运光环";
        显示升级消耗(升级界面, 光环名字);

    }


    public void 显示升级消耗(GameObject 升级界面,string 光环名字) {
        if (int.Parse(pm.返回金钱数量("仙晶")) >= 返回升级价格(光环名字))
        {
            升级界面.transform.Find("光环名字/升级消耗").GetComponent<Text>().text = "升级仙晶:(<color=green>" + pm.返回金钱数量("仙晶") + "</color>/" + 返回升级价格(光环名字) + ")\n<color=blue>可升级</color>";
        }
        else
        {
            升级界面.transform.Find("光环名字/升级消耗").GetComponent<Text>().text = "升级仙晶:(<color=red>" + pm.返回金钱数量("仙晶") + "</color>/" + 返回升级价格(光环名字) + ")\n<color=red>仙晶不足</color>";
        }
    }

    public void 光环升级()
    {
        int 这个光环等级 = 获取存档光环等级(光环名字);

        if (光环名字 == null || 光环名字.Equals("")) {
            gut.生成警告框("升级出错!");
            return;
        }

        if (这个光环等级 >= 10) 
        { 
            gut.生成警告框("已满级");
        } 
        else {
            string 返回结果 = PropMgr.GetInstance().失去金钱("仙晶", 返回升级价格(光环名字));
            if (!返回结果.Equals("成功"))
            {
                gut.生成警告框(返回结果);
            }
            else
            {
                role_Data myData = io.GetInstance().load();
                myData.拓展["光环模块"][光环名字] = bm.Xitos(这个光环等级 + 1);
                io.GetInstance().save(myData);
                gut.生成警告框("升级成功");
            }
            光环信息刷新();
            升级信息刷新();
            DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>().人物属性刷新();
        }

    }

    private int 返回升级价格(string 光环名字) {
        int 价格=0;
        int 当前等级 = 获取存档光环等级(光环名字);
        switch (当前等级) {
            case 0:
                价格 = 100;
                break;
            case 1:
                价格 = 200;
                break;
            case 2:
                价格 = 400;
                break;
            case 3:
                价格 = 700;
                break;
            case 4:
                价格 = 1000;
                break;
            case 5:
                价格 = 1500;
                break;
            case 6:
                价格 = 2000;
                break;
            case 7:
                价格 = 2600;
                break;
            case 8:
                价格 = 3500;
                break;
            case 9:
                价格 = 5000;
                break;
            default:
                价格 = 0;
                break;
        }
        if (光环名字.Equals("无敌光环")) {
            价格 *= 2;
        }else if (光环名字.Equals("奇遇光环"))
        {
            价格 *= 4;
        }
        else if (光环名字.Equals("幸运光环"))
        {
            价格 *= 8;
        }
        return 价格;
    }

    public void 删除自己() {
        gut.删除对象(gameObject);
    }

}
