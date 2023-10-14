using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISetting : MonoBehaviour
{
    private io io_;
    private G_Util gut;
    private basicMgr bm;



    private void Awake()
    {
        io_ = io.GetInstance();
        gut = NameMgr.画布.GetComponent<G_Util>();
        bm = basicMgr.GetInstance();

    }

    // Start is called before the first frame update
    void Start()
    {
        role_Data myData = io_.load();
        gameObject.transform.Find("状态页").gameObject.SetActive(false);
        if (myData.事件表.Count == 0) {
            EvenMgr.GetInstance().控制事件图标的显隐(false);
        }
        if (myData.记录.ContainsKey("通关仙路")) {
            gameObject.transform.Find("menu/world/blue_bg_pic").transform.Find("条件").gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void 初始化地址()
    {
        GameObject 地址父物体 = gameObject.transform.Find("战斗页").transform.Find("地图/列表/Scroll View/Viewport/Content").gameObject;
        int 宽系数 = AdressMgr.地图表.Count / 3;
        if (AdressMgr.地图表.Count % 3 != 0)
            宽系数++;
        地址父物体.GetComponent<RectTransform>().sizeDelta = new Vector2(780, 160*宽系数);
        int i = 1;
        foreach (string 地名 in AdressMgr.地图表.Keys) {
            gut.生成地址(地址父物体, i, 地名);
            i++;
        }
    }




    public void 初始化战斗设置()
    {
        //检索战斗设置
        role_Data myData = io_.load();
        if (!myData.列表型记录.ContainsKey("战斗设置"))
            myData.列表型记录.Add("战斗设置", new Dictionary<string, List<string>>() { { "战斗设置", new List<string>() } });

        Dictionary<string, List<string>> 战斗设置 = myData.列表型记录["战斗设置"];
        foreach (string str_Setting in 战斗设置["战斗设置"])
        {
            gameObject.transform.Find("战斗页/combat_bg/2级画布/战斗设置面板/" + str_Setting + "/Button/打勾").gameObject.SetActive(true);
        }
        io_.save(myData);

    }



    public void 初始化仙晶商城() {
        gut.初始化仙晶商城();
    }


    public void 初始化地图() {
        GameObject 地图界面 = gameObject.transform.Find("战斗页").transform.Find("地图").gameObject;
        if (!地图界面.activeSelf) {
            地图界面.SetActive(true);
            初始化地址();
        }
    }

}
