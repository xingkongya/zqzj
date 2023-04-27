using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongTianUtil : MonoBehaviour
{
    private EventCenter ec;
    public combat cb;


    private void Awake()
    {
        ec = EventCenter.GetInstance();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        ec.AddEventListener<combat>("怪物失败", 通天失败);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void 通天失败(combat cb)
    {
        Debug.Log("调用");
        role_Data myData = NameMgr.IO.load();
        m_tongtianta tt = GameObject.Find("combat_other").GetComponent<m_tongtianta>();
        if (myData.记录.ContainsKey("通天塔记录"))//这是已打败的层数,不需要加一
        {
            if (int.Parse(myData.记录["通天塔记录"]) < int.Parse(tt.层数)) {
                myData.记录["通天塔记录"] = tt.层数;
            }
        }
        else {
            myData.记录.Add("通天塔记录", tt.层数);
        }
        if(int.Parse(tt.层数)<100)
            tt.层数 = int.Parse(tt.层数) + 1 + "";//加一
        if (myData.树形记录["每日记录"].ContainsKey("通天塔层数"))
        {
            myData.树形记录["每日记录"]["通天塔层数"] = tt.层数;
        }
        else {
            myData.树形记录["每日记录"].Add("通天塔层数", tt.层数);
        }
        NameMgr.IO.save(myData);
        tt.进入下一层();
    }


    private void OnDisable()
    {
        ec.RemoveEventListener<combat>("怪物失败", 通天失败);
    }
}
