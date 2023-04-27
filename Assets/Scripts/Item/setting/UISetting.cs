using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISetting : MonoBehaviour
{
    private io io_;


    private void Awake()
    {
        io_ = io.GetInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        role_Data myData = io_.load();
        gameObject.transform.Find("状态页").gameObject.SetActive(false);
        if (myData.记录.ContainsKey("通关仙路")) {
            gameObject.transform.Find("menu/world/blue_bg_pic").transform.Find("条件").gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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

}
